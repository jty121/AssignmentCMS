using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCms.Models;

namespace UmbracoCms.Controllers;

public class RequestSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    public IActionResult HandleRequestSubmit(RequestFormModel form)
    {
        if(!ModelState.IsValid)
        {
            ViewData["name"] = form.Name;
            ViewData["email"] = form.Email;
            ViewData["phone"] = form.Phone;
            ViewData["option"] = form.SelectedOption;


            ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
            ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
            ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone);
            ViewData["error_option"] = string.IsNullOrEmpty(form.SelectedOption);

            return CurrentUmbracoPage();
            // används oftast för att behålla användaren på samma sida utan att ändra URL, visar samma sida igen men med ny data ex. felmeddelanden
        }

        TempData["success"] = "Request send successfully";
        return RedirectToCurrentUmbracoPage(); 
        // används ofta efter en lyckad formulärhantering, delvis för att förhindra att formuläret skickas på nytt om användaren uppdaterar sidan. Följer PRG mönster (post,redirect,get)
    }

    public IActionResult HandleQuestionSubmit(QuestionFormModel form)
    {
        if(!ModelState.IsValid)
        {
            ViewData["name"] = form.Name;
            ViewData["email"] = form.Email;
            ViewData["message"] = form.Message;

            ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
            ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
            ViewData["error_message"] = string.IsNullOrEmpty(form.Message);

            return CurrentUmbracoPage();
        }
        TempData["success"] = "Your question was send successfully";
        return RedirectToCurrentUmbracoPage();
    }

    public IActionResult HandleEmailSubmit(EmailFormModel form)
    {
        if (!ModelState.IsValid)
        {
            ViewData["email"] = form.Email;
            ViewData["error_email"] = string.IsNullOrEmpty(form.Email);

            return CurrentUmbracoPage();
        }
        TempData["success"] = "A verification email has been sent to you!";
        return RedirectToCurrentUmbracoPage();
    }
}
