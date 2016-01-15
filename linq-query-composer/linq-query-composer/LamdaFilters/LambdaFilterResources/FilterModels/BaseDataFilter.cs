using Travel.Agency.RazorGrid.LambdaFilters.FilterData;
using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels
{
    public abstract class BaseDataFilter : IFilter
    {
        public string FilterTitle { get; set; }
        public string FilterType { get; set; }
        public dynamic FilterItems { get; set; } //Actually a List<ResultItem<TKey>>

        public virtual IQueryable GetSourceQueryable(List<FilterSearchItem> searchItems)
        {
            return null;
        }

        public virtual void SetFilterDataForFilter(
            IQueryable queryableSource
            , List<FilterSearchItem> searchItems) { }

        public virtual void SetFilterDataForFilter(
            IFilterDataRetriever dataRetriever
            ,List<FilterSearchItem> searchItems) { }

        abstract public string GetFilterSearchKey();

        public dynamic ConstructReturnObject()
        {
            return 
                new {
                    FilterTitle = this.FilterTitle
                    , FilterType = this.FilterType
                    , FilterItems = this.FilterItems
                    , FilterSearchKey = this.GetFilterSearchKey()
                };
        }
    }
}
