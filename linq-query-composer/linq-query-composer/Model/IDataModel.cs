namespace Linq.Query.Composer.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDataModel 
    {
        dynamic GetRawGridData(List<FilterItem> searchItems);
        dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterItem> searchItems, int pageIndex, out int pageCount);
    }
}
