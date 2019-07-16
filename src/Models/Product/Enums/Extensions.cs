using System;
using System.ComponentModel;
using System.Reflection;

namespace Exemplo.Models.Products.Enums {
    
    public static class Extensions {
        public static string GetDescription(this Enum value) {
            FieldInfo info = value.GetType().GetField(value.ToString());
            var attribute =  info.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            return attribute.Description;
        }
    }

}