using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Extensions;

public static class HeroExtensions
{
    public static bool IsRulerOfKingdom(this Hero hero)
    {
        return hero.Clan?.Kingdom?.Leader == hero;
    }
}