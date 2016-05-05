linq-query-composer
====================================================
linq-query composer is a LINQ utility module which generates LINQ expressions on the fly by inspecting the injected model. If you are working on a tight project with inexperienced LINQ developers and cannot afford performance penalties such as

```
myDbSet
    .ToList()//should not be executing against the database at this point!!
    .Where(i => i.Name == name)
...
```

this utility is a good solution. Also, if you are building a LINQ-heavy application and want to avoid LINQ expression redundancy, this module is a good supplement.

Getting Set Up
--------------

1 - Include this library as a reference in your project.

2 - Specify your data model with desired attributes:
```
public class ReservationGridModel : DataModel<Reservation, ReservationGridModel>
{
    [Key]
    [DataEntityProperty(TargetedPropertyPath = new[] { "ReservationID" })]
    public int ReservationId { get; set; }

    [DataEntityProperty(TargetedPropertyPath = new[] { "SequanceID" })]
    public long SequanceID { get; set; }

    [DataEntityProperty(TargetedPropertyPath = new[] { "FromDate" })]
    public string strFromDate { get; set; }

    [DataEntityProperty(TargetedPropertyPath = new[] { "ToDate" })]
    public string strToDate { get; set; }

    [DataEntityProperty(TargetedPropertyPath = new[] { "Hotel", "HotelName" })]
    public string HotelName { get; set; }

    [DataEntityProperty(TargetedPropertyPath = new[] { "GuestName" })]
    public string GuestName { get; set; }

    [Key]
    [DataEntityProperty(TargetedPropertyPath = new[] { "PostToAccount" })]
    public bool? PostToAccount { get; set; }

    [DataComputedProperty]
    public string PostToAccountString
    {
        get
        {
            return this.PostToAccount.HasValue && this.PostToAccount.Value ? "Yes" : "No";
        }
    }

    [DataEntityProperty(TargetedPropertyPath = new[] { "ReservationStatus", "Name" })]
    public string ReservationStatusName { get; set; }
}

```

3 - Retrieve your data when needed like so:
```
var searchItems = new List<FilterItem>();
ReservationDataModel gridModel = new ReservationDataModel();
IQueryable queryableSource = gridModel.GetRawData(searchItems);

int PageCount, pageIndex = 0;
dynamic data = gridModel.SelectDataForDataModel(
    queryableSource, searchItems, pageIndex, out PageCount);
```
ToDo
--------------
Provide proper documentation

Re-arrange inner modules and re-architect by de-compression 

Format and place comments in code.
