using Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels;
using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Travel.Agency.EntityFramework;

namespace Travel.Agency.RazorGrid.LambdaFilters.FilterAssembler
{
    public class FilterAssembler
    {
        public List<IFilter> AssembleFiltersForModel(
            List<IFilter> filters
            , List<FilterSearchItem> searchItems
            , IFilterDataRetriever dataRetriever = null)
        {
            foreach (var filter in filters.Where(w => typeof(BaseDataFilter).IsAssignableFrom(w.GetType())))
            {
                if (filter.FilterType == "checkbox")
                {
                    BaseDataFilter currentFilter =(BaseDataFilter)filter;//to avoid casting twice below 

                    if (dataRetriever == null)
                    {
                        currentFilter.SetFilterDataForFilter(currentFilter.GetSourceQueryable(searchItems), searchItems);
                    }
                    else
                    {
                        currentFilter.SetFilterDataForFilter(dataRetriever, searchItems);
                    }
                }
            }

            return filters;
        }

        public List<dynamic> ConstructReturnObjects(List<IFilter> filterResults)
        {
            List<dynamic> filterReturnObjects = new List<dynamic>();

            foreach (var filter in filterResults)
            {
                filterReturnObjects.Add(filter.ConstructReturnObject());
            }

            return filterReturnObjects;
        }

        public string SerializeFilters(List<IFilter> filterResults)
        {
            return 
                new JavaScriptSerializer()
                    .Serialize(ConstructReturnObjects(filterResults));
        }

        public MvcHtmlString BuildRazorScriptBlock(dynamic serializedFilters, string filterVariableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<script>");
            sb.Append("var " + filterVariableName + " = ");
            sb.Append(serializedFilters);
            sb.Append(";");
            sb.Append("</script>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}