using BrokenGrenade.Common.Models;
using Discord;
using Discord.Webhook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BrokenGrenade.Web.BL;

public class DiscordWebhookSender
{
    private readonly ILogger<DiscordWebhookSender> _logger;

    private readonly string _baseUrl;
    
    private readonly string _username;
    private readonly string _avatarUrl;
    private readonly int _color;
    private readonly string _missionMention;
    
    private readonly DiscordWebhookClient? _applicationWebhook;
    private readonly DiscordWebhookClient? _missionWebhook;
    private readonly DiscordWebhookClient? _trainingWebhook;
    private readonly DiscordWebhookClient? _managementWebhook;
    
    public DiscordWebhookSender(IConfiguration configuration, ILogger<DiscordWebhookSender> logger)
    {
        _logger = logger;

        _baseUrl = configuration["BaseUrl"] ?? "https://hub.brokengrenade.cz";
        _username = configuration["Discord:Username"] ?? "Broken Grenade";
        _avatarUrl = configuration["Discord:AvatarUrl"] ?? "https://www.brokengrenade.cz/wp-content/uploads/2023/03/logo_kruh.jpg";
        _color = int.Parse(configuration["Discord:Color"] ?? "9703438");
        _missionMention = configuration["Discord:MissionMention"] ?? "<@&688394199984504996>";

        if (configuration["Discord:ApplicationWebhookUrl"] is not null)
            _applicationWebhook = new DiscordWebhookClient(configuration["Discord:ApplicationWebhookUrl"]);
        
        if (configuration["Discord:MissionWebhookUrl"] is not null)
            _missionWebhook = new DiscordWebhookClient(configuration["Discord:MissionWebhookUrl"]);

        if (configuration["Discord:TrainingWebhookUrl"] is not null)
            _trainingWebhook = new DiscordWebhookClient(configuration["Discord:TrainingWebhookUrl"]);

        if (configuration["Discord:ManagementWebhookUrl"] is not null)
            _managementWebhook = new DiscordWebhookClient(configuration["Discord:ManagementWebhookUrl"]);

        
        
    }

    public void SendManagementMessage(string title, string message)
    {
        throw new NotImplementedException();
    }

    public async Task<ulong?> SendMissionAsync(MissionModel mission)
    {
        if (_missionWebhook is null)
        {
            _logger.LogError("Failed to send mission to Discord, mission webhook URL is null");
            return null;
        }

        var builder = new EmbedBuilder()
            .WithTitle(mission.Name)
            .WithColor((uint)_color)
            .WithUrl($"{_baseUrl}/Mission/{mission.Id}")
            .WithFields(
                new EmbedFieldBuilder()
                    .WithName("Autor")
                    .WithValue(mission.Creator?.Nickname ?? "Neuvedeno")
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Datum")
                    .WithValue(mission.MissionStartDate.ToString("dd.MM.yyyy"))
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Druh Mise")
                    .WithValue(mission.MissionType?.Name ?? "Neuvedeno")
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Druh Modpacku")
                    .WithValue(mission.ModpackType?.Name ?? "Ostatní")
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Připojování")
                    .WithValue(mission.MissionStartDate.AddMinutes(-30).ToString("HH:mm"))
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Začátek mise")
                    .WithValue(mission.MissionStartDate.ToString("HH:mm"))
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Slotování")
                    .WithValue($"[Odkaz]({mission.SlottingSheetUrl})")
            );

        if (mission.ModpackUrl is not null)
            builder = builder.AddField("Vlastní modpack", $"[Odkaz]({mission.ModpackUrl}", true);
        
        return await _missionWebhook.SendMessageAsync(text: _missionMention,
            embeds: new []{builder.Build()},
            username: _username,
            avatarUrl: _avatarUrl);
    }
    
    public void SendApplication(ApplicationModel application)
    {
        // if (_applicationWebhook is null)
        // {
        //     _logger.LogError("Failed to send application to Discord, application webhook URL is null");
        //     return;
        // }
        //
        // var embed = new Embed
        // {
        //     title = "Nová přihláška",
        //     url = $"{_baseUrl}/Application/Detail/{application.Id}",
        //     color = 9703438,
        //     fields = new[]
        //     {
        //         new Field
        //         {
        //             name = "Přezdívka",
        //             value = application.User?.Nickname,
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Email",
        //             value = application.Email,
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Discord",
        //             value = application.Discord,
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Věk",
        //             value = application.Age.ToString(),
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Herní čas",
        //             value = application.PlayTime.ToString(),
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Popiš sám sebe",
        //             value = application.About
        //         },
        //         new Field
        //         {
        //             name = "Předchozí zkušenosti",
        //             value = application.PreviousExperience
        //         },
        //         new Field
        //         {
        //             name = "Proč se chceš přidat k BG?",
        //             value = application.ReasonToJoin
        //         },
        //         new Field
        //         {
        //             name = "Co nám můžeš nabídnout?",
        //             value = application.WhatCanYouOffer
        //         },
        //         new Field
        //         {
        //             name = "Reference",
        //             value = application.HowDidYouFindUs
        //         }
        //     }
        // };
        //
        // var message = new WebhookObject
        // {
        //     username = _username,
        //     avatar_url = _avatarUrl,
        //     embeds = new[] {embed}
        // };
        //
        // try
        // {
        //     _applicationWebhook.PostData(message);
        // }
        // catch (Exception e)
        // {
        //     _logger.LogError(e, "Failed to send application to Discord");
        // }
    }
    
    public void SendTraining(TrainingModel training)
    {
        // if (_trainingWebhook is null)
        // {
        //     _logger.LogError("Failed to send training to Discord, training webhook URL is null");
        //     return;
        // }
        //
        // var embed = new Embed
        // {
        //     title = training.Name,
        //     color = 9703438,
        //     fields = new[]
        //     {
        //         new Field
        //         {
        //             name = "Instruktor",
        //             value = training.Creator?.Nickname,
        //             inline = true
        //         },
        //         new Field
        //         {
        //             name = "Datum",
        //             value = training.Date.ToString("dd.MM.yyyy HH:mm"),
        //             inline = true
        //         }
        //     }
        // };
        //
        // if (!string.IsNullOrWhiteSpace(training.Note))
        //     embed.fields = embed.fields.Append(new Field
        //     {
        //         name = "Poznámka",
        //         value = training.Note
        //     }).ToArray();
        //
        // var message = new WebhookObject
        // {
        //     username = _username,
        //     avatar_url = _avatarUrl,
        //     embeds = new[] { embed }
        // };
        //
        // try
        // {
        //     _trainingWebhook.PostData(message);
        // }
        // catch (Exception e)
        // {
        //     _logger.LogError(e, "Failed to send training to Discord");
        // }
    }
}