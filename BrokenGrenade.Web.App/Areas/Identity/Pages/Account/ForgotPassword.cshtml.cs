// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using BrokenGrenade.Web.App.Resources;
using BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account;
using BrokenGrenade.Web.BL.Facades;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;

namespace BrokenGrenade.Web.App.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<ForgotPasswordResource> _localizer;
        private readonly UserFacade _userFacade;

        public ForgotPasswordModel(UserManager<UserEntity> userManager, IEmailSender emailSender, IStringLocalizer<ForgotPasswordResource> localizer, UserFacade userFacade)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _localizer = localizer;
            _userFacade = userFacade;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessageResourceName = nameof(SharedResources.RequiredField), ErrorMessageResourceType = typeof(SharedResources))]
            [EmailAddress(ErrorMessageResourceName = nameof(SharedResources.InvalidEmail), ErrorMessageResourceType = typeof(SharedResources))]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userFacade.ResetPasswordAsync(Input.Email);
                }
                catch
                {
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
