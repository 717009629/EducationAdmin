using Abp.Configuration;
using Abp.Configuration.Startup;
using EducationAdmin.Authorization.Users;
using EducationAdmin.Education;
using EducationAdmin.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationAdmin.Configuration
{
    public class CustomConfigProvider : ICustomConfigProvider
    {
        public Dictionary<string, object> GetConfig(CustomConfigProviderContext customConfigProviderContext)
        {
            var dict = new Dictionary<string, object>();
            GetDict<ClassState>(dict);
            GetDict<OrderState>(dict);
            GetDict<ClassType>(dict);
           // GetDict<CustomerState>(dict);
            GetDict<UserType>(dict);
            return dict;
        }

        private Dictionary<string, object> GetDict<T>(Dictionary<string, object> d) where T : Enum
        {
            Type t = typeof(T);
            var arrays = Enum.GetNames(t);
            Dictionary<int, string> dict = new Dictionary<int, string>();
            
            foreach (var a in arrays)
            {
                dict.Add((int)Enum.Parse(t, a), a);
            }

            var name =t.Name;
            d.Add(name.Substring(0, 1).ToUpper() + name.Substring(1) , dict);

            var list = new List<object>();

            foreach (var a in arrays)
            {
                list.Add(new {key= (int)Enum.Parse(t, a), value= a });
            }
            d.Add(name.Substring(0, 1).ToLower() + name.Substring(1), list);

            return d;
        }

    }


}
