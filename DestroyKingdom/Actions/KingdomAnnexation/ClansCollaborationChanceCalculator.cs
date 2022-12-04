using System;
using System.Linq;
using DestroyKingdom.Extensions;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Actions.KingdomAnnexation;

public static class ClansCollaborationChanceCalculator
{
    private const int BaseValue = 10;
    private const int RelationFactorRange = 40;

    public static int GetChance(Kingdom annexedKingdom, Kingdom annexingKingdom)
    {
        var annexingKingdomRulingClan = annexingKingdom.RulingClan;
        if (annexingKingdomRulingClan == null) return 0;

        var averageRelation = annexedKingdom.VassalClans()
            .Sum(clan => clan.GetRelationWithClan(annexingKingdomRulingClan)) / annexedKingdom.VassalClans().Count;
        var averageRelationWithOffset = Math.Max(averageRelation + 50, 0);
        var relationFactor = (float)averageRelationWithOffset / 150;
        return (int)(BaseValue + RelationFactorRange * relationFactor);
    }
}