namespace Linq.Query.Composer
{
    using Linq.Query.Composer.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.SqlServer;
    using System.Globalization;
    //using System.Data.Objects.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class LambdaExpressionGenerator
    {
        public static Expression<Func<TEntity, TKeyType>> GetJoinPredicate<TEntity, TKeyType>(string property)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            return
                Expression.Lambda<Func<TEntity, TKeyType>>(
                    Expression.Convert(Expression.PropertyOrField(parameter, property), typeof(TKeyType)), parameter);
        }

        public static Expression<Func<TEntity, TKeyType>> GetNestedJoinPredicate<TEntity, TKeyType>(
            string childProperty, string targetProperty)
        {
            Type entityType = typeof(TEntity);

            ParameterExpression parameter = Expression.Parameter(entityType, "entity");
            MemberExpression childMember = Expression.MakeMemberAccess(parameter, entityType.GetProperty(childProperty));

            return
                Expression.Lambda<Func<TEntity, TKeyType>>(
                    Expression.Convert(Expression.PropertyOrField(childMember, targetProperty), typeof(TKeyType)),
                    parameter);
        }

        public static Expression<Func<TEntity, bool>> TASTemplateWhereExpression<TEntity>(bool isDefualtActive)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            Expression left = Expression.Property(parameter, typeof(TEntity).GetProperty("Deleted"));
            Expression right = Expression.Constant(false);

            Expression predicateBody = Expression.Equal(left, right);

            if (isDefualtActive)
            {
                left = Expression.Property(parameter, typeof(TEntity).GetProperty("Active"));
                right = Expression.Constant(true);
                Expression expressionActive = Expression.Equal(left, right);
                predicateBody = Expression.And(predicateBody, expressionActive);
            }

            return Expression.Lambda<Func<TEntity, bool>>(predicateBody, new[] { parameter });
        }

        public static Expression<Func<TMainSet, bool>> GenerateWhereClause<TMainSet>(
            List<FilterItem> searchItems, List<PropertyInfo> quickSearchProperties)
        {
            bool isDefualtActive = searchItems.Count == 0;

            Expression<Func<TMainSet, bool>> defaultExpression = TASTemplateWhereExpression<TMainSet>(isDefualtActive);

            Expression whereBody = defaultExpression.Body;
            ParameterExpression whereParameter = defaultExpression.Parameters[0];

            whereBody = searchItems.Aggregate(
                whereBody,
                (current, searchItem) =>
                GenerateSubWhereClause<TMainSet>(whereParameter, current, searchItem, quickSearchProperties));

            return Expression.Lambda<Func<TMainSet, bool>>(whereBody, whereParameter);
        }

        public static Expression GenerateSubWhereClause<TMainSet>(
            ParameterExpression appendantParameter,
            Expression appendantExpression,
            FilterItem searchItem,
            List<PropertyInfo> quickSearchProperties)
        {
            ConstantExpression filterValues = null;
            Expression expressionBody = null;

            List<string> propertyPath = searchItem.SearchKey.Split(new[] { '.' }).ToList();

            // temp hack due to both string and int checkboxes in system
            if (searchItem.SearchType.StartsWith("List")) 
            {
                if (searchItem.SearchType.EndsWith("<int>"))
                {
                    expressionBody = AssembleContainsExpression<TMainSet, int>(
                        appendantParameter, appendantExpression, searchItem, propertyPath, filterValues, expressionBody);
                }
                else if (searchItem.SearchType.EndsWith("<string>"))
                {
                    expressionBody = AssembleContainsExpression<TMainSet, string>(
                        appendantParameter, appendantExpression, searchItem, propertyPath, filterValues, expressionBody);
                }
            }
            else if (searchItem.SearchType == "autocomplete")
            {
                MemberExpression property = Expression.Property(appendantParameter, typeof(TMainSet), propertyPath[0]);

                string[] subArgs = searchItem.SearchData.Split(new char[] { '.' });

                if (property.Type == typeof(string))
                {
                    filterValues = Expression.Constant(subArgs[0]);
                }
                else
                {
                    filterValues = Expression.Constant(int.Parse(subArgs[0]));
                    if (property.Type == typeof(int?))
                    {
                        property = Expression.Property(property, "Value");
                    }
                }

                expressionBody = Expression.Equal(property, filterValues);
            }
            else if (searchItem.SearchType == "DateTime" || searchItem.SearchType == "Time")
            {
                MemberExpression property = Expression.Property(appendantParameter, typeof(TMainSet), propertyPath[0]);

                string[] subArgs = searchItem.SearchData.Split(new[] { '.' });

                if (property.Type == typeof(DateTime?))
                {
                    property = Expression.Property(property, "Value");
                }

                if (searchItem.SearchType == "DateTime")
                {
                    filterValues = Expression.Constant(DateTime.Parse(subArgs[0]));
                }
                else
                {
                    filterValues =
                        Expression.Constant(
                            DateTime.ParseExact(subArgs[0], "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay);
                }

                expressionBody = subArgs[1] == "GreaterThan"
                                     ? Expression.GreaterThanOrEqual(property, filterValues)
                                     : Expression.LessThanOrEqual(property, filterValues);
            }
            else if (searchItem.SearchType == "QuickSearch")
            {
                string term = searchItem.SearchData;

                // recursive call
                expressionBody = AssembleQuickSearchExpression<TMainSet>(
                    appendantParameter,
                    GenerateSubQuickSearchExpression<TMainSet>(appendantParameter, quickSearchProperties.First(), term),
                    quickSearchProperties.Skip(1).ToList(),
                    term);
            }
            else if (searchItem.SearchType == "Active")
            {
                MemberExpression property = Expression.Property(appendantParameter, typeof(TMainSet), "Active");
                string[] subArgs = searchItem.SearchData.Split(new[] { '.' });
                filterValues = Expression.Constant(bool.Parse(subArgs[0]), typeof(bool));
                expressionBody = Expression.Equal(property, filterValues);
                if (subArgs.LongLength > 1)
                {
                    Expression expressionBody2 = null;
                    filterValues = Expression.Constant(bool.Parse(subArgs[1]), typeof(bool));
                    expressionBody2 = Expression.Equal(property, filterValues);
                    expressionBody = Expression.Or(expressionBody, expressionBody2);
                }
            }
            else if (searchItem.SearchType == "bool")
            {
                MemberExpression property = Expression.Property(appendantParameter, typeof(TMainSet), propertyPath[0]);

                if (property.Type == typeof(bool?))
                {
                    property = Expression.Property(property, "Value");
                }

                filterValues = Expression.Constant(bool.Parse(searchItem.SearchData), typeof(bool));
                expressionBody = Expression.Equal(property, filterValues);
            }

            return Expression.And(expressionBody, appendantExpression);
        }

        public static Expression AssembleContainsExpression<TEntity, TProperty>(
            ParameterExpression appendantParameter,
            Expression appendantExpression,
            FilterItem searchItem,
            List<string> propertyPath,
            ConstantExpression filterValues,
            Expression expressionBody)
        {
            ParameterExpression parameter = appendantParameter;
            Type type = typeof(TEntity);

            string collectionProperty = null;

            if (propertyPath.Count > 1)
            {
                type =
                    Expression.Property(appendantParameter, typeof(TEntity), propertyPath[0])
                              .Type.GetGenericArguments()
                              .First();

                collectionProperty = propertyPath.First();
                propertyPath.RemoveAt(0);

                parameter = Expression.Parameter(type, "collectionEntity");
            }

            Expression property =
                LambdaExpressionGenerator.AssembleProperty(
                    Expression.MakeMemberAccess(parameter, type.GetProperty(propertyPath.First())),
                    propertyPath.Skip(1).ToList());

            List<TProperty> array =
                searchItem.SearchData.Split(new[] { "," }, StringSplitOptions.None)
                          .Select(s => (TProperty)Convert.ChangeType(s, typeof(TProperty)))
                          .ToList();

            if (property.Type == typeof(int?))
            {
                property = Expression.Property(property, "Value");
            }

            Type searchValuesType = array.GetType().GetGenericArguments().FirstOrDefault();
            filterValues = Expression.Constant(array, array.GetType());

            expressionBody = Expression.Call(
                typeof(Enumerable), "Contains", new[] { searchValuesType }, filterValues, property);

            if (!string.IsNullOrEmpty(collectionProperty))
            {
                property = Expression.Property(appendantParameter, typeof(TEntity), collectionProperty);

                // inner where - where(w => array.contains(w.property))
                expressionBody = Expression.Call(
                    typeof(Enumerable),
                    "Where",
                    new[] { type },
                    property,
                    Expression.Lambda(expressionBody, new[] { parameter }));

                // any - where(w => array.contains(w.property)).Any()
                expressionBody = Expression.Call(typeof(Enumerable), "Any", new[] { type }, expressionBody);
            }

            return expressionBody;
        }

        public static Expression AssembleQuickSearchExpression<TEntity>(
            ParameterExpression appendantParameter,
            Expression appendantExpression,
            List<PropertyInfo> quickSearchProperties,
            string term)
        {
            if (quickSearchProperties.Count > 0)
            {
                PropertyInfo property = quickSearchProperties.First();
                quickSearchProperties.RemoveAt(0);

                var appendedExpression = Expression.Or(
                    appendantExpression, GenerateSubQuickSearchExpression<TEntity>(appendantParameter, property, term));

                return AssembleQuickSearchExpression<TEntity>(
                    appendantParameter, appendedExpression, quickSearchProperties, term);
            }

            return appendantExpression;
        }

        public static Expression GenerateSubQuickSearchExpression<TEntity>(
            ParameterExpression appendantParameter, PropertyInfo quickSearchProperty, string term)
        {
            Expression expression = null;
            List<string> propertyPath = ObjectInspector.RetrieveGridEntityPropertyPath(quickSearchProperty);

            if (typeof(IEnumerable<string>).IsAssignableFrom(quickSearchProperty.PropertyType))
            {
                MemberExpression member = Expression.MakeMemberAccess(
                    appendantParameter, typeof(TEntity).GetProperty(propertyPath.First()));

                propertyPath.RemoveAt(0);

                expression = WhereForInnerCollectionQuickSearch(member, propertyPath, term);
            }
            else
            {
                Expression entityProperty =
                    LambdaExpressionGenerator.AssembleProperty(
                        Expression.MakeMemberAccess(
                            appendantParameter, typeof(TEntity).GetProperty(propertyPath.First())),
                        propertyPath.Skip(1).ToList());

                if (entityProperty.Type != typeof(string))
                {
                    // currently, always cast to double for temporary situation to support different number types
                    entityProperty =
                        Expression.Call(
                            LambdaExpressionGenerator.GetSqlFunctionStringConvertMethod(typeof(double?)),
                            Expression.Convert(entityProperty, typeof(double?)));
                }

                expression = Expression.Call(entityProperty, GetStringContainsMethod(), Expression.Constant(term));
            }

            return expression;
        }

        public static Expression WhereForInnerCollectionQuickSearch(
            Expression member, List<string> propertyPath, string term)
        {
            Type collectionType = member.Type.GetGenericArguments().First();

            ParameterExpression parameter = Expression.Parameter(collectionType, "collectionEntity");

            Expression entityProperty =
                LambdaExpressionGenerator.AssembleProperty(
                    Expression.MakeMemberAccess(parameter, collectionType.GetProperty(propertyPath.First())),
                    propertyPath.Skip(1).ToList());

            Expression expressionBody = Expression.Call(
                entityProperty, GetStringContainsMethod(), Expression.Constant(term));

            expressionBody = Expression.Call(
                typeof(Enumerable),
                "Where",
                new[] { collectionType },
                member,
                Expression.Lambda(expressionBody, new[] { parameter }));

            expressionBody = Expression.Call(typeof(Enumerable), "Any", new[] { collectionType }, expressionBody);

            return expressionBody;
        }

        public static MethodInfo GetStringContainsMethod()
        {
            return typeof(string).GetMethod("Contains", new[] { typeof(string) });
        }
        public static dynamic CreateTypeForSearchGridModel<TGridModel>(List<TGridModel> gridData, int pageCount)
        {
            return new { Data = gridData, PageCount = pageCount };
        }

        public static Expression<Func<TModel, List<TGridModel>>> SearchGridModelExpressionWrapper<TModel, TGridModel>(
            TModel model, List<TGridModel> gridData)
        {
            return GenerateSearchGridModelExpression<TModel, TGridModel>(gridData);
        }

        public static Expression<Func<TModel, List<TGridModel>>> GenerateSearchGridModelExpression<TModel, TGridModel>(
            List<TGridModel> gridData)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TModel), "model");

            MemberExpression expressionBody = Expression.Property(parameter, "Data");

            return Expression.Lambda<Func<TModel, List<TGridModel>>>(
                expressionBody, new[] { parameter });
        }

        public static Expression<Func<TEntity, long>> GenerateOrderByExpression<TEntity>()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            List<PropertyInfo> properties =
                typeof(TEntity)
                    .GetProperties()
                    .ToList();

            string propertyName = string.Empty;
            propertyName = properties.Any(w => w.Name.Contains("Seq"))
                               ? properties.First(w => w.Name.Contains("Seq")).Name
                               : properties.First().Name;

            Expression expressionBody = Expression.Convert(Expression.Property(parameter, propertyName), typeof(long));
            return Expression.Lambda<Func<TEntity, long>>(expressionBody, new[] { parameter });
        }

        public static Expression<Func<TModel, TProperty>> GenerateHeaderExpression<TModel, TProperty>(
            PropertyInfo property)
        {
            return GenerateExpression<TModel, TProperty>(property, null);
        }

        public static Expression<Func<TModel, TProperty>> GenerateBodyExpression<TModel, TProperty>(
            PropertyInfo property, int? rowIndex)
        {
            return GenerateExpression<TModel, TProperty>(property, rowIndex.HasValue ? rowIndex.Value : 0);
        }

        public static Expression<Func<TModel, TProperty>> GenerateExpression<TModel, TProperty>(PropertyInfo property, int? rowIndex)
        {
            ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");
            var dataListExpr = Expression.Property(fieldName, "Data");
            var itemExpr = rowIndex.HasValue ? (Expression)Expression.Property(dataListExpr, "Item", Expression.Constant(rowIndex.Value)) :
                Expression.Property(dataListExpr, "Item");
            Expression propertyExpr = Expression.Property(itemExpr, property.Name);

            return Expression.Lambda<Func<TModel, TProperty>>(propertyExpr, fieldName);
        }

        public static Expression<Func<TEntity, TModel>> GetSelectClause<TEntity, TModel>()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            List<PropertyInfo> properties = ObjectInspector.ExtractGridModelSelectProperties<TModel>();
            MemberBinding[] modelMembers =
                AssembleSelectMembers<TEntity, TModel>(properties, new MemberBinding[] { }.ToList(), parameter);

            NewExpression newModelItem = Expression.New(typeof(TModel));
            MemberInitExpression init = Expression.MemberInit(newModelItem, modelMembers);

            return (Expression<Func<TEntity, TModel>>)Expression.Lambda(init, parameter);
        }

        // recursive method
        public static MemberBinding[] AssembleSelectMembers<TEntity, TModel>(
            List<PropertyInfo> modelProperties, List<MemberBinding> memberBindings, ParameterExpression parameter)
        {
            if (modelProperties.Count > 0)
            {
                PropertyInfo property = modelProperties.First();
                modelProperties.RemoveAt(0);

                List<string> entityPropertyPath = ObjectInspector.RetrieveGridEntityPropertyPath(property);

                // initiate recursive method
                Expression entityProperty =
                    AssembleProperty(
                        Expression.MakeMemberAccess(parameter, typeof(TEntity).GetProperty(entityPropertyPath.First())),
                        entityPropertyPath.Skip(1).ToList());

                MemberAssignment modelPropertyBind = Expression.Bind(
                    typeof(TModel).GetProperty(property.Name), entityProperty);
                memberBindings.Add(modelPropertyBind);

                return AssembleSelectMembers<TEntity, TModel>(modelProperties, memberBindings, parameter);
            }

            return memberBindings.ToArray();
        }

        // recursive method
        public static Expression AssembleProperty(Expression member, List<string> propertyPath)
        {
            if (propertyPath.Count > 0)
            {
                if (member.Type.Name.Contains("Collection"))
                {
                    member = SelectInnerCollection(member, propertyPath);
                    return member;
                }

                string property = propertyPath.First();
                propertyPath.RemoveAt(0);

                MemberExpression entityMember = Expression.MakeMemberAccess(member, member.Type.GetProperty(property));

                return AssembleProperty(entityMember, propertyPath);
            }

            if (member.Type == typeof(DateTime) || member.Type == typeof(DateTime?))
            {
                member = TransformDateString(member);
            }
            else if (member.Type == typeof(TimeSpan) || member.Type == typeof(TimeSpan?))
            {
                member = TransformTimeString(member);
            }

            return member;
        }

        public static Expression SelectInnerCollection(Expression member, List<string> propertyPath)
        {
            Type collectionType = member.Type.GetGenericArguments().First();

            ParameterExpression parameter = Expression.Parameter(collectionType, "collectionEntity");

            Expression entityProperty =
                AssembleProperty(
                    Expression.MakeMemberAccess(parameter, collectionType.GetProperty(propertyPath.First())),
                    propertyPath.Skip(1).ToList());

            Expression innerSelectExpression = Expression.Lambda(
                entityProperty, new[] { parameter });

            return Expression.Call(
                typeof(Enumerable),
                "Select",
                new[] { collectionType, typeof(string) },
                member,
                innerSelectExpression);
        }

        public static Expression TransformDateString(Expression entityProperty)
        {
            UnaryExpression nullableEntityDate = Expression.Convert(entityProperty, typeof(DateTime?));

            MethodInfo concatStringMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

            MethodInfo dateNameMethod = typeof(SqlFunctions).GetMethod(
                "DateName", new[] { typeof(string), typeof(DateTime?) });

            var datePart = Expression.Call(dateNameMethod, Expression.Constant("dd"), nullableEntityDate);
            Expression transformPredicate = Expression.Add(datePart, Expression.Constant("/"), concatStringMethod);

            MethodInfo datePartMethod = typeof(SqlFunctions).GetMethod(
                "DatePart", new[] { typeof(string), typeof(DateTime?) });

            MethodInfo stringConvertMethod = GetSqlFunctionStringConvertMethod(typeof(double?));

            // original month from 'MM' gives the name of the month. Therefore, do SqlFunctions.StringConvert((double)'MM') 
            // to extract digits
            datePart = Expression.Call(
                stringConvertMethod,
                Expression.Convert(Expression.Call(datePartMethod, Expression.Constant("MM"), nullableEntityDate), typeof(Nullable<double>)));

            transformPredicate = Expression.Add(transformPredicate, datePart, concatStringMethod);
            transformPredicate = Expression.Add(transformPredicate, Expression.Constant("/"), concatStringMethod);
            datePart = Expression.Call(dateNameMethod, Expression.Constant("yyyy"), nullableEntityDate);
            transformPredicate = Expression.Add(transformPredicate, datePart, concatStringMethod);

            return transformPredicate;
        }

        public static Expression TransformTimeString(Expression entityProperty)
        {
            UnaryExpression nullableEntityTime = Expression.Convert(entityProperty, typeof(TimeSpan?));

            MethodInfo concatStringMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

            MethodInfo dateNameMethod = typeof(SqlFunctions).GetMethod(
                "DateName", new[] { typeof(string), typeof(TimeSpan?) });

            var datePart = Expression.Call(dateNameMethod, Expression.Constant("hh"), nullableEntityTime);
            Expression transformPredicate = Expression.Add(datePart, Expression.Constant(":"), concatStringMethod);
            datePart = Expression.Call(dateNameMethod, Expression.Constant("mi"), nullableEntityTime);
            transformPredicate = Expression.Add(transformPredicate, datePart, concatStringMethod);
            transformPredicate = Expression.Add(transformPredicate, Expression.Constant(":"), concatStringMethod);
            datePart = Expression.Call(dateNameMethod, Expression.Constant("ss"), nullableEntityTime);
            transformPredicate = Expression.Add(transformPredicate, datePart, concatStringMethod);

            return transformPredicate;
        }

        public static MethodInfo GetSqlFunctionStringConvertMethod(Type argumentType)
        {
            return typeof(SqlFunctions).GetMethod("StringConvert", new[] { argumentType });
        }
    }
}