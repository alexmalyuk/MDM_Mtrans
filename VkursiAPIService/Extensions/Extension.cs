using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VkursiAPI.Extensions
{
    public static class Extension
    {
        /// <summary>
        /// Extension for enums to get its DisplayAttribute.Name field
        /// </summary>
        /// <param name="value"></param>
        /// <example>
        /// Sample code to get DisplayAttribute.Name field
        /// <code>
        /// var name = APIType.GetOrganizationInfo.DisplayName() // returns "organizations" string to name variable
        /// </code>
        /// </example>
        /// <returns>
        /// Return string which contains DisplayAttribute.Name field
        /// </returns>
        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}
