using Microsoft.AspNetCore.Mvc;
using OnatrixProject.Models;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixProject.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult HandleSubmit(ContactFormModels form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["phone"] = form.Phone;
                ViewData["service"] = form.Service;
                ViewData["message"] = form.Message;

                ViewData["error_name"] = string.IsNullOrEmpty(form.Name) ? "Name is required." : null;
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email) ? "Email is required." : null;
                ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone) ? "Phone is required." : null;
                ViewData["error_service"] = string.IsNullOrEmpty(form.Service) ? "Service is required." : null;
                ViewData["error_message"] = string.IsNullOrEmpty(form.Message) ? "Message is required." : null;
                return CurrentUmbracoPage();
            }

            TempData["success"] = "Thank you for your message! We will get back to you as soon as possible!";
            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public IActionResult HandleSubmitEmailOnly(ContactFormModels form)
        {
            if (string.IsNullOrEmpty(form.Email))
            {
                TempData["email"] = form.Email;
                TempData["error_email"] = "Email is required.";
                return CurrentUmbracoPage();
            }

            TempData["success"] = "Thank you for your message! We will get back to you as soon as possible!";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
