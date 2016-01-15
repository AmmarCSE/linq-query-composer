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
using System.Linq.Expressions;

namespace Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels
{
    public class OperatorFilter : IOperatorFilter
    {
        public Expression ConstructOperatorFilterExpression()
        {
            return null;
        }
    }

    public class OperatorFilter<TOperatorOwnerSet, TKey> : IOperatorFilter
        where TOperatorOwnerSet : class
    {
        public Expression ConstructOperatorFilterExpression()
        {
            return null;
        }
    }

    public class OperatorFilter<TMainSet, TOperatorOwnerSet, TKeyType> : IOperatorFilter
            where TMainSet : class where TOperatorOwnerSet : class 
    {
        public Expression ConstructOperatorFilterExpression()
        {
            return null;
        }
    }
}
