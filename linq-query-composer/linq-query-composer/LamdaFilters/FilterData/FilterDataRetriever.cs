// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterDataRetriever.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Filter Data Retriever.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.LambdaFilters.FilterData
{
    using Travel.Agency.EntityFramework;
    using System.Collections.Generic;
    using System.Linq;
    using Travel.Agency.EntityFramework;
    using Travel.Agency.RazorGrid.LambdaFilters.FilterData.LambdaHelper;
    using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;

    /// <summary>
    /// The filter data retriever.
    /// </summary>
    public class FilterDataRetriever
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private TAS_DevEntities dbContext = ContextHelper.GetContext();

        
        
        
        
        
                                                                                                                        public List<ResultItem<TKey>> GetFilterDataForFilter<TMainSet, TKey>(
            IQueryable queryableSource, Filter<TMainSet, TKey> filter, List<FilterSearchItem> searchItems)
            where TMainSet : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();

            return
                ((IQueryable<TMainSet>)queryableSource).Select(
                    expressionHelper.GetSelectClause<TMainSet, TKey>(filter.MainSetKey, filter.MainSetDisplayProperty))
                                                       .Distinct()
                                                       .OrderBy(order => order.Value)
                                                       .ToList();
        }

        /// <summary>
        /// The get filter data for filter.
        /// </summary>
        /// <param name="queryableSource">
        /// The queryable source.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <typeparam name="TMainSet">
        /// </typeparam>
        /// <typeparam name="TFilterSet">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public List<ResultItem<TKey>> GetFilterDataForFilter<TMainSet, TFilterSet, TKey>(
            IQueryable queryableSource, Filter<TMainSet, TFilterSet, TKey> filter, List<FilterSearchItem> searchItems)
            where TMainSet : class
            where TFilterSet : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();

            return
                ((IQueryable<TMainSet>)queryableSource).Join(
                    this.dbContext.Set<TFilterSet>(),
                    expressionHelper.GetJoinPredicate<TMainSet, TKey>(filter.MainSetKey),
                    expressionHelper.GetJoinPredicate<TFilterSet, TKey>(filter.FilterSetKey),
                    (m, f) => f)
                                                       .Select(
                                                           expressionHelper.GetSelectClause<TFilterSet, TKey>(
                                                               filter.FilterSetKey, filter.FilterSetDisplayProperty))
                                                       .Distinct()
                                                       .OrderBy(order => order.Value)
                                                       .ToList();
        }

        /// <summary>
        /// The get filter data for filter.
        /// </summary>
        /// <param name="queryableSource">
        /// The queryable source.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <typeparam name="TMainSet">
        /// </typeparam>
        /// <typeparam name="TJunctionSet">
        /// </typeparam>
        /// <typeparam name="TFilterSet">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ResultItem<string>> GetFilterDataForFilter<TMainSet, TJunctionSet, TFilterSet, TKey>(
            IQueryable queryableSource,
            Filter<TMainSet, TJunctionSet, TFilterSet, TKey> filter,
            List<FilterSearchItem> searchItems)
            where TMainSet : class
            where TJunctionSet : class
            where TFilterSet : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();

            return
                ((IQueryable<TMainSet>)queryableSource).Join(
                    this.dbContext.Set<TJunctionSet>(),
                    expressionHelper.GetJoinPredicate<TMainSet, TKey>(filter.MainSetKey),
                    expressionHelper.GetJoinPredicate<TJunctionSet, TKey>(filter.JunctionSetLeftKey),
                    (m, j) => j)
                                                       .Join(
                                                           this.dbContext.Set<TFilterSet>(),
                                                           expressionHelper.GetJoinPredicate<TJunctionSet, TKey>(
                                                               filter.JunctionSetRightKey),
                                                           expressionHelper.GetJoinPredicate<TFilterSet, TKey>(
                                                               filter.FilterSetKey),
                                                           expressionHelper
                                                               .GetJoinResultSelector<TJunctionSet, TFilterSet, TKey>(
                                                                   filter.JunctionSetLeftKey,
                                                                   filter.FilterSetDisplayProperty))
                                                       .Distinct()
                                                       .AsEnumerable()
                                                       .GroupBy(njrt => njrt.Value)
                                                       .Select(
                                                           njrt =>
                                                           new ResultItem<string>
                                                               {
                                                                   Id =
                                                                       string.Join(
                                                                           ",",
                                                                           njrt.Where(
                                                                               injrt =>
                                                                               injrt.Value
                                                                               == njrt.First().Value)
                                                                               .Select(
                                                                                   injrt => injrt.Id)),
                                                                   Value = njrt.First().Value
                                                               })
                                                               .OrderBy(order => order.Value)
                                                       .ToList();
        }

        /// <summary>
        /// The get filter data for filter.
        /// </summary>
        /// <param name="queryableSource">
        /// The queryable source.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <typeparam name="TParentSet">
        /// </typeparam>
        /// <typeparam name="TMainSet">
        /// </typeparam>
        /// <typeparam name="TJunctionSet">
        /// </typeparam>
        /// <typeparam name="TFilterSet">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public List<ResultItem<string>> GetFilterDataForFilter<TParentSet, TMainSet, TJunctionSet, TFilterSet, TKey>(
            IQueryable queryableSource,
            Filter<TParentSet, TMainSet, TJunctionSet, TFilterSet, TKey> filter,
            List<FilterSearchItem> searchItems)
            where TParentSet : class
            where TMainSet : class
            where TJunctionSet : class
            where TFilterSet : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();

            return
                ((IQueryable<TParentSet>)queryableSource).Join(
                    this.dbContext.Set<TMainSet>()
                             .Where(expressionHelper.TASTemplateWhereExpression<TMainSet>(searchItems.Count == 0)),
                    expressionHelper.GetJoinPredicate<TParentSet, TKey>(filter.MainSetKey),
                    expressionHelper.GetJoinPredicate<TMainSet, TKey>(filter.ChildSetLeftKey),
                    (p, m) => m)
                                                         .Join(
                                                             this.dbContext.Set<TJunctionSet>(),
                                                             expressionHelper.GetJoinPredicate<TMainSet, TKey>(
                                                                 filter.ChildSetRightKey),
                                                             expressionHelper.GetJoinPredicate<TJunctionSet, TKey>(
                                                                 filter.JunctionSetLeftKey),
                                                             (m, j) => j)
                                                         .Join(
                                                             dbContext.Set<TFilterSet>(),
                                                             expressionHelper.GetJoinPredicate<TJunctionSet, TKey>(
                                                                 filter.JunctionSetRightKey),
                                                             expressionHelper.GetJoinPredicate<TFilterSet, TKey>(
                                                                 filter.FilterSetKey),
                                                             expressionHelper
                                                                 .GetJoinResultSelector<TJunctionSet, TFilterSet, TKey>(
                                                                     filter.JunctionSetLeftKey,
                                                                     filter.FilterSetDisplayProperty))
                                                         .Distinct()
                                                         .AsEnumerable()
                                                         .GroupBy(njrt => njrt.Value)
                                                         .Select(
                                                             njrt =>
                                                             new ResultItem<string>
                                                                 {
                                                                     Id =
                                                                         string.Join(
                                                                             ",",
                                                                             njrt.Where(
                                                                                 injrt =>
                                                                                 injrt.Value
                                                                                 == njrt.First()
                                                                                        .Value)
                                                                                 .Select(
                                                                                     injrt =>
                                                                                     injrt.Id)),
                                                                     Value = njrt.First().Value
                                                                 })
                                                                 .OrderBy(order => order.Value)
                                                         .ToList();
        }
    }
}