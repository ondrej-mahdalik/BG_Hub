// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BrokenGrenade.Web.App.Resources;
using BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account.Manage;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BrokenGrenade.Web.App.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IStringLocalizer<ChangePasswordResource> _localizer;

        public ChangePasswordModel(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            ILogger<ChangePasswordModel> logger,
            IStringLocalizer<ChangePasswordResource> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
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
        [TempData]
        public string StatusMessage { get; set; }

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
            [DataType(DataType.Password)]
            [Display(Name = nameof(ChangePasswordResource.InputOldPassword), ResourceType = typeof(ChangePasswordResource))]
            public string OldPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessageResourceName = nameof(SharedResources.RequiredField), ErrorMessageResourceType = typeof(SharedResources))]
            [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(SharedResources.StringLengthRange), ErrorMessageResourceType = typeof(SharedResources))]
            [DataType(DataType.Password)]
            [Display(Name = nameof(ChangePasswordResource.InputNewPassword), ResourceType = typeof(ChangePasswordResource))]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = nameof(ChangePasswordResource.InputConfirmPassword), ResourceType = typeof(ChangePasswordResource))]
            [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(ChangePasswordResource.InputPasswordsDontMatch), ErrorMessageResourceType = typeof(ChangePasswordResource))]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully");
            StatusMessage = _localizer[nameof(ChangePasswordResource.PasswordChangedConfirmation)];

            return RedirectToPage();
        }
    }
}
