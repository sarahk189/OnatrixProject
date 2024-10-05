using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    public class ContactSurfaceController : SurfaceController
    {
        public ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }


        [HttpPost]
        public async Task<IActionResult> HandleSubmit(ContactFormModels form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = form.Name;
                ViewData["phone"] = form.Phone;
                ViewData["email"] = form.Email;


                ViewData["error_name"] = string.IsNullOrEmpty(form.Name) ? "Name is required" : null;
                ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone) ? "Phone is required" : null;
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email) ? "Email is required" : null;


                return CurrentUmbracoPage();
            }

            EmailService emailService = new EmailService();
            var result = await emailService.SendEmailAsync(form.Email, form.Name);

            if (result)
            {
                TempData["success"] = "Form submitted successfully";

                TempData.Remove("name");
                TempData.Remove("email");
                TempData.Remove("phone");

                return RedirectToCurrentUmbracoPage();
            }
            else
            {
                TempData["error"] = "An error occurred while submitting the form";
                return RedirectToCurrentUmbracoPage();
            }
        }

    }
}
