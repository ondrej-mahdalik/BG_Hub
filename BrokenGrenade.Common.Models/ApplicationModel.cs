using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Common.Models;

public class ApplicationModel : ModelBase
{
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Přezdívka")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} musí mít minimálně {2} znaky a maximálně {1} znaků.")]
    public string Nickname { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [RegularExpression("^.{3,32}#[0-9]{4}$", ErrorMessage = "Discord tag musí být ve formátu 'Přezdívka#1234'")]
    public string Discord { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Odkaz na Steam profil")]
    [Url(ErrorMessage = "Zadej platnou URL adresu.")]
    [RegularExpression(@"^(?:https?:\/\/)?steamcommunity\.com\/(?:profiles|id)\/[a-zA-Z0-9]+(\/)?$", ErrorMessage = "Nejedná se o URL adresu Steam profilu.")]
    public string SteamUrl { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Věk")]
    [Range(15, 99, ErrorMessage = "Musíš být starší než 15 let.")]
    public int? Age { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Nahraný čas v Arma 3")]
    [Range(0, int.MaxValue, ErrorMessage = "Nemůžeš mít záporný čas.")]
    public int? PlayTime { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Popiš sám sebe")]
    [StringLength(500, MinimumLength = 50, ErrorMessage = "Odpověď musí mít minimálně {2} znaků (a maximálně {1}). Pokud je moc krátká, zkus se trochu víc rozepsat.")]
    public string About { get; set; }
    
    [Display(Name = "Máš již nějakou zkušenost s Arma skupinami?")]
    [StringLength(500, ErrorMessage = "Odpověď musí mít maximálně {1} znaků.")]
    public string PreviousExperience { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Proč se k nám chceš přidat?")]
    [StringLength(500, MinimumLength = 50, ErrorMessage = "Odpověď musí mít minimálně {2} znaků (a maximálně {1}). Pokud je moc krátká, zkus se trochu víc rozepsat.")]
    public string ReasonToJoin { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Display(Name = "Co nám můžeš nabídnout?")]
    [StringLength(500, MinimumLength = 50, ErrorMessage = "Odpověď musí mít minimálně {2} znaků (a maximálně {1}). Pokud je moc krátká, zkus se trochu víc rozepsat.")]
    public string WhatCanYouOffer { get; set; }
    
    [Display(Name = "Jak jsi se o nás dozvěděl?")]
    [StringLength(500, ErrorMessage = "Odpověď musí mít maximálně {1} znaků.")]
    public string HowDidYouFindUs { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Musíš souhlasit s našimi pravidly.")]
    public bool AgreedToRules { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Musíš potvrdit, že splňuješ všechny naše podmínky pro přijetí.")]
    public bool MeetsRequirements { get; set; }
    
    [Required(ErrorMessage = "Pole {0} je povinné.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Musíš potvrdit souhlas se zpracováním osobních údajů.")]
    public bool AcceptedGdpr { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public Guid? ChangedById { get; set; }
    public UserModel? ChangedBy { get; set; }
    
    public UserModel? User { get; set; }
    public Guid? UserId { get; set; }
    
    public ApplicationModel(string nickname, string email, string discord, string steamUrl, int age, int playTime, string about, string previousExperience, string reasonToJoin, string whatCanYouOffer, string howDidYouFindUs)
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
    
    public ApplicationModel()
    {
        Nickname = string.Empty;
        Email = string.Empty;
        Discord = string.Empty;
        SteamUrl = string.Empty;
        About = string.Empty;
        PreviousExperience = string.Empty;
        ReasonToJoin = string.Empty;
        WhatCanYouOffer = string.Empty;
        HowDidYouFindUs = string.Empty;
    }
}