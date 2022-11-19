// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace DestroyKingdom.Extensions;

public static class TownExtensions
{
    public static int KingdomControlledCultureFiefsPercentage(Kingdom kingdom, CultureObject culture)
    {
        var cultureFiefs = CultureFiefs(culture);
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