namespace NewPlatform.Flexberry.ORM.ODataService.Tests.Extensions
{
    using Microsoft.OData.Edm;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.OData;

    public static class EdmEntityObjectExtensions
    {
        public static Dictionary<string, object> ToJson(this EdmEntityObject value, bool isRoot = true)
        {
            var d = new Dictionary<string, object>();

            foreach (IEdmProperty p in value.ActualEdmType.DeclaredProperties)
            {
                switch (p.Name.ToLower())
                {
                    case "вычислимоеполе":
                    case "пол":
                        continue;
                }

                object propertyValue = null;
                try
                {
                    _ = value.TryGetPropertyValue(p.Name, out propertyValue);
                }
                catch (MissingMethodException)
                {
                    continue;
                }

                if (p.Type.IsNullable && propertyValue == null)
                {
                    continue;
                }

                if (propertyValue is EdmEntityObject subValue)
                {
                    propertyValue = ToJson(subValue, false);
                    /*
                    if (subValue.ActualEdmType.DeclaredProperties.Any(x => x.Name.ToLower() == "__primarykey"))
                    {
                        propertyValue = ToJson(subValue);
                    }
                    */
                }

                if (p.Type.Definition.TypeKind == EdmTypeKind.Collection)
                {
                    var l = new List<object>();
                    foreach (var item in propertyValue as IEnumerable<object>)
                    {
                        var item1 = item;
                        if (item1 is EdmEntityObject subItem)
                        {
                            /*
                            if (subItem.ActualEdmType.DeclaredProperties.Any(x => x.Name.ToLower() == "__primarykey"))
                            {
                                item1 = subItem.ToJson();
                            }
                            else
                            {
                                continue;
                            }
                            */
                            item1 = subItem.ToJson(false);
                        }

                        l.Add(item1);
                    }

                    propertyValue = l;
                }

                d.Add(p.Name, propertyValue);
            }

            return isRoot ? d : (d.Keys.Any(x => x.ToLower() == "__primarykey") ? d : null);
            //return d;
            //throw new NotImplementedException();
        }
    }
}
