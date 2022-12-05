using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Extensions;

public static class KingdomExtensions
{
    public static List<Kingdom> AllActiveKingdomsFactions()
    {
        return Kingdom.All.Where((kingdom) => kingdom.IsKingdomFaction && !kingdom.IsEliminated).ToList();
    }

    public static List<Clan> VassalClans(this Kingdom kingdom)
    {
        return kingdom.Clans.Where(clan => clan.IsNoble && clan != kingdom.RulingClan && !clan.IsEliminated).ToList();
    }

    public static int KingdomsStrengthRatio(Kingdom kingdom, Kingdom other)
    {
        var strengthRatio = (int)(kingdom.TotalStrength * 100 / other.TotalStrength);
        return strengthRatio;
    }
}