using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Conditions;

public static class PlayerTraitCondition
{
    public static bool Fearless()
    {
        return PlayerTraitConditionInternal(Hero.MainHero.GetHeroTraits().Valor);
    }

    public static bool Generous()
    {
        return PlayerTraitConditionInternal(Hero.MainHero.GetHeroTraits().Generosity);
    }

    public static bool Cruel()
    {
        return PlayerTraitConditionInternal(Hero.MainHero.GetHeroTraits().Mercy, negativeTrait: true);
    }

    public static bool Honorable()
    {
        return PlayerTraitConditionInternal(Hero.MainHero.GetHeroTraits().Honor);
    }

    private static bool PlayerTraitConditionInternal(int traitLevel, bool negativeTrait = false)
    {
        var hasEnoughTraitLevel = negativeTrait ? traitLevel < 0 : traitLevel > 0;
        return hasEnoughTraitLevel;
    }
}