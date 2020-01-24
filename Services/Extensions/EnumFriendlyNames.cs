using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Services.Extensions
{
    public static class EnumFriendlyNames
    {
        public static string GetDesplayName(this Enum enumValue)
        {

            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            if (fi != null)
            {
                var attribute = (DisplayAttribute)fi.GetCustomAttribute(typeof(DisplayAttribute));
                return attribute.GetName();
            }
            return "";
            
        }
    }
}
