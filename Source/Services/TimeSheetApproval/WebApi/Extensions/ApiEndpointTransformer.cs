using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace TimeSheetApproval.WebApi.Extensions
{
    public class ApiEndpointTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {            
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower().Replace(" ","-");
        }
    }
    
}
