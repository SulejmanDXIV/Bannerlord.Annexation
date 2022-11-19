using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace DestroyKingdom.Extensions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class TownExtensions
{
    public static int KingdomControlledCultureFiefsPercentage(Kingdom kingdom, CultureObject culture)
    {
        var cultureFiefs = CultureFiefs(culture);
        if (cultureFiefs.Count == 0) return 100;
        var kingdomControlledCultureFiefs = cultureFiefs
            .Where((fief) => fief.OwnerClan?.Kingdom == kingdom).ToList();
        return kingdomControlledCultureFiefs.Count() * 100 / cultureFiefs.Count;
    }


    public static List<Town> KingdomControlledCultureFiefs(Kingdom kingdom, CultureObject culture)
    {
        return CultureFiefs(culture)
            .Where((fief) => fief.OwnerClan?.Kingdom == kingdom).ToList();
    }

    public static List<Town> CultureFiefs(CultureObject culture)
    {
        return Town.AllFiefs.Where((fief) => fief.Culture == culture).ToList();
    }
}