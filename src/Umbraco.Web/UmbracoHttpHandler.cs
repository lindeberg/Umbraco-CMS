﻿using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Core.Logging;
using Umbraco.Core.Security;
using Umbraco.Core.Services;
using Umbraco.Web.Composing;

namespace Umbraco.Web
{
    public abstract class UmbracoHttpHandler : IHttpHandler
    {
        private UrlHelper _url;

        protected UmbracoHttpHandler()
            : this(Current.UmbracoContextAccessor, Current.Services, Current.Logger, Current.ProfilingLogger)
        { }

        protected UmbracoHttpHandler(IUmbracoContextAccessor umbracoContextAccessor,ServiceContext service, ILogger logger, IProfilingLogger profilingLogger )
        {
            UmbracoContextAccessor = umbracoContextAccessor;
            Logger = logger;
            ProfilingLogger = profilingLogger ;
            Services = service;
        }

        public abstract void ProcessRequest(HttpContext context);

        public abstract bool IsReusable { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets the ProfilingLogger.
        /// </summary>
        public IProfilingLogger ProfilingLogger { get; }

        /// <summary>
        /// Gets the Umbraco context accessor.
        /// </summary>
        public IUmbracoContextAccessor UmbracoContextAccessor { get; }

        /// <summary>
        /// Gets the services context.
        /// </summary>
        public ServiceContext Services { get; }

        /// <summary>
        /// Gets the web security helper.
        /// </summary>
        public IBackOfficeSecurity Security => UmbracoContextAccessor.UmbracoContext.Security;

        /// <summary>
        /// Gets the Url helper.
        /// </summary>
        /// <remarks>This URL helper is created without any route data and an empty request context.</remarks>
        public UrlHelper Url => _url ?? (_url = new UrlHelper(HttpContext.Current.Request.RequestContext));
    }
}
