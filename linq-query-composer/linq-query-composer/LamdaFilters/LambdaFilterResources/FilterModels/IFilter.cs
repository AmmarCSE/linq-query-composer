using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels
{
    public interface IFilter
    {
        string FilterTitle { get; set; }
        string FilterType { get; set; }

        dynamic ConstructReturnObject(); 
    }

}
