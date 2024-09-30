using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoOnatrix.Models;
using UmbracoOnatrix.Services;

namespace UmbracoOnatrix.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        private readonly EmailService _emailService;
        public ContactSurfaceController(EmailService emailService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _emailService = emailService;
        }

        public IActionResult SubmitEmailForm(ContactForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["email"] = form.Email;

                ViewData["error_email"] = string.IsNullOrEmpty(form.Email);

                return CurrentUmbracoPage();
            }
            var res = _emailService.SendEmail(form.Email);
            if (res)
            {
                ViewData["success"] = "Your email has been succesfully submitted!";
            }
            else
            {
                ViewData["email"] = form.Email;

                ViewData["error_email"] = !string.IsNullOrEmpty(form.Email);

            }
            return CurrentUmbracoPage();
        }

        public IActionResult SubmitServiceForm(ServiceForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["message"] = form.Message;

                ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
                ViewData["error_message"] = string.IsNullOrEmpty(form.Message);


                return CurrentUmbracoPage();
            }
            var res = _emailService.SendEmail(form.Email);
            if (res)
            {
                ViewData["success"] = "Your form has been succesfully submitted!";
            }
            else
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["message"] = form.Message;

                ViewData["error_email"] = !string.IsNullOrEmpty(form.Email);
                return CurrentUmbracoPage();
            }
            ViewData["success"] = "Your form has been succesfully submitted!";
            return CurrentUmbracoPage();
        }

        public IActionResult SubmitHomeForm(HomeForm form)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["phone"] = form.Phone;
                ViewData["selectedOption"] = form.SelectedOption;


                ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
                ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
                ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone);
                ViewData["error_selectedOption"] = string.IsNullOrEmpty(form.SelectedOption);

                return CurrentUmbracoPage();
            }
            var res = _emailService.SendEmail(form.Email);
            if (res)
            {
                ViewData["success"] = "Your form has been succesfully submitted!";
            }
            else
            {
                ViewData["name"] = form.Name;
                ViewData["email"] = form.Email;
                ViewData["phone"] = form.Phone;
                ViewData["selectedOption"] = form.SelectedOption;

                ViewData["error_email"] = !string.IsNullOrEmpty(form.Email);

            }
            return CurrentUmbracoPage();
        }
    }
}
