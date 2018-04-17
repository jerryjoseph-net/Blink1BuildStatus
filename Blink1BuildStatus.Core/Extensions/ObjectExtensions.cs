using System;
using System.ComponentModel;

namespace Blink1BuildStatus.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object value)
        {
            //Handling Nullable types i.e, int?, double?, bool? .. etc
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                var typeConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)typeConverter.ConvertFrom(value);
            }
            else if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value.ToString(), ignoreCase: true);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}
