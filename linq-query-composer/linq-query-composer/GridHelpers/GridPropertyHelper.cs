// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridPropertyHelper.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Property Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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

    /// <summary>
    /// The grid property helper.
    /// </summary>
    public static class GridPropertyHelper
    {
        public static List<PropertyInfo> ExtractQuickSearchProperties<TModel>()
        {
            return null;
        }
        /// <summary>
        /// The extract grid model properties.
        /// </summary>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public static List<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        /// <summary>
        /// The extract grid model select properties.
        /// </summary>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public static List<PropertyInfo> ExtractGridModelSelectProperties<TGridModel>()
        {
            return
                new List<PropertyInfo>(
                    typeof(TGridModel).GetProperties()
                                      .Where(p => !Attribute.IsDefined(p, typeof(GridComputedPropertyAttribute), false)));
        }

        /// <summary>
        /// The extract key properties.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The IList.
        /// </returns>
        public static IList<PropertyInfo> ExtractKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        /// <summary>
        /// The extract non key properties.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The IList.
        /// </returns>
        public static IList<PropertyInfo> ExtractNonKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        /// <summary>
        /// The retrieve grid entity property path.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The dynamic.
        /// </returns>
        public static dynamic RetrieveGridEntityPropertyPath(PropertyInfo property)
        {
            GridEntityPropertyAttribute attribute = 
                (GridEntityPropertyAttribute)property.GetCustomAttributes(typeof(GridEntityPropertyAttribute), false)
                    .First();
            attribute.TargetedPropertyPath = attribute.TargetedPropertyPath ?? new string[] { };

            return attribute.TargetedPropertyPath.ToList();
        }

        /// <summary>
        /// The determine type code.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int DetermineTypeCode(PropertyInfo property)
        {
            bool isNullable; // dummy variable
            return DetermineTypeCodeAndNullability(property, out isNullable);
        }

        /// <summary>
        /// The determine type code and nullability.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="isNullable">
        /// The is nullable.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
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