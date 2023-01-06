namespace BrokenGrenade.Common.Models;

public class ApplicationModel : ModelBase
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
}