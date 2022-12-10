using System;
using System.Diagnostics.CodeAnalysis;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace DestroyKingdom.Shared;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
internal class Settings : AttributeGlobalSettings<Settings>
{
    private const string GroupNameAnnexationConditions = "Annexation conditions";
    private const int DefaultAnnexedKingdomMaxFiefsAmounts = 0;
    private const float DefaultAnnexedKingdomMaxStrengthRatio = .25f;
    private const float DefaultAnnexingKingdomMinCultureFiefsPercentage = .4f;
    private const string GroupNameReducedExecutionRelationshipPenalty = "Reduced rebel execution relationship penalty";
    private const int DefaultRebelExecutionRelationPenaltyDenominatorAnnexing = 5;
    private const int DefaultRebelExecutionRelationPenaltyDenominatorOthers = 3;
    private const string GroupNameCollaborationChances = "Chances of collaboration";
    private const float DefaultMinimumCollaborationChance = .1f;
    private const float DefaultMaximumCollaborationChance = .5f;
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
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
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
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
    private float AnnexedKingdomMaxStrengthRatioInternal { get; set; } = DefaultAnnexedKingdomMaxStrengthRatio;

    public static int AnnexedKingdomMaxStrengthRatio => (int)Math.Round(
        (Instance?.AnnexedKingdomMaxStrengthRatioInternal ?? DefaultAnnexedKingdomMaxStrengthRatio) * 100, 0
    );

    [SettingPropertyFloatingInteger(
        displayName: "Minimum culture fiefs",
        minValue: .0f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 2,
        RequireRestart = false,
        HintText =
            "Kingdom can annex other kingdom only if percentage of fiefs that have annexed kingdom's culture controlled by this annexing kingdom is at least as big as this value"
    )]
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
    private float AnnexingKingdomMinCultureFiefsPercentageInternal { get; set; } =
        DefaultAnnexingKingdomMinCultureFiefsPercentage;

    public static int AnnexingKingdomMinCultureFiefsPercentage => (int)Math.Round(
        (Instance?.AnnexingKingdomMinCultureFiefsPercentageInternal ??
         DefaultAnnexingKingdomMinCultureFiefsPercentage) * 100, 0
    );

    [SettingPropertyInteger(
        displayName: "Execution relation penalty denominator (annexing kingdom)",
        minValue: 1,
        maxValue: 10,
        Order = 3,
        RequireRestart = false,
        HintText =
            "Relation penalty with nobles of annexing kingdom after executing rebel nobles of annexed kingdom will be divided by this number"
    )]
    [SettingPropertyGroup(GroupNameReducedExecutionRelationshipPenalty)]
    private int RebelExecutionRelationPenaltyDenominatorAnnexingInternal { get; set; } =
        DefaultRebelExecutionRelationPenaltyDenominatorAnnexing;

    public static int RebelExecutionRelationPenaltyDenominatorAnnexing =>
        Instance?.RebelExecutionRelationPenaltyDenominatorAnnexingInternal ??
        DefaultRebelExecutionRelationPenaltyDenominatorAnnexing;

    [SettingPropertyInteger(
        displayName: "Execution relation penalty denominator (other kingdoms)",
        minValue: 1,
        maxValue: 10,
        Order = 4,
        RequireRestart = false,
        HintText =
            "Relation penalty with nobles of other kingdoms after executing rebel nobles of annexed kingdom will be divided by this number"
    )]
    [SettingPropertyGroup(GroupNameReducedExecutionRelationshipPenalty)]
    private int RebelExecutionRelationPenaltyDenominatorOthersInternal { get; set; } =
        DefaultRebelExecutionRelationPenaltyDenominatorOthers;

    public static int RebelExecutionRelationPenaltyDenominatorOthers =>
        Instance?.RebelExecutionRelationPenaltyDenominatorOthersInternal ??
        DefaultRebelExecutionRelationPenaltyDenominatorOthers;

    [SettingPropertyFloatingInteger(
        displayName: "Minimum chance of collaboration",
        minValue: .1f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 5,
        RequireRestart = false,
        HintText =
            "Minimum chance of vassal clans of annexed kingdom to join your kingdom after annexation (doesn't apply to ruler which will always join and mercenaries that will just terminate their contract)."
    )]
    [SettingPropertyGroup(GroupNameCollaborationChances)]
    private float MinimumCollaborationChanceInternal { get; set; } = DefaultMinimumCollaborationChance;

    public static int MinimumCollaborationChance => (int)Math.Round(
        (Instance?.MinimumCollaborationChanceInternal ?? DefaultMinimumCollaborationChance) * 100, 0
    );

    [SettingPropertyFloatingInteger(
        displayName: "Maximum chance of collaboration",
        minValue: .1f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 6,
        RequireRestart = false,
        HintText =
            "Maximum chance of vassal clans of annexed kingdom to join your kingdom after annexation - the better your relations with lords of the kingdom are, the higher the chance they will join you. This value will be ignored if it's lower than minimum one."
    )]
    [SettingPropertyGroup(GroupNameCollaborationChances)]
    private float MaximumCollaborationChanceInternal { get; set; } = DefaultMaximumCollaborationChance;

    public static int MaximumCollaborationChance => (int)Math.Round(
        (Instance?.MaximumCollaborationChanceInternal ?? DefaultMaximumCollaborationChance) * 100, 0
    );
}