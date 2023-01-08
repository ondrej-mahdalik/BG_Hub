using BrokenGrenade.Common.Enums;

namespace BrokenGrenade.Web.DAL.Entities;

public class ApplicationEntity : EntityBase
{
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Discord { get; set; }
    public string SteamUrl { get; set; }
    public int Age { get; set; }
    public int PlayTime { get; set; }
    public string About { get; set; }
    public string PreviousExperience { get; set; }
    public string ReasonToJoin { get; set; }
    public string WhatCanYouOffer { get; set; }
    public string HowDidYouFindUs { get; set; }

    public string? StaffComment { get; set; }

    public bool AgreedToRules { get; set; }
    public bool MeetsRequirements { get; set; }
    public bool AcceptedGdpr { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.AwaitingDecision;

    public Guid? EditedById { get; set; }
    public UserEntity? EditedBy { get; init; }

    public UserEntity? User { get; init; }

    public ApplicationEntity(string nickname, string email, string discord, string steamUrl, int age, int playTime,
        string about, string previousExperience, string reasonToJoin, string whatCanYouOffer,
        string howDidYouFindUs)
    {
        Nickname = nickname;
        Email = email;
        Discord = discord;
        SteamUrl = steamUrl;
        Age = age;
        PlayTime = playTime;
        About = about;
        PreviousExperience = previousExperience;
        ReasonToJoin = reasonToJoin;
        WhatCanYouOffer = whatCanYouOffer;
        HowDidYouFindUs = howDidYouFindUs;
    }

    public ApplicationEntity()
    {
        Nickname = string.Empty;
        Email = string.Empty;
        Discord = string.Empty;
        SteamUrl = string.Empty;
        Age = 0;
        PlayTime = 0;
        About = string.Empty;
        PreviousExperience = string.Empty;
        ReasonToJoin = string.Empty;
        WhatCanYouOffer = string.Empty;
        HowDidYouFindUs = string.Empty;
    }
}