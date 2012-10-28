﻿using System.Web.Routing;
using Bifrost.Web.Applications;
using Bifrost.Mimir.Web.Events;
using Bifrost.Services.Execution;

namespace Bifrost.Mimir.Web
{
    public class ClassForTypeSafeConfiguration
    {
    }
}

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure WithMimir(this IConfigure configure)
        {
            RouteTable.Routes.AddApplicationFromAssembly("Mimir", typeof(Bifrost.Mimir.Web.ClassForTypeSafeConfiguration).Assembly);
            RouteTable.Routes.AddService<EventService>("Events");
            RouteTable.Routes.AddService<EventSubscriptionService>("EventSubscriptions");
            return configure;
        }
    }
}