namespace Travel.Agency.RazorGrid.LambdaFilters.FilterAssembler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Travel.Agency.RazorGrid.GridResources;
    using Travel.Agency.RazorGrid.Helpers;
    using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
    using Travel.Agency.RazorGrid.SearchResources;

    public class SearchAssembler
    {
        public Dictionary<string, SearchStructure> AssembleSearch(HtmlHelper htmlHelper
            , ISearchModel searchModel, List<FilterSearchItem> searchItems, int pageIndex)
        {
            searchItems = searchItems ?? new List<FilterSearchItem>();

            IQueryable queryableSource = searchModel.GridModel.GetRawGridData(searchItems);

            int PageCount;
            dynamic gridData = searchModel.GridModel.SelectGridDataForGridModel(
                queryableSource, searchItems, pageIndex, out PageCount);

            SetFilterData(
                searchModel.Filters
                , queryableSource
                , searchItems
            );

            var searchGridModel = GridLambdaExpressionHelper.CreateTypeForSearchGridModel(
                gridData, PageCount);

            string grid = 
                GetRazorGrid(
                    htmlHelper
                    , searchGridModel
                    , gridData
                    ,searchModel.GridPermissions
                );

            dynamic searchStructure = this.AssembleSearchStructure(
                searchModel.SearchModelName, grid, searchModel.Filters, PageCount);

            return searchStructure;
        }

        public string AssembleSearchForPostBack(
            HtmlHelper htmlHelper, ISearchModel searchModel, List<FilterSearchItem> searchItems, int pageIndex)
        {
            Dictionary<string, SearchStructure> searchStructure = this.AssembleSearch(htmlHelper, searchModel, searchItems, pageIndex);

            return this.SerializeObject(searchStructure);
        }

        public MvcHtmlString AssembleSearchForRazor<TSearchModel>(
            HtmlHelper<TSearchModel> htmlHelper, ISearchModel searchModel, List<FilterSearchItem> searchItems, int pageIndex = 0)
        {
            Dictionary<string, SearchStructure> searchStructure = this.AssembleSearch(htmlHelper, searchModel, searchItems, pageIndex);

            dynamic searchStructureForRazor = AssembleSearchStructureForRazor(
                searchStructure, searchModel.GridPermissions, htmlHelper);

            return this.BuildRazorScriptBlock(searchStructureForRazor);
        }

        public Dictionary<string, SearchStructure> AssembleSearchStructure(
            string searchModelName, string gridHtml, List<IFilter> filters, int pageCount)
        {
            return new Dictionary<string, SearchStructure>
                       {
                           {
                               searchModelName,
                               new SearchStructure
                                   {
                                       GridHtml = gridHtml,
                                       Filters =
                                           filters.Select(
                                               s =>
                                               s.ConstructReturnObject()),
                                       PageCount = pageCount
                                   }
                           }
                       };
        }

        public dynamic AssembleSearchStructureForRazor<TSearchModel>(
            Dictionary<string, SearchStructure> searchStructure
            , List<GridEnums.GridPermission> gridPermissions
            , HtmlHelper<TSearchModel> htmlHelper)
        {
            string searchModelName = searchStructure.First().Key;

            dynamic searchStructureObject = searchStructure.First().Value;

            string filters = this.SerializeObject(searchStructureObject.Filters);

            return new { searchModelName, gridHtml = searchStructureObject.GridHtml, serializedFilters = filters };
        }

        public string SerializeObject(dynamic searchStructure)
        {
            return new JavaScriptSerializer().Serialize(searchStructure);
        }

        public MvcHtmlString BuildRazorScriptBlock(dynamic searchStructureForRazor)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(searchStructureForRazor.gridHtml);
            sb.Append("<script>");
            sb.Append("var searchModelName = '");
            sb.Append(searchStructureForRazor.searchModelName);
            sb.Append("';");
            sb.Append("</script>");

            string serializedFilters =
                new FilterAssembler()
                    .BuildRazorScriptBlock(searchStructureForRazor.serializedFilters, "filters")
                    .ToString();

            sb.Append(serializedFilters);

            return MvcHtmlString.Create(sb.ToString());
        }

        private string GetRazorGrid(
            HtmlHelper htmlHelper
            , dynamic searchGridModel
            , dynamic data
            , List<GridEnums.GridPermission> gridPermissions)
        {
            return
                GridExtensions
                    .GridFor(
                        GridHtmlHelperRetriever
                            .RetrieveGridHtmlHelper(
                                htmlHelper
                                , searchGridModel
                            )
                        , GridLambdaExpressionHelper
                            .SearchGridModelExpressionWrapper(
                                searchGridModel
                                , data
                            )
                        , gridPermissions
                    )
                    .ToString();
        }

        private void SetFilterData(
            IList<IFilter> filters
            , IQueryable queryableSource
            , List<FilterSearchItem> searchItems)
        {
            foreach (var filter in filters.Where(w => w is BaseDataFilter))
            {
                if (filter.FilterType.StartsWith("checkbox"))
                {
                    ((BaseDataFilter)filter).SetFilterDataForFilter(queryableSource, searchItems);
                }
            }
        }
    }
}
