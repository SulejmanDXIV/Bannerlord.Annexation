using System;
using System.Diagnostics.CodeAnalysis;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace DestroyKingdom.Shared;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
internal class Settings : AttributeGlobalSettings<Settings>
{
    private const int DefaultAnnexedKingdomMaxFiefsAmounts = 0;
    private const float DefaultAnnexedKingdomMaxStrengthRatio = .25f;
    private const float DefaultAnnexingKingdomMinCultureFiefsPercentage = .4f;
    public override string Id => "Annexation_Settings";
    public override string DisplayName => "Annexation";
    public override string FolderName => "Annexation";
    public override string FormatType => "json2";

    [SettingPropertyInteger(
        displayName: "Maximum amount of fiefs of annexed kingdom",
        minValue: 0,
        maxValue: 5,
        Order = 0,
        RequireRestart = false,
        HintText = "Kingdom can be annexed only if it has less fiefs than configured by this value"
    )]
    [SettingPropertyGroup("Annexation conditions")]
    private int AnnexedKingdomMaxFiefsAmountInternal { get; set; } = DefaultAnnexedKingdomMaxFiefsAmounts;

    public static int AnnexedKingdomMaxFiefsAmount =>
        Instance?.AnnexedKingdomMaxFiefsAmountInternal ?? DefaultAnnexedKingdomMaxFiefsAmounts;

    [SettingPropertyFloatingInteger(
        displayName: "Maximum relative strength percentage of annexed kingdom",
        minValue: .1f,
        maxValue: .5f,
        valueFormat: "0%",
        Order = 1,
        RequireRestart = false,
        HintText =
            "Kingdom can be annexed only if it it's total strength divided by your annexing kingdom strength is smaller than this value"
    )]
    [SettingPropertyGroup("Annexation conditions")]
    private float AnnexedKingdomMaxStrengthRatioInternal { get; set; } = DefaultAnnexedKingdomMaxStrengthRatio;

    public static int AnnexedKingdomMaxStrengthRatio => (int)Math.Round(
        (Instance?.AnnexedKingdomMaxStrengthRatioInternal ?? DefaultAnnexedKingdomMaxStrengthRatio) * 100, 0
    );

    [SettingPropertyFloatingInteger(
        displayName: "Minimum culture fiefs",
        minValue: .25f,
        maxValue: .7f,
        valueFormat: "0%",
        Order = 2,
        RequireRestart = false,
        HintText =
            "Kingdom can annex other kingdom only if percentage of fiefs that have annexed kingdom's culture controlled by this annexing kingdom is at least as big as this value"
    )]
    [SettingPropertyGroup("Annexation conditions")]
    private float AnnexingKingdomMinCultureFiefsPercentageInternal { get; set; } =
        DefaultAnnexingKingdomMinCultureFiefsPercentage;

    public static int AnnexingKingdomMinCultureFiefsPercentage => (int)Math.Round(
        (Instance?.AnnexingKingdomMinCultureFiefsPercentageInternal ??
         DefaultAnnexingKingdomMinCultureFiefsPercentage) * 100, 0
    );
}