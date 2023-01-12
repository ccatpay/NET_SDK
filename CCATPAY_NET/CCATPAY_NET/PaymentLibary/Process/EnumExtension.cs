using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System
{
    public static class EnumExtension
    {
        public static string DisplayName(this object value)
        {
            Type enumType = value?.GetType();
            return value == null ? "" : GetDisplayName(value, enumType);
        }

        private static string GetDisplayName(object value, Type enumType)
        {
            var enumValue = Enum.GetName(enumType, value);
            if (enumValue == null) return "";
            var member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (!attrs.Any()) return value.ToString();
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
                outString = ((DisplayAttribute)attrs[0]).GetName();
            return outString;
        }
    }
}
