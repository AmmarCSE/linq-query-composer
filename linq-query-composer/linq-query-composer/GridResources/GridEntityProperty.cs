using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace razor_grid_lib.GridResources
{
    public class GridEntityProperty<TProperty>
    {
        public string TargetProperty { get; set; }
        public string[] TargetChildren { get; set; }
        public TProperty Value { get; set; }
    }
}
