// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.BL.Facades;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BrokenGrenade.Web.App.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly ApplicationFacade _applicationFacade;
        private readonly PunishmentFacade _punishmentFacade;

        public DownloadPersonalDataModel(
            UserManager<UserEntity> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            ApplicationFacade applicationFacade,
            PunishmentFacade punishmentFacade)
        {
            _userManager = userManager;
            _logger = logger;
            _applicationFacade = applicationFacade;
            _punishmentFacade = punishmentFacade;
        }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, object>();
            var personalDataProps = typeof(UserEntity).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var application = await _applicationFacade.GetByUserAsync(user.Id);
            if (application is not null)
                personalData.Add("Application", application);

            var receivedPunishments = await _punishmentFacade.GetByUserAsync(user.Id);
            personalData.Add("ReceivedPunishments", receivedPunishments);

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            personalData.Add($"Authenticator Key", await _userManager.GetAuthenticatorKeyAsync(user));

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "application/json");
        }
    }
}
