namespace Linq.Query.Composer.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDataModel 
    {
        dynamic GetRawData(List<FilterItem> searchItems);
        dynamic SelectDataForDataModel(
            IQueryable queryable, List<FilterItem> searchItems, int pageIndex, out int pageCount);
    }
}
