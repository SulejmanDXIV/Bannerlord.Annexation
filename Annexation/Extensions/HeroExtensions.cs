// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

using TaleWorlds.CampaignSystem;

namespace Annexation.Extensions;

public static class HeroExtensions
{
    public static bool IsRulerOfKingdom(this Hero hero)
    {
        var kingdom = hero.Clan.Kingdom;
        return kingdom.IsKingdomFaction && kingdom.Leader == hero;
    }
}