// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaExpressionHelper.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Lambda Expression Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.LambdaFilters.FilterData.LambdaHelper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Travel.Agency.RazorGrid.Helpers;
    using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;

    public class LambdaExpressionHelper
    {
        public Expression<Func<TEntity, TKeyType>> GetJoinPredicate<TEntity, TKeyType>(string property)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            return
                Expression.Lambda<Func<TEntity, TKeyType>>(
                    Expression.Convert(Expression.PropertyOrField(parameter, property), typeof(TKeyType)), parameter);
        }

        public Expression<Func<TEntity, TKeyType>> GetNestedJoinPredicate<TEntity, TKeyType>(
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

        public Expression<Func<TFilterSet, ResultItem<TKey>>> GetSelectClause<TFilterSet, TKey>(
            string keyProperty, string valueProperty)
        {
            Type filterSetType = typeof(TFilterSet);
            Type resultItemType = typeof(ResultItem<TKey>);

            ParameterExpression parameter = Expression.Parameter(filterSetType, "filterSet");

            List<string> keyPropertyPath = keyProperty.Split('.').ToList();
            List<string> valuePropertyPath = valueProperty.Split('.').ToList();

            Expression filterSetId =
                GridLambdaExpressionHelper.AssembleProperty(
                    Expression.MakeMemberAccess(parameter, filterSetType.GetProperty(keyPropertyPath.First())),
                    keyPropertyPath.Skip(1).ToList());

            Expression filterSetValue =
                GridLambdaExpressionHelper.AssembleProperty(
                    Expression.MakeMemberAccess(parameter, filterSetType.GetProperty(valuePropertyPath.First())),
                    valuePropertyPath.Skip(1).ToList());

            MemberAssignment idBind = Expression.Bind(resultItemType.GetProperty("Id"), filterSetId);
            MemberAssignment valueBind = Expression.Bind(resultItemType.GetProperty("Value"), filterSetValue);

            NewExpression newFilterItem = Expression.New(resultItemType);
            MemberInitExpression init = Expression.MemberInit(newFilterItem, valueBind, idBind);

            return (Expression<Func<TFilterSet, ResultItem<TKey>>>)Expression.Lambda(init, parameter);
        }

        public Expression<Func<TJunctionSet, TFilterSet, ResultItem<TKey>>> GetJoinResultSelector<TJunctionSet, TFilterSet, TKey>(string keyProperty, string valueProperty)
        {
            Type junctionSetType = typeof(TJunctionSet);
            Type filterSetType = typeof(TFilterSet);
            Type resultItemType = typeof(ResultItem<TKey>);

            ParameterExpression junctionParameter = Expression.Parameter(junctionSetType, "junctionSet");
            ParameterExpression filterParameter = Expression.Parameter(filterSetType, "filterSet");

            MemberExpression junctionSetId = Expression.MakeMemberAccess(
                junctionParameter, junctionSetType.GetProperty(keyProperty));
            MemberExpression filterSetValue = Expression.MakeMemberAccess(
                filterParameter, filterSetType.GetProperty(valueProperty));

            MemberAssignment idBind = Expression.Bind(resultItemType.GetProperty("Id"), junctionSetId);
            MemberAssignment valueBind = Expression.Bind(resultItemType.GetProperty("Value"), filterSetValue);

            NewExpression newFilterItem = Expression.New(resultItemType);
            MemberInitExpression init = Expression.MemberInit(newFilterItem, valueBind, idBind);

            return
                (Expression<Func<TJunctionSet, TFilterSet, ResultItem<TKey>>>)
                Expression.Lambda(init, new[] { junctionParameter, filterParameter });
        }

        public Expression<Func<TSourceSet, TJunctionSet, ResultItem<TKey>>> GetCommonFilterSourceJoinResultSelector<TSourceSet, TJunctionSet, TKey>(string keyProperty, string valueProperty)
        {
            Type sourceSetType = typeof(TSourceSet);
            Type junctionSetType = typeof(TJunctionSet);
            Type resultItemType = typeof(ResultItem<TKey>);

            ParameterExpression sourceParameter = Expression.Parameter(sourceSetType, "sourceSet");
            ParameterExpression junctionParameter = Expression.Parameter(junctionSetType, "junctionSet");

            MemberExpression junctionSetId = Expression.MakeMemberAccess(
                junctionParameter, junctionSetType.GetProperty(keyProperty));
            MemberExpression sourceSetValue = Expression.MakeMemberAccess(
                sourceParameter, sourceSetType.GetProperty(valueProperty));

            MemberAssignment idBind = Expression.Bind(resultItemType.GetProperty("Id"), junctionSetId);
            MemberAssignment valueBind = Expression.Bind(resultItemType.GetProperty("Value"), sourceSetValue);

            NewExpression newFilterItem = Expression.New(resultItemType);
            MemberInitExpression init = Expression.MemberInit(newFilterItem, valueBind, idBind);

            return
                (Expression<Func<TSourceSet, TJunctionSet, ResultItem<TKey>>>)
                Expression.Lambda(init, new[] { sourceParameter, junctionParameter });
        }

        public Expression<Func<TSourceSet, TMainSetTwo, ResultItem<TKey>>> GetCommonFilterJoinResultSelector<TSourceSet, TMainSetTwo, TKey>(string keyProperty, string valueProperty)
        {
            Type sourceSetType = typeof(TSourceSet);
            Type mainSetTwoType = typeof(TMainSetTwo);
            Type filterItemType = typeof(ResultItem<TKey>);

            ParameterExpression sourceParameter = Expression.Parameter(sourceSetType, "sourceSet");
            ParameterExpression mainSetTwoParameter = Expression.Parameter(mainSetTwoType, "mainSetTwo");

            MemberExpression sourceSetId = Expression.MakeMemberAccess(
                sourceParameter, sourceSetType.GetProperty(keyProperty));
            MemberExpression sourceSetValue = Expression.MakeMemberAccess(
                sourceParameter, sourceSetType.GetProperty(valueProperty));

            MemberAssignment idBind = Expression.Bind(filterItemType.GetProperty("Id"), sourceSetId);
            MemberAssignment valueBind = Expression.Bind(filterItemType.GetProperty("Value"), sourceSetValue);

            NewExpression newFilterItem = Expression.New(filterItemType);
            MemberInitExpression init = Expression.MemberInit(newFilterItem, valueBind, idBind);

            return
                (Expression<Func<TSourceSet, TMainSetTwo, ResultItem<TKey>>>)
                Expression.Lambda(init, new[] { sourceParameter, mainSetTwoParameter });
        }

        public Expression<Func<TEntity, bool>> TASTemplateWhereExpression<TEntity>(bool isDefualtActive)
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

        public Expression<Func<TMainSet, bool>> GenerateWhereClause<TMainSet>(
            List<FilterSearchItem> searchItems, List<PropertyInfo> quickSearchProperties)
        {
            bool isDefualtActive = searchItems.Count == 0;

            Expression<Func<TMainSet, bool>> defaultExpression = TASTemplateWhereExpression<TMainSet>(isDefualtActive);

            Expression whereBody = defaultExpression.Body;
            ParameterExpression whereParameter = defaultExpression.Parameters[0];

            whereBody = searchItems.Aggregate(
                whereBody,
                (current, searchItem) =>
                this.GenerateSubWhereClause<TMainSet>(whereParameter, current, searchItem, quickSearchProperties));

            return Expression.Lambda<Func<TMainSet, bool>>(whereBody, whereParameter);
        }

        public Expression GenerateSubWhereClause<TMainSet>(
            ParameterExpression appendantParameter,
            Expression appendantExpression,
            FilterSearchItem searchItem,
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

        public Expression AssembleContainsExpression<TEntity, TProperty>(
            ParameterExpression appendantParameter,
            Expression appendantExpression,
            FilterSearchItem searchItem,
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
                GridLambdaExpressionHelper.AssembleProperty(
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

        public Expression AssembleQuickSearchExpression<TEntity>(
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
                    appendantExpression, this.GenerateSubQuickSearchExpression<TEntity>(appendantParameter, property, term));

                return this.AssembleQuickSearchExpression<TEntity>(
                    appendantParameter, appendedExpression, quickSearchProperties, term);
            }

            return appendantExpression;
        }

        public Expression GenerateSubQuickSearchExpression<TEntity>(
            ParameterExpression appendantParameter, PropertyInfo quickSearchProperty, string term)
        {
            Expression expression = null;
            List<string> propertyPath = GridPropertyHelper.RetrieveGridEntityPropertyPath(quickSearchProperty);

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
                    GridLambdaExpressionHelper.AssembleProperty(
                        Expression.MakeMemberAccess(
                            appendantParameter, typeof(TEntity).GetProperty(propertyPath.First())),
                        propertyPath.Skip(1).ToList());

                if (entityProperty.Type != typeof(string))
                {
                    // currently, always cast to double for temporary situation to support different number types
                    entityProperty =
                        Expression.Call(
                            GridLambdaExpressionHelper.GetSqlFunctionStringConvertMethod(typeof(double?)),
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
                GridLambdaExpressionHelper.AssembleProperty(
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
    }
}
