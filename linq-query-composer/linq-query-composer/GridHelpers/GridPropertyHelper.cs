namespace Linq.Query.Composer.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    using Linq.Query.Composer.GridAttributes;
    using Linq.Query.Composer.GridResources;

    public static class GridPropertyHelper
    {
        public static List<PropertyInfo> ExtractQuickSearchProperties<TModel>()
        {
            return null;
        }
        public static List<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static List<PropertyInfo> ExtractGridModelSelectProperties<TGridModel>()
        {
            return
                new List<PropertyInfo>(
                    typeof(TGridModel).GetProperties()
                                      .Where(p => !Attribute.IsDefined(p, typeof(GridComputedPropertyAttribute), false)));
        }

        public static IList<PropertyInfo> ExtractKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static IList<PropertyInfo> ExtractNonKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static dynamic RetrieveGridEntityPropertyPath(PropertyInfo property)
        {
            GridEntityPropertyAttribute attribute = 
                (GridEntityPropertyAttribute)property.GetCustomAttributes(typeof(GridEntityPropertyAttribute), false)
                    .First();
            attribute.TargetedPropertyPath = attribute.TargetedPropertyPath ?? new string[] { };

            return attribute.TargetedPropertyPath.ToList();
        }

        public static int DetermineTypeCode(PropertyInfo property)
        {
            bool isNullable; // dummy variable
            return DetermineTypeCodeAndNullability(property, out isNullable);
        }

        public static int DetermineTypeCodeAndNullability(PropertyInfo property, out bool isNullable)
        {
            isNullable = false;
            TypeCode typeCode;
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                typeCode = Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]);
                isNullable = true;
            }
            else
            {
                typeCode = Type.GetTypeCode(property.PropertyType);
            }

            if (typeCode == TypeCode.Object && property.PropertyType.Name == "TimeSpan")
            {
                return 17;
            }

            return (int)typeCode;
        }
    }
}