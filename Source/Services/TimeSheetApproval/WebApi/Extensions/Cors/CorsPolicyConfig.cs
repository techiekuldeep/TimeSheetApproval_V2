// <copyright file="CorsPolicyConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimeSheetApproval.WebApi.Extensions.Cors
{
    using Microsoft.AspNetCore.Cors.Infrastructure;

    public class CorsPolicyConfig : CorsPolicy
    {
        public string Name { get; set; }
    }
}
