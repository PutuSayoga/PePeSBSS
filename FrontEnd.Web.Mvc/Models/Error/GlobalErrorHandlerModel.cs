using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Error
{
    public class GlobalErrorHandlerModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExtraMessage { get; set; }
    }
}
