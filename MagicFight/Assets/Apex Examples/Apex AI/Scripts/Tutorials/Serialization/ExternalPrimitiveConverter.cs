#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using System.Globalization;
    using Apex.Serialization;

    public class ExternalPrimitiveConverter : IValueConverter
    {
        public Type[] handledTypes
        {
            get
            {
                return new Type[] { typeof(ExternalPrimitiveType) };
            }
        }

        public object FromString(string value, Type targetType)
        {
            var val = Convert.ToSingle(value, CultureInfo.InvariantCulture);
            return new ExternalPrimitiveType(val);
        }

        public string ToString(object value)
        {
            float val = ((ExternalPrimitiveType)value).value;
            return val.ToString("G8", CultureInfo.InvariantCulture);
        }
    }
}