// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

using System.Linq;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Extensions;

public static class KingdomExtensions
{
    public static int VassalClansCount(this Kingdom kingdom)
    {
        return kingdom.Clans.Count((clan) => clan.IsNoble) - 1;
    }

    public static int KingdomsStrengthRatio(Kingdom kingdom, Kingdom other)
    {
        var strengthRatio = (int)(kingdom.TotalStrength * 100 / other.TotalStrength);
        return strengthRatio;
    }
}