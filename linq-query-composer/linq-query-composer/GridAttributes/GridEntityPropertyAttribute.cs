namespace Linq.Query.Composer.GridAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class GridEntityPropertyAttribute : Attribute
    {
        public string[] TargetedPropertyPath { get; set; }
    }
}