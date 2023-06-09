using BrokenGrenade.Common.Enums;
using BrokenGrenade.Common.Extensions;
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
    private readonly uint _color;
    private readonly string _missionMention;
    private readonly string _newbieMention;
    
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
        _color = uint.Parse(configuration["Discord:Color"] ?? "9703438");
        _missionMention = configuration["Discord:MissionMention"] ?? "<@&688394199984504996>";
        _newbieMention = configuration["Discord:NewbieMention"] ?? "<@&611969964353519645>";

        if (configuration["Discord:ApplicationWebhookUrl"] is not null)
            _applicationWebhook = new DiscordWebhookClient(configuration["Discord:ApplicationWebhookUrl"]);
        
        if (configuration["Discord:MissionWebhookUrl"] is not null)
            _missionWebhook = new DiscordWebhookClient(configuration["Discord:MissionWebhookUrl"]);

        if (configuration["Discord:TrainingWebhookUrl"] is not null)
            _trainingWebhook = new DiscordWebhookClient(configuration["Discord:TrainingWebhookUrl"]);

        if (configuration["Discord:ManagementWebhookUrl"] is not null)
            _managementWebhook = new DiscordWebhookClient(configuration["Discord:ManagementWebhookUrl"]);
    }

    public async Task SendManagementMessageAsync(string title, string message)
    {
        if (_managementWebhook is null)
        {
            _logger.LogError("Failed to send management message to Discord, management webhook URL is null");
            return;
        }

        var builder = new EmbedBuilder()
            .WithTitle(title)
            .WithColor(_color)
            .WithDescription(message);
        
        await _managementWebhook.SendMessageAsync(embeds: new[] { builder.Build() },
            username: _username,
            avatarUrl: _avatarUrl);
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
            .WithColor(_color)
            .WithUrl($"{_baseUrl}/Mission/{mission.Id}")
            .WithFields(
                new EmbedFieldBuilder()
                    .WithName("Autor")
                    .WithValue(mission.Creator?.Nickname ?? "Neuvedeno")
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Datum")
                    .WithValue(mission.MissionStartDate.ToDateString())
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
            builder = builder.AddField("Vlastní modpack", $"[Odkaz]({mission.ModpackUrl})", true);
        
        return await _missionWebhook.SendMessageAsync(text: _missionMention,
            embeds: new []{builder.Build()},
            username: _username,
            avatarUrl: _avatarUrl);
    }
    
    public async Task<ulong?> SendApplicationAsync(ApplicationModel application)
    {
        if (_applicationWebhook is null)
        {
            _logger.LogError("Failed to send application to Discord, application webhook URL is null");
            return null;
        }

        var builder = new EmbedBuilder()
            .WithTitle("Nová přihláška")
            .WithColor(_color)
            .WithUrl($"{_baseUrl}/Staff/Application/{application.Id}")
            .WithFields(
                new EmbedFieldBuilder()
                    .WithName("Přezdívka")
                    .WithValue(application.Nickname)
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Email")
                    .WithValue(application.Email)
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Discord")
                    .WithValue(application.Discord)
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Věk")
                    .WithValue(application.Age)
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Herní čas")
                    .WithValue(application.PlayTime)
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Popiš sám sebe")
                    .WithValue(application.About),
                new EmbedFieldBuilder()
                    .WithName("Předchozí zkušenosti")
                    .WithValue(string.IsNullOrWhiteSpace(application.PreviousExperience) ? "*Neuvedeno*" : application.PreviousExperience),
                new EmbedFieldBuilder()
                    .WithName("Proč se chceš přidat k BG?")
                    .WithValue(application.ReasonToJoin),
                new EmbedFieldBuilder()
                    .WithName("Reference")
                    .WithValue(string.IsNullOrWhiteSpace(application.HowDidYouFindUs) ? "*Neuvedeno*" : application.HowDidYouFindUs)
            );

        return await _applicationWebhook.SendMessageAsync(text: "@everyone",
            embeds: new[]
            {
                builder.Build()
            },
            username: _username,
            avatarUrl: _avatarUrl);
    }
    
    public async Task<ulong?> SendTrainingAsync(TrainingModel training, TrainingMentionType mentionType)
    {
        if (_trainingWebhook is null)
        {
            _logger.LogError("Failed to send training to Discord, training webhook URL is null");
            return null;
        }

        var builder = new EmbedBuilder()
            .WithTitle(training.Name)
            .WithColor(_color)
            .WithUrl($"{_baseUrl}/Training/{training.Id}")
            .WithFields(
                new EmbedFieldBuilder()
                    .WithName("Datum")
                    .WithValue(training.Date.ToDateTimeString())
                    .WithIsInline(true),
                new EmbedFieldBuilder()
                    .WithName("Instruktor")
                    .WithValue(training.Creator?.Nickname ?? "Neuvedeno")
                    .WithIsInline(true)
            );
        
        var mention = mentionType switch
        {
            TrainingMentionType.None => "",
            TrainingMentionType.Newbies => _newbieMention,
            TrainingMentionType.Subscribers => _missionMention,
            _ => throw new ArgumentOutOfRangeException(nameof(mentionType), mentionType, null)
        };
        return await _trainingWebhook.SendMessageAsync(text: mention, embeds: new[]
            {
                builder.Build()
            },
            username: _username, avatarUrl: _avatarUrl);
    }
}