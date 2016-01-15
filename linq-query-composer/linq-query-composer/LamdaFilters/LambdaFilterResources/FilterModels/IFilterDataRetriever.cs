using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels
{
    public interface IFilterDataRetriever
    {
        List<ResultItem<string>> GetFilterDataForFilter(BaseDataFilter filter, List<FilterSearchItem> searchItems);
    }

}
