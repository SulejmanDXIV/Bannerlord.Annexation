using DestroyKingdom.Actions.KingdomAnnexation;
using DestroyKingdom.Conditions;
using DestroyKingdom.Extensions;
using DestroyKingdom.Shared;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace DestroyKingdom.CampaignBehaviors
{
    internal class PlayerRulerAnnexationConversationCampaignBehavior : CampaignBehaviorBase
    {
        private const string AnnexationRequestedToken = "annexation_requested";
        private const string FirstReasonToken = "annexation_give_first_reason";
        private const string AfterFirstReasonToken = "annexation_need_second_reason";
        private const string SecondReasonToken = "annexation_give_second_reason";
        private const string AfterSecondReasonToken = "annexation_need_third_reason";
        private const string ThirdReasonToken = "annexation_give_third_reason";
        private const string AfterThirdReasonToken = "annexation_need_fourth_reason";
        private const string FourthReasonToken = "annexation_give_fourth_reason";
        private const string OathStartToken = "annexation_oath";

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddDialogues);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private static void AddDialogues(CampaignGameStarter starter)
        {
            starter.AddPlayerLine(
                id: "annexation_demand",
                inputToken: NativeGameTokens.HeroMainOptions,
                outputToken: AnnexationRequestedToken,
                text: "Sizi ve imparatorluğunuz olan {HERO_KINGDOM} ile {HERO_KINGDOM_TITLE} of {HERO_KINGDOM_CULTURE} olarak otoritemi tanımanızı talep ediyorum.",
                condition: PlayerIsRulerAndHeroIsRulerCondition
            );
            starter.AddDialogLine(
                id: "annexation_ask_first_reason",
                inputToken: AnnexationRequestedToken,
                outputToken: FirstReasonToken,
                text: "Bunu neden yapayım ?"
            );
            starter.AddPlayerLine(
                id: "annexation_strength",
                inputToken: FirstReasonToken,
                outputToken: AfterFirstReasonToken,
                text: "Çünkü biz {PLAYER_KINGDOM} olarak sizden daha güçlüyüz.",
                clickableCondition: HeroKingdomStrengthClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_no_reasons",
                inputToken: FirstReasonToken,
                outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Boş ver
            );
            starter.AddDialogLine(
                id: "annexation_ask_second_reason",
                inputToken: AfterFirstReasonToken,
                outputToken: SecondReasonToken,
                text: "Bu doğru, ancak bu tür bir karar için daha fazla nedene ihtiyacım var."
            );
            starter.AddPlayerLine(
                id: "annexation_low_fiefs",
                inputToken: SecondReasonToken,
                outputToken: AfterSecondReasonToken,
                text: "{HERO_KINGDOM} {LOW_FIEFS_DESCRIPTION}.",
                clickableCondition: HeroKingdomLowFiefsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_only_one_reason",
                inputToken: SecondReasonToken,
                outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Boş ver
            );
            starter.AddDialogLine(
                id: "annexation_ask_third_reason",
                inputToken: AfterSecondReasonToken,
                outputToken: ThirdReasonToken,
                text: "Ama neden bizi yöneten siz olmalısınız"
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control",
                inputToken: ThirdReasonToken,
                outputToken: AfterThirdReasonToken,
                text: "Eski yerlerinizin yeni hakimi biz olduğumuz için.",
                condition: PlayerAnyTraitsCondition,
                clickableCondition: PlayerControllingCultureTownsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control_no_traits",
                inputToken: ThirdReasonToken,
                outputToken: OathStartToken,
                text: "Eski yerlerinizin yeni hakimi biz olduğumuz için.",
                condition: PlayerNoTraitsCondition,
                clickableCondition: PlayerControllingCultureTownsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_only_two_reasons",
                inputToken: ThirdReasonToken, outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Boş ver
            );
            starter.AddDialogLine(
                id: "annexation_ask_fourth_reason",
                inputToken: AfterThirdReasonToken,
                outputToken: FourthReasonToken,
                text: "Devam et."
            );
            starter.AddPlayerLine(
                id: "annexation_honor",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "Sözüme güvenebilirsin, sizinle ve kültürünüzle ilgili olacağım.",
                condition: PlayerTraitCondition.Honorable
            );
            starter.AddPlayerLine(
                id: "annexation_cruel",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "Otoritemi tanımazsanız sizi ve tüm destekçilerinizi öldürürüm.",
                condition: PlayerTraitCondition.Cruel
            );
            starter.AddPlayerLine(
                id: "annexation_generous",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "Bana sadık kalan herkesi cömertçe ödüllendireceğim.",
                condition: PlayerTraitCondition.Generous
            );
            starter.AddPlayerLine(
                id: "annexation_fearless",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "Savaş meydanlarında korkusuz bir lider olarak değerimi birçok kez gösterdim.",
                condition: PlayerTraitCondition.Fearless
            );
            starter.AddPlayerLine(
                id: "annexation_only_three_reasons",
                inputToken: FourthReasonToken,
                outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Boş ver
            );
            starter.AddDialogLine(
                id: "annexation_oath_text",
                inputToken: OathStartToken,
                outputToken: "annexation_oath_2",
                text:
                "Bu zor bir karar, ancak tüm koşulları düşündükten sonra yapabileceğim tek şey bu."
            );
            starter.AddDialogLine(
                id: "annexation_oath_text_2",
                inputToken: "annexation_oath_2",
                outputToken: "annexation_oath_3",
                text: "Benim hükümdarım olarak sana hürmet ettiğime yemin ederim.");
            starter.AddDialogLine(
                id: "annexation_oath_text_3",
                inputToken: "annexation_oath_3",
                outputToken: "annexation_oath_4",
                text: "Kılıcıma ihtiyacın olursa düşmanlarınla savaşmak için yanında olacağım.");
            starter.AddDialogLine(
                id: "annexation_oath_text_4",
                inputToken: "annexation_oath_4",
                outputToken: "annexation_oath_5",
                text: "Ben de sizin ve mirasçılarınızın haklarını savunacağım.");
            starter.AddDialogLine(
                id: "annexation_oath_text_5",
                inputToken: "annexation_oath_5",
                outputToken: NativeGameTokens.LordPreTalk,
                text: "Sen artık buralısın.",
                consequence: KingdomAnnexationAction.ApplyByPlayerConversation
            );
        }

        private static void SetTextVariables()
        {
            var heroKingdom = Hero.OneToOneConversationHero.Clan?.Kingdom;
            var playerKingdom = Hero.MainHero.Clan?.Kingdom;
            if (heroKingdom == null || playerKingdom == null) return;
            var lowFiefsDescription = heroKingdom.Fiefs.IsEmpty() ? "olarak hiç tımarınız yok" : "olarak az tımarınız var";
            MBTextManager.SetTextVariable("HERO_KINGDOM_TITLE", GetHeroFactionRulerText());
            MBTextManager.SetTextVariable("PLAYER_KINGDOM", playerKingdom.Name);
            MBTextManager.SetTextVariable("HERO_KINGDOM", heroKingdom.Name);
            MBTextManager.SetTextVariable("HERO_KINGDOM_CULTURE", heroKingdom.Culture.Name);
            MBTextManager.SetTextVariable("LOW_FIEFS_DESCRIPTION", lowFiefsDescription);
            
        }

        private static bool PlayerNoTraitsCondition()
        {
            return !PlayerAnyTraitsCondition();
        }

        private static bool PlayerAnyTraitsCondition()
        {
            return PlayerTraitCondition.Fearless() ||
                   PlayerTraitCondition.Generous() ||
                   PlayerTraitCondition.Cruel() ||
                   PlayerTraitCondition.Honorable();
        }

        private static TextObject GetHeroFactionRulerText()
        {
            var playerGenderSuffix = Hero.MainHero.IsFemale ? "_f" : "";
            return GameTexts.FindText("str_faction_ruler",
                Hero.OneToOneConversationHero.Clan?.Kingdom?.Culture?.StringId?.Add(playerGenderSuffix, false)
            );
        }

        private static bool HeroKingdomStrengthClickableCondition(out TextObject? explanation)
        {
            var heroKingdom = Hero.OneToOneConversationHero?.Clan?.Kingdom;
            var playerKingdom = Hero.MainHero?.Clan?.Kingdom;
            explanation = null;
            if (heroKingdom == null || playerKingdom == null) return false;
            var strengthRatio = KingdomExtensions.KingdomsStrengthRatio(heroKingdom, playerKingdom);
            var enoughStrengthAdvantage = strengthRatio < Settings.AnnexedKingdomMaxStrengthRatio;
            if (strengthRatio > 100)
                explanation = new TextObject("{HERO_KINGDOM} sizden daha güçlü.");
            else if (!enoughStrengthAdvantage)
                explanation = new TextObject(
                    $"{heroKingdom.Name}, {playerKingdom.Name} gücünün %{strengthRatio} miktarına sahip. %{Settings.AnnexedKingdomMaxStrengthRatio} değerinden az olması gerekir."
                );
            return enoughStrengthAdvantage;
        }

        private static bool PlayerControllingCultureTownsClickableCondition(out TextObject? explanation)
        {
            explanation = null;
            var controllingEnoughFiefs = KingdomAnnexationCondition.ControllingEnoughCultureLands(
                annexingKingdom: Hero.MainHero?.Clan?.Kingdom,
                annexedKingdom: Hero.OneToOneConversationHero?.Clan?.Kingdom,
                controlledFiefsPercentage: out var playerControlledHeroCultureFiefsPercentage,
                requiredFiefsPercentage: out var requiredFiefsPercentage
            );
            if (!controllingEnoughFiefs)
            {
                var cultureName = Hero.OneToOneConversationHero?.Clan?.Kingdom?.Culture?.Name ?? TextObject.Empty;
                explanation = new TextObject(
                    $"{cultureName} tımarının %{playerControlledHeroCultureFiefsPercentage} kadarını kontrol ediyorsunuz, (%{requiredFiefsPercentage} gerekli)."
                );
            }

            return controllingEnoughFiefs;
        }

        private static bool HeroKingdomLowFiefsClickableCondition(out TextObject? explanation)
        {
            var heroKingdom = Hero.OneToOneConversationHero?.Clan?.Kingdom;
            explanation = null;
            if (heroKingdom == null) return false;
            var fiefsCount = heroKingdom.Fiefs.Count;
            var maxFiefsCount = Settings.AnnexedKingdomMaxFiefsAmount;
            var lowOnFiefs = fiefsCount <= maxFiefsCount;
            if (!lowOnFiefs)
            {
                explanation =
                    new TextObject($"{heroKingdom.Name}, {fiefsCount} tımar kontrol ediyor (maximum {maxFiefsCount}).");
            }

            return lowOnFiefs;
        }

        private static bool PlayerIsRulerAndHeroIsRulerCondition()
        {
            var areRulers = Hero.OneToOneConversationHero.IsRulerOfKingdom()
                            && Hero.MainHero.IsRulerOfKingdom();
            if (areRulers) SetTextVariables();
            return areRulers;
        }
    }
}