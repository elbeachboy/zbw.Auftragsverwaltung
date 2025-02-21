﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using zbw.Auftragsverwaltung.BlazorApp.Services;

namespace zbw.Auftragsverwaltung.BlazorApp.Helpers
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthService AuthenticationService { get; set; }


        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;

            if (!authorize)
            {
                base.Render(builder);
                return;
            }

            if (AuthenticationService.Validate())
            {
                base.Render(builder);
                return;
            }

            AuthenticationService.Refresh();

            if (!AuthenticationService.Validate())
            {
                var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
                NavigationManager.NavigateTo($"account/login?returnUrl={returnUrl}");
            }
            else
            {
                base.Render(builder);
            }
        }

        
    }
}
