using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels
{
    public interface IOperatorFilter
    {
        Expression ConstructOperatorFilterExpression(); 
    }

}
