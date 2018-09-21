using System;
using System.Reflection;

namespace Selectors
{
    public static class PermissionsHelper
    {
        public static class Entity
        {
            public static string Add(Type entity) => $@"{entity.Name}_Add";
            public static string Edit(Type entity) => $@"{entity.Name}_Edit";
            public static string Delete(Type entity) => $@"{entity.Name}_Delete";
            public static string BatchAdd(Type entity) => $@"{entity.Name}_BatchAdd";
            public static string BatchEdit(Type entity) => $@"{entity.Name}_BatchEdit";
            public static string BatchDelete(Type entity) => $@"{entity.Name}_BatchDelete";
        }

        public static class EntityTranslations
        {
            //TODO: Hello
            public static string Add(Type entity) => $@"{entity.Name}_Add_Translation";
            public static string Edit(Type entity) => $@"{entity.Name}_Edit_Translation";
            public static string Delete(Type entity) => $@"{entity.Name}_Delete_Translation";
            public static string BatchAdd(Type entity) => $@"{entity.Name}_BatchAdd_Translation";
            public static string BatchEdit(Type entity) => $@"{entity.Name}_BatchEdit_Translation";
            public static string BatchDelete(Type entity) => $@"{entity.Name}_BatchDelete_Translation";
        }

        public static class Fetch
        {
            public static string Request(Type entity) => $@"{entity.Name}_Fetch";
            public static string RequestBatch(Type entity) => $@"{entity.Name}_Fetch_Batch";
            public static string First(Type entity) => $@"{entity.Name}_First";
            public static string Last(Type entity) => $@"{entity.Name}_Last";
            public static string Skip(Type entity) => $@"{entity.Name}_Skip";
            public static string After(Type entity) => $@"{entity.Name}_After";
            public static string Before(Type entity) => $@"{entity.Name}_Before";
            public static string Filter(Type entity) => $@"{entity.Name}_Filter";
            public static string Sorting(Type entity) => $@"{entity.Name}_Sorting";
            public static string Language(Type entity) => $@"{entity.Name}_Language";
            public static string FilterParam(Type entity, MemberInfo member) => $@"{entity.Name}_{member.Name}_Filter";
            public static string ExtensionFilter(Type entity, Type extensionName, PropertyInfo property) => $@"{entity.Name}_{extensionName.Name}_{property.Name}";
        }

        public static class Property
        {
            public static string Get(PropertyInfo property) => $@"{property.Name}_Get";
            public static string GetParameter(PropertyInfo property, ParameterInfo parameterInfo) => $@"{property.Name}_{parameterInfo.Name}_Get_Param";
            public static string Add(PropertyInfo property) => $@"{property.Name}_Add";
            public static string Edit(PropertyInfo property) => $@"{property.Name}_Edit";
            public static string Delete(PropertyInfo property) => $@"{property.Name}_DeleteBy";
        }

        public static class PropertyTranslations
        {
            public static string Get(PropertyInfo property) => $@"{property.Name}_Get_Translation";
            public static string Add(PropertyInfo property) => $@"{property.Name}_Add_Translation";
            public static string Edit(PropertyInfo property) => $@"{property.Name}_Edit_Translation";
            public static string Delete(PropertyInfo property) => $@"{property.Name}_Delete_Translation";
        }
    }
}