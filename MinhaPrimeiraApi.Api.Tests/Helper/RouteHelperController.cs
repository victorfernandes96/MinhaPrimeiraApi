using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MinhaPrimeiraApi.Api.Tests.Helper
{
    public static class RouteHelperController
    {
        const string routeAttributeName = "RouteAttribute";

        public static string GetRoute<T>(string methodName)
            where T : ControllerBase
        {
            var routeController = (typeof(T).CustomAttributes.FirstOrDefault(c => c.AttributeType.Name == routeAttributeName)?
                .ConstructorArguments[0].Value?.ToString() ?? string.Empty) + "/";
            var method = typeof(T).GetMethod(methodName);
            var route = method.CustomAttributes.FirstOrDefault(c => c.AttributeType.Name == routeAttributeName);
            if (route == null)
            {
                route = method.CustomAttributes.FirstOrDefault(c => c.AttributeType.Name.Contains("Http"));
            }
            if (route?.ConstructorArguments.Any() ?? false)
            {
                return routeController + route?.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
            }
            return routeController;
        }
    }
}