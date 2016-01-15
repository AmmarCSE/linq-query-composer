using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels
{
    public class ResultItem<TKey>
    {
        public TKey Id { get; set; }
        public dynamic Value { get; set; }
    }
}
