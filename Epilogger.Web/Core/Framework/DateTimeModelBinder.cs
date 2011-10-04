using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timezone.Framework
{

    public class DateTimeModelBinder : IModelBinder
    {
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || value.AttemptedValue == "null" || value.AttemptedValue == String.Empty || value.AttemptedValue.Trim(',') == String.Empty) return null;
            return TimeZoneManager.ToUtcTime(((DateTime?)value.ConvertTo(typeof(DateTime))).Value);
        }

    }

}