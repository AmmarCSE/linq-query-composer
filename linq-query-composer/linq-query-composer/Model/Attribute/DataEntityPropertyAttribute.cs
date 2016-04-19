namespace Linq.Query.Composer.Model.Attribute
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class DataEntityPropertyAttribute : Attribute
    {
        public string[] TargetedPropertyPath { get; set; }
    }
}