// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridLambdaExpressionHelper.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Lambda Expression Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.SqlServer;
    //using System.Data.Objects.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /*// Summary:
    //     Represents support for the HTML label element in an ASP.NET MVC view.*/

    /// <summary>
    /// The grid lambda expression helper.
    /// </summary>
    public static class GridLambdaExpressionHelper
    {
        /// <summary>
        /// The create type for search grid model.
        /// </summary>
        /// <param name="gridData">
        /// The grid data.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The dynamic.
        /// </returns>
        public static dynamic CreateTypeForSearchGridModel<TGridModel>(List<TGridModel> gridData, int pageCount)
        {
            return new { Data = gridData, PageCount = pageCount };
        }

        /// <summary>
        /// The search grid model expression wrapper.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="gridData">
        /// The grid data.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TModel, List<TGridModel>>> SearchGridModelExpressionWrapper<TModel, TGridModel>(
            TModel model, List<TGridModel> gridData)
        {
            return GenerateSearchGridModelExpression<TModel, TGridModel>(gridData);
        }

        /// <summary>
        /// The generate search grid model expression.
        /// </summary>
        /// <param name="gridData">
        /// The grid data.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TModel, List<TGridModel>>> GenerateSearchGridModelExpression<TModel, TGridModel>(
            List<TGridModel> gridData)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TModel), "model");

            MemberExpression expressionBody = Expression.Property(parameter, "Data");

            return Expression.Lambda<Func<TModel, List<TGridModel>>>(
                expressionBody, new[] { parameter });
        }

        /// <summary>
        /// The generate order by expression.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
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

        /// <summary>
        /// The generate header expression.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TModel, TProperty>> GenerateHeaderExpression<TModel, TProperty>(
            PropertyInfo property)
        {
            return GenerateExpression<TModel, TProperty>(property, null);
        }

        /// <summary>
        /// The generate body expression.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TModel, TProperty>> GenerateBodyExpression<TModel, TProperty>(
            PropertyInfo property, int? rowIndex)
        {
            return GenerateExpression<TModel, TProperty>(property, rowIndex.HasValue ? rowIndex.Value : 0);
        }

        /// <summary>
        /// The generate expression.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TModel, TProperty>> GenerateExpression<TModel, TProperty>(PropertyInfo property, int? rowIndex)
        {
            ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");
            var dataListExpr = Expression.Property(fieldName, "Data");
            var itemExpr = rowIndex.HasValue ? (Expression)Expression.Property(dataListExpr, "Item", Expression.Constant(rowIndex.Value)) :
                Expression.Property(dataListExpr, "Item");
            Expression propertyExpr = Expression.Property(itemExpr, property.Name);

            return Expression.Lambda<Func<TModel, TProperty>>(propertyExpr, fieldName);
        }

        /// <summary>
        /// The get select clause.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TEntity, TModel>> GetSelectClause<TEntity, TModel>()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");

            List<PropertyInfo> properties = GridPropertyHelper.ExtractGridModelSelectProperties<TModel>();
            MemberBinding[] modelMembers =
                AssembleSelectMembers<TEntity, TModel>(properties, new MemberBinding[] { }.ToList(), parameter);

            NewExpression newModelItem = Expression.New(typeof(TModel));
            MemberInitExpression init = Expression.MemberInit(newModelItem, modelMembers);

            return (Expression<Func<TEntity, TModel>>)Expression.Lambda(init, parameter);
        }

        // recursive method

        /// <summary>
        /// The assemble select members.
        /// </summary>
        /// <param name="modelProperties">
        /// The model properties.
        /// </param>
        /// <param name="memberBindings">
        /// The member bindings.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The MemberBinding[].
        /// </returns>
        public static MemberBinding[] AssembleSelectMembers<TEntity, TModel>(
            List<PropertyInfo> modelProperties, List<MemberBinding> memberBindings, ParameterExpression parameter)
        {
            if (modelProperties.Count > 0)
            {
                PropertyInfo property = modelProperties.First();
                modelProperties.RemoveAt(0);

                List<string> entityPropertyPath = GridPropertyHelper.RetrieveGridEntityPropertyPath(property);

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

        /// <summary>
        /// The assemble property.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <param name="propertyPath">
        /// The property path.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
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

        /// <summary>
        /// The select inner collection.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <param name="propertyPath">
        /// The property path.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
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

        /// <summary>
        /// The transform date string.
        /// </summary>
        /// <param name="entityProperty">
        /// The entity property.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
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

        /// <summary>
        /// The transform time string.
        /// </summary>
        /// <param name="entityProperty">
        /// The entity property.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
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

        /// <summary>
        /// The get sql function string convert method.
        /// </summary>
        /// <param name="argumentType">
        /// The argument type.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/>.
        /// </returns>
        public static MethodInfo GetSqlFunctionStringConvertMethod(Type argumentType)
        {
            return typeof(SqlFunctions).GetMethod("StringConvert", new[] { argumentType });
        }
    }
}