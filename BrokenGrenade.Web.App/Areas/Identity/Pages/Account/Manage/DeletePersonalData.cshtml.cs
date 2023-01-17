// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using BrokenGrenade.Web.App.Resources;
using BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account.Manage;
using BrokenGrenade.Web.BL.Facades;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Extensions.Localization;

namespace BrokenGrenade.Web.App.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly UserFacade _userFacade;

        public DeletePersonalDataModel(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserFacade userFacade)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
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
            [Display(Name = nameof(SharedResources.Password), ResourceType = typeof(SharedResources))]
            [Required(ErrorMessageResourceName = nameof(SharedResources.RequiredField), ErrorMessageResourceType = typeof(SharedResources))]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Display(Name = nameof(DeletePersonalDataResource.ReasonToLeave), ResourceType = typeof(DeletePersonalDataResource))]
            [StringLength(500, ErrorMessageResourceName = nameof(SharedResources.StringLengthMax), ErrorMessageResourceType = typeof(SharedResources), MinimumLength = 0)]
            public string Reason { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO: Handle feedback from user
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, _sharedLocalizer[nameof(SharedResources.InvalidPassword)]);
                    return Page();
                }
            }

            await _userFacade.DeleteAsync(user.Id);
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", user.Id);

            return Redirect("~/");
        }
    }
}
