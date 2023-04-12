using System;
using System.Diagnostics.CodeAnalysis;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace DestroyKingdom.Shared;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
internal class Settings : AttributeGlobalSettings<Settings>
{
    private const string GroupNameAnnexationConditions = "İlhak Etme Koşulu";
    private const int DefaultAnnexedKingdomMaxFiefsAmounts = 0;
    private const float DefaultAnnexedKingdomMaxStrengthRatio = .25f;
    private const float DefaultAnnexingKingdomMinCultureFiefsPercentage = .4f;
    private const string GroupNameReducedExecutionRelationshipPenalty = "İnfaz Sonucunda İlişki Cezası Oranı";
    private const int DefaultRebelExecutionRelationPenaltyDenominatorAnnexing = 5;
    private const int DefaultRebelExecutionRelationPenaltyDenominatorOthers = 3;
    private const string GroupNameCollaborationChances = "Karşı Tarafla Anlaşma Şansı";
    private const float DefaultMinimumCollaborationChance = .1f;
    private const float DefaultMaximumCollaborationChance = .5f;
    public override string Id => "Annexation_Settings";
    public override string DisplayName => "Annexation";
    public override string FolderName => "Annexation";
    public override string FormatType => "json2";

    [SettingPropertyInteger(
        displayName: "İlhak edilmiş krallığın en fazla tımar miktarı",
        minValue: 0,
        maxValue: 5,
        Order = 0,
        RequireRestart = false,
        HintText = "Krallık, yalnızca bu değerden daha az tımara sahipse ilhak edilebilir."
    )]
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
    private int AnnexedKingdomMaxFiefsAmountInternal { get; set; } = DefaultAnnexedKingdomMaxFiefsAmounts;

    public static int AnnexedKingdomMaxFiefsAmount =>
        Instance?.AnnexedKingdomMaxFiefsAmountInternal ?? DefaultAnnexedKingdomMaxFiefsAmounts;

    [SettingPropertyFloatingInteger(
        displayName: "İlhak edilmiş krallığın maksimum güç yüzdesi",
        minValue: .1f,
        maxValue: .5f,
        valueFormat: "0%",
        Order = 1,
        RequireRestart = false,
        HintText =
            "İlhak edilmek istenen bir krallık, yalnızca ilhak etmeye çalışan krallığın gücüne bölündüğünde belirli bir değerden daha küçükse ilhak edilebilir."
    )]
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
    private float AnnexedKingdomMaxStrengthRatioInternal { get; set; } = DefaultAnnexedKingdomMaxStrengthRatio;

    public static int AnnexedKingdomMaxStrengthRatio => (int)Math.Round(
        (Instance?.AnnexedKingdomMaxStrengthRatioInternal ?? DefaultAnnexedKingdomMaxStrengthRatio) * 100, 0
    );

    [SettingPropertyFloatingInteger(
        displayName: "Size ait olan minimum tımar",
        minValue: .0f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 2,
        RequireRestart = false,
        HintText =
            "Bir krallık, diğer bir krallığı ilhak etmek istiyorsa, ilhak etmeye çalışan krallığın kontrolündeki tımar yüzdesinin belirli bir değerin altında olmaması gerekiyor."
    )]
    [SettingPropertyGroup(GroupNameAnnexationConditions)]
    private float AnnexingKingdomMinCultureFiefsPercentageInternal { get; set; } =
        DefaultAnnexingKingdomMinCultureFiefsPercentage;

    public static int AnnexingKingdomMinCultureFiefsPercentage => (int)Math.Round(
        (Instance?.AnnexingKingdomMinCultureFiefsPercentageInternal ??
         DefaultAnnexingKingdomMinCultureFiefsPercentage) * 100, 0
    );

    [SettingPropertyInteger(
        displayName: "İlhak edilen krallığa karşı yapılan infazın ilişki cezası oranı",
        minValue: 1,
        maxValue: 10,
        Order = 3,
        RequireRestart = false,
        HintText =
            "İlhak edilen krallığın isyancı soylularının infazından sonra, ilhak eden krallığın soylularıyla olan ilişki cezası bu orana göre hesaplanacaktır."
    )]
    [SettingPropertyGroup(GroupNameReducedExecutionRelationshipPenalty)]
    private int RebelExecutionRelationPenaltyDenominatorAnnexingInternal { get; set; } =
        DefaultRebelExecutionRelationPenaltyDenominatorAnnexing;

    public static int RebelExecutionRelationPenaltyDenominatorAnnexing =>
        Instance?.RebelExecutionRelationPenaltyDenominatorAnnexingInternal ??
        DefaultRebelExecutionRelationPenaltyDenominatorAnnexing;

    [SettingPropertyInteger(
        displayName: "Diğer krallıklara karşı yapılan infazın ilişki cezası oranı",
        minValue: 1,
        maxValue: 10,
        Order = 4,
        RequireRestart = false,
        HintText =
            "İlhak edilen krallığın isyancı soylularının infaz edilmesinden sonra, diğer krallıkların soylularıyla olan ilişki cezaları bu sayıya göre hesaplanacaktır."
    )]
    [SettingPropertyGroup(GroupNameReducedExecutionRelationshipPenalty)]
    private int RebelExecutionRelationPenaltyDenominatorOthersInternal { get; set; } =
        DefaultRebelExecutionRelationPenaltyDenominatorOthers;

    public static int RebelExecutionRelationPenaltyDenominatorOthers =>
        Instance?.RebelExecutionRelationPenaltyDenominatorOthersInternal ??
        DefaultRebelExecutionRelationPenaltyDenominatorOthers;

    [SettingPropertyFloatingInteger(
        displayName: "Minimum anlaşma şansı",
        minValue: .1f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 5,
        RequireRestart = false,
        HintText =
            "İşgal edilen krallığın vasal klanları, ilhak işleminden sonra senin krallığına katılma olasılığı düşüktür (ilhak edilen yönetici dışında her zaman ve sözleşmeleri sona erecek olan paralı askerler hariç)."
    )]
    [SettingPropertyGroup(GroupNameCollaborationChances)]
    private float MinimumCollaborationChanceInternal { get; set; } = DefaultMinimumCollaborationChance;

    public static int MinimumCollaborationChance => (int)Math.Round(
        (Instance?.MinimumCollaborationChanceInternal ?? DefaultMinimumCollaborationChance) * 100, 0
    );

    [SettingPropertyFloatingInteger(
        displayName: "Maksimum anlaşma şansı",
        minValue: .1f,
        maxValue: 1f,
        valueFormat: "0%",
        Order = 6,
        RequireRestart = false,
        HintText =
            "İlhak edilen krallığın vassal klanlarının sizin krallığınıza katılma olasılığı maksimum değeri - krallarla olan ilişkilerinizin ne kadar iyi olduğuna bağlı olarak, katılma ihtimalleri de artacaktır. Bu özellik, minimum değerin altındaysa dikkate alınmayacaktır."
    )]
    [SettingPropertyGroup(GroupNameCollaborationChances)]
    private float MaximumCollaborationChanceInternal { get; set; } = DefaultMaximumCollaborationChance;

    public static int MaximumCollaborationChance => (int)Math.Round(
        (Instance?.MaximumCollaborationChanceInternal ?? DefaultMaximumCollaborationChance) * 100, 0
    );
}