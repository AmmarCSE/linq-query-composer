﻿namespace Linq.Query.Composer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    using Linq.Query.Composer.Model;
    using Linq.Query.Composer.Model.Attribute;
    using System.Data.Entity.SqlServer;

    public static class ObjectInspector
    {
        public static List<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static List<PropertyInfo> ExtractGridModelSelectProperties<TGridModel>()
        {
            return
                new List<PropertyInfo>(
                    typeof(TGridModel).GetProperties()
                                      .Where(p => !Attribute.IsDefined(p, typeof(DataComputedPropertyAttribute), false)));
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
            DataEntityPropertyAttribute attribute = 
                (DataEntityPropertyAttribute)property.GetCustomAttributes(typeof(DataEntityPropertyAttribute), false)
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

        public static MethodInfo GetStringContainsMethod()
        {
            return typeof(string).GetMethod("Contains", new[] { typeof(string) });
        }

        public static dynamic CreateTypeForSearchGridModel<TGridModel>(List<TGridModel> gridData, int pageCount)
        {
            return new { Data = gridData, PageCount = pageCount };
        }

        public static MethodInfo GetSqlFunctionDateTimeMethodInfo(string methodName)
        {
            return typeof(SqlFunctions).GetMethod(
                methodName, new[] { typeof(string), typeof(DateTime?) });
        }

        public static MethodInfo GetSqlFunctionStringConvertMethod(Type argumentType)
        {
            return typeof(SqlFunctions).GetMethod("StringConvert", new[] { argumentType });
        }
    }
}