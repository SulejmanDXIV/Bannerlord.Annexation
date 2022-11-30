using DestroyKingdom.Extensions;
using DestroyKingdom.Shared;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Conditions;

public static class KingdomAnnexationCondition
{
    public static bool ControllingEnoughCultureLands(
        Kingdom? annexingKingdom,
        Kingdom? annexedKingdom
    )
    {
        return ControllingEnoughCultureLands(
            annexingKingdom: annexingKingdom,
            annexedKingdom: annexedKingdom,
            controlledFiefsPercentage: out _,
            requiredFiefsPercentage: out _
        );
    }

    public static bool ControllingEnoughCultureLands(
        Kingdom? annexingKingdom,
        Kingdom? annexedKingdom,
        out int controlledFiefsPercentage,
        out int requiredFiefsPercentage
    )
    {
        var annexedKingdomCulture = annexedKingdom?.Culture;
        controlledFiefsPercentage = 0;
        requiredFiefsPercentage = Settings.AnnexingKingdomMinCultureFiefsPercentage;
        if (annexedKingdomCulture == null || annexingKingdom == null) return false;
        var controlledCultureFiefsPercentage = TownExtensions.KingdomControlledCultureFiefsPercentage(
            annexingKingdom,
            annexedKingdomCulture
        );
        var controllingEnoughFiefs = controlledCultureFiefsPercentage >= requiredFiefsPercentage;
        controlledFiefsPercentage = controlledCultureFiefsPercentage;
        return controllingEnoughFiefs;
    }
}