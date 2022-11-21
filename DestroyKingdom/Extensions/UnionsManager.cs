using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace DestroyKingdom.Extensions;

public static class UnionsManager
{
    private static readonly List<List<Kingdom>> CurrentUnions = new();

    public static void CreateUnion(List<Kingdom> kingdoms)
    {
        CurrentUnions.Add(kingdoms);
    }

    public static List<Kingdom> GetUnionMembers(Kingdom queriedKingdom)
    {
        return CurrentUnions
            .Where((kingdoms) => kingdoms.Contains(queriedKingdom))
            .SelectMany((kingdoms) => kingdoms)
            .Distinct()
            .Where((kingdom) => kingdom != queriedKingdom)
            .ToList();
    }
}