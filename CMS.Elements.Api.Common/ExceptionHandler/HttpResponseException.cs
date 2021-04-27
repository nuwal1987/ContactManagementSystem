using System;

namespace CMS.Elements.Api.Common.ExceptionHandler
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object Value { get; set; }
    }
}
