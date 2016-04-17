// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterDataHelper.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Filter Data Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linq.Query.Composer.LambdaFilters.FilterData
{
    using DBContext;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Linq.Query.Composer.LambdaFilters.FilterData.LambdaHelper;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    /// <summary>
    /// The filter data helper.
    /// </summary>
    public static class FilterDataHelper
    {

        /// <summary>
        /// The get raw queryable.
        /// </summary>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <param name="isCommon">
        /// The is common.
        /// </param>
        /// <param name="quickSearchProperties">
        /// The quick search properties.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<TEntity> GetRawQueryable<TEntity>(
            List<FilterSearchItem> searchItems, bool isCommon, List<PropertyInfo> quickSearchProperties)
            where TEntity : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();
            TAS_DevEntities1 dbContext = new TAS_DevEntities1();
            IQueryable<TEntity> queryable = dbContext.Set<TEntity>();

            if (!isCommon)
            {
                queryable =
                    queryable.Where(expressionHelper.GenerateWhereClause<TEntity>(searchItems, quickSearchProperties));
            }

            return queryable;
        }
    }
}