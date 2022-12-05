using System;
using System.Linq;
using DestroyKingdom.Extensions;
using DestroyKingdom.Shared;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Actions.KingdomAnnexation;

public static class ClansCollaborationChanceCalculator
{
    public static int GetChance(Kingdom annexedKingdom, Kingdom annexingKingdom)
    {
        var annexingKingdomRulingClan = annexingKingdom.RulingClan;
        if (annexingKingdomRulingClan == null) return 0;

        var averageRelation = annexedKingdom.VassalClans()
            .Sum(clan => AverageRelationWithRulers(clan, annexedKingdom, annexingKingdom));
        var averageRelationWithOffset = Math.Max(averageRelation + 50, 0);
        var relationFactor = (float)averageRelationWithOffset / 150;
        var relationFactorRange = Settings.MaximumCollaborationChance >= Settings.MinimumCollaborationChance
            ? Settings.MaximumCollaborationChance - Settings.MinimumCollaborationChance
            : 0;
        return (int)(Settings.MinimumCollaborationChance + relationFactorRange * relationFactor);
    }

    private static int AverageRelationWithRulers(Clan clan, Kingdom annexedKingdom, Kingdom annexingKingdom)
    {
        return (clan.GetRelationWithClan(annexedKingdom.RulingClan) +
                clan.GetRelationWithClan(annexingKingdom.RulingClan)) / 2 / annexedKingdom.VassalClans().Count;
    }
}