using System.Linq;
using System.Web.Mvc;
using System;
using System.Globalization;

namespace Timezone.Framework
{

    public class DateTimeMetadataProvider : DataAnnotationsModelMetadataProvider
    {

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            //Null checks
            if (containerType == null) throw new ArgumentNullException("containerType");
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
            var propertyDescriptor = this.GetTypeDescriptor(containerType).GetProperties().Find(propertyName, true);
            if (propertyDescriptor == null) throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "Property not found (container='{0}', property='{1}'.", containerType.FullName, propertyName));

            //DateTime: For DateTime model accessors, perform the conversion to the users time zone
            if (propertyDescriptor.PropertyType == typeof(DateTime))
            {
                var value = DateTime.MinValue;
                var rawValue = modelAccessor.Invoke();
                if (rawValue != null)
                    value = TimeZoneManager.GetUserTime((DateTime)rawValue);

                return GetMetadataForProperty(() => value, containerType, propertyDescriptor);
            }

            //DateTime?: For DateTime? model accessors, perform the conversion to the users time zone
            if (propertyDescriptor.PropertyType == typeof(DateTime?))
                return GetMetadataForProperty(() =>
                {
                    var dt = (DateTime?)modelAccessor.Invoke();
                    return !dt.HasValue ? dt : TimeZoneManager.GetUserTime(dt.Value);
                }, containerType, propertyDescriptor);

            //Everything else
            return GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
        }
    }

}