using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSys.Web.Models;

namespace UserSys.Web.Commons
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid==false)
            {
                string errorMsg = GetValidMsg(context.ModelState);
                context.Result = new JsonResult(new JsonData { Status = "error", Msg = errorMsg });
            }
        }

        public static string GetValidMsg(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in modelState.Keys)
            {
              var errorCount =  modelState[key].Errors.Count();
                if (errorCount<=0)
                {
                    continue;
                }
                sb.Append("属性【").Append(key).Append("】错误：");
                foreach (var modelError in modelState[key].Errors)
                {
                    sb.Append(modelError.ErrorMessage);
                }
            }
            return sb.ToString() ;
        }

    }

}
