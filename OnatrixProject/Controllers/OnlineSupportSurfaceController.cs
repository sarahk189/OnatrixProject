using Microsoft.AspNetCore.Mvc;
using OnatrixProject.Models;
using OnatrixProject.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixProject.Controllers
{
    public class OnlineSupportSurfaceController : SurfaceController
    {
        private readonly EmailService _emailService;

        public OnlineSupportSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, EmailService emailService)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> HandleSubmitEmailOnly(OnlineSupportFormModel form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["email"] = form.Email;
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email) ? "Email is required" : null;
                return CurrentUmbracoPage();
            }

            var defaultName = "Onatrix Customer";
            var result = await _emailService.SendEmailAsync(form.Email, defaultName);

            if (result)
            {
                TempData["emailsuccess"] = "Email submitted successfully";
                TempData.Remove("email");
                return RedirectToCurrentUmbracoPage();
            }
            else
            {
                TempData["emailerror"] = "An error occurred while submitting the email";
                return RedirectToCurrentUmbracoPage();
            }
        }
    }
}