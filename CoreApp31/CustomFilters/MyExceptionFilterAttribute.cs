using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp31.CustomFilters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider modelMetadata;
        /// <summary>
        /// Inject IModelMetadataProvider
        /// This interface is resolved by MvcOptions in ConfigureServices()
        /// </summary>
        public MyExceptionFilterAttribute(IModelMetadataProvider modelMetadata)
        {
            this.modelMetadata = modelMetadata;
        }

        public override void OnException(ExceptionContext context)
        {
            // 1. Exception Is handled so that Request Processing is completed
            // and the result return process starts
            context.ExceptionHandled = true;
            // 2. Read the exception Message
            string message = context.Exception.Message;
            // 3. Returning result
            var viewResult = new ViewResult();
            // 3a. Create a ViewDataDiciotnary and define Key/Values for it
            var viewData = new ViewDataDictionary(modelMetadata, context.ModelState);
            // 3b. define keys for viewData
            viewData["controller"] = context.RouteData.Values["controller"];
            viewData["action"] = context.RouteData.Values["action"];
            viewData["errorMessage"] = message;
            viewResult.ViewName = "CustomError";
            viewResult.ViewData = viewData;
            context.Result = viewResult;
        }
    }
}
