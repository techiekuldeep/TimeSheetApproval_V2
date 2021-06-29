﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TimeSheetApproval.WebApi.Extensions
{
    public class ControllerDocumentationConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller == null)
                return;
            foreach (var attribute in controller.Attributes)
            {
                if (attribute.GetType() == typeof(RouteAttribute))
                {
                    var routeAttribute = (RouteAttribute)attribute;
                    if (!string.IsNullOrEmpty(routeAttribute.Name))
                    {
                        controller.ControllerName = routeAttribute.Name;
                    }
                }
            }
        }
    }
}
