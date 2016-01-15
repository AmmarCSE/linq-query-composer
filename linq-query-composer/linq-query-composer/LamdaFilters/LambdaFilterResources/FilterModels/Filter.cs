using Travel.Agency.RazorGrid.LambdaFilters.FilterData;
using Travel.Agency.RazorGrid.LambdaFilters.FilterData.LambdaHelper;
using Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Travel.Agency.EntityFramework;

namespace Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels
{
    public class Filter : IFilter
    {
        public string MainSetKey { get; set; }
        public string FilterTitle { get; set; }
        public string FilterType { get; set; }

        public dynamic ConstructReturnObject()
        {
            return 
                new {
                    FilterTitle = this.FilterTitle
                    , FilterType = this.FilterType
                    , FilterSearchKey = this.MainSetKey
                };
        }
    }

    public class Filter<TMainSet, TKey> : BaseDataFilter
        where TMainSet : class
    {
        public string MainSetKey { get; set; }
        public string MainSetDisplayProperty { get; set; }

        public override IQueryable GetSourceQueryable(List<FilterSearchItem> searchItems)
        {
            return 
                FilterDataHelper
                    .GetRawQueryable<TMainSet>(
                        searchItems
                        , false
                        , null
                     );
        }

        public override void SetFilterDataForFilter(IQueryable queryableSource
            , List<FilterSearchItem> searchItems)
        {
            FilterItems =
                new FilterDataRetriever()
                    .GetFilterDataForFilter(queryableSource, this, searchItems);
        }

        public override string GetFilterSearchKey()
        {
            return MainSetKey;
        }
    }
    public class Filter<TMainSet, TFilterSet, TKeyType> : BaseDataFilter
            where TMainSet : class where TFilterSet : class 
    {
        public string MainSetKey { get; set; }
        public string FilterSetKey { get; set; }
        public string FilterSetDisplayProperty { get; set; }

        public override IQueryable GetSourceQueryable(List<FilterSearchItem> searchItems)
        {
            return 
                FilterDataHelper
                    .GetRawQueryable<TMainSet>(
                        searchItems
                        , false
                        , null
                     );
        }

        public override void SetFilterDataForFilter(IQueryable queryableSource
            , List<FilterSearchItem> searchItems)
        {
            FilterItems =
                new FilterDataRetriever()
                    .GetFilterDataForFilter(queryableSource, this, searchItems);
        }

        public override string GetFilterSearchKey()
        {
            return MainSetKey;
        }
    }

    public class Filter<TMainSet, TJunctionSet, TFilterSet, TKeyType> : BaseDataFilter
            where TMainSet : class where TJunctionSet : class where TFilterSet : class 
    {
        public string MainSetKey { get; set; }
        public string JunctionSetLeftKey { get; set; }
        public string JunctionSetRightKey { get; set; }
        public string FilterSetKey { get; set; }
        public string FilterSetDisplayProperty { get; set; }

        public override IQueryable GetSourceQueryable(List<FilterSearchItem> searchItems)
        {
            return 
                FilterDataHelper
                    .GetRawQueryable<TMainSet>(
                        searchItems
                        , false
                        , null
                     );
        }

        public override void SetFilterDataForFilter(IQueryable queryableSource
            , List<FilterSearchItem> searchItems)
        {
            FilterItems =
                new FilterDataRetriever()
                    .GetFilterDataForFilter(queryableSource, this, searchItems);
        }

        public override string GetFilterSearchKey()
        {
            return MainSetKey;
        }
    }

    public class Filter<TParentSet, TChildSet, TJunctionSet, TFilterSet, TKeyType> : BaseDataFilter
            where TParentSet : class where TChildSet : class where TJunctionSet : class where TFilterSet : class 
    {
        public string MainSetKey { get; set; }
        public string ChildSetLeftKey { get; set; }
        public string ChildSetRightKey { get; set; }
        public string JunctionSetLeftKey { get; set; }
        public string JunctionSetRightKey { get; set; }
        public string FilterSetKey { get; set; }
        public string FilterSetDisplayProperty { get; set; }
        public string ChildSetPropertyName { get; set; }

        public override IQueryable GetSourceQueryable(List<FilterSearchItem> searchItems)
        {
            return 
                FilterDataHelper
                    .GetRawQueryable<TParentSet>(
                        searchItems
                        , false
                        , null
                     );
        }

        public override void SetFilterDataForFilter(IQueryable queryableSource
            , List<FilterSearchItem> searchItems)
        {
            FilterItems =
                new FilterDataRetriever()
                    .GetFilterDataForFilter(queryableSource, this, searchItems);
        }

        public override string GetFilterSearchKey()
        {
            return ChildSetPropertyName + "." + ChildSetRightKey;
        }
    }

    public class CommonFilter<TKeyType> : BaseDataFilter
    {
        public string CommonSetKey { get; set; }
        public string CommonSetDisplayProperty { get; set; }
        public string MainSetOnePropertyPath { get; set; }
        public string MainSetTwoPropertyPath { get; set; }

        public override void SetFilterDataForFilter(
            IFilterDataRetriever dataRetriever
            , List<FilterSearchItem> searchItems)
        {
            FilterItems =
                dataRetriever
                    .GetFilterDataForFilter(
                        this
                        , searchItems
                    );
        }

        public override string GetFilterSearchKey()
        {
            return CommonSetKey;
        }
    }
}
