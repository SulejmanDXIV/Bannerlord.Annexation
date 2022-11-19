using DestroyKingdom.Extensions;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Conditions;

public static class KingdomAnnexationCondition
{
    public const int RequiredStrengthRatio = 25;
    private const int DefaultRequiredFiefsPercentage = 40;
    private const int EmpireRequiredFiefsPercentage = 45;

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
        requiredFiefsPercentage = DefaultRequiredFiefsPercentage;
        if (annexedKingdomCulture == null || annexingKingdom == null) return false;
        var controlledCultureFiefsPercentage = TownExtensions.KingdomControlledCultureFiefsPercentage(
            annexingKingdom,
            annexedKingdomCulture
        );
        requiredFiefsPercentage = annexedKingdomCulture.StringId == "empire"
            ? EmpireRequiredFiefsPercentage
            : DefaultRequiredFiefsPercentage;
        var controllingEnoughFiefs = controlledCultureFiefsPercentage >= requiredFiefsPercentage;
        if (!controllingEnoughFiefs)
            controlledFiefsPercentage = controlledCultureFiefsPercentage;
        return controllingEnoughFiefs;
    }
}