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
                text: "I, the rightful {PLAYER_KINGDOM_TITLE} of {PLAYER_KINGDOM}, request that you and {HERO_KINGDOM} acknowledge my sovereignty as {HERO_KINGDOM_TITLE} of {HERO_KINGDOM_CULTURE}.",
                condition: PlayerIsRulerAndHeroIsRulerCondition
            );
            starter.AddDialogLine(
                id: "annexation_ask_first_reason",
                inputToken: AnnexationRequestedToken,
                outputToken: FirstReasonToken,
                text: "What is your reasoning for this request?"
            );
            starter.AddPlayerLine(
                id: "annexation_strength",
                inputToken: FirstReasonToken,
                outputToken: AfterFirstReasonToken,
                text: "{PLAYER_KINGDOM} holds a position of strength, and it would be in your best interest to align with us rather than risk our hostility.",
                clickableCondition: HeroKingdomStrengthClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_no_reasons",
                inputToken: FirstReasonToken,
                outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Nevermind
            );
            starter.AddDialogLine(
                id: "annexation_ask_second_reason",
                inputToken: AfterFirstReasonToken,
                outputToken: SecondReasonToken,
                text: "While I cannot deny the power of {PLAYER_KINGDOM}, I require further persuasion before making such a significant decision."
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
                text: SharedTexts.Nevermind
            );
            starter.AddDialogLine(
                id: "annexation_ask_third_reason",
                inputToken: AfterSecondReasonToken,
                outputToken: ThirdReasonToken,
                text: "But why should you, specifically, be the one to rule over the {HERO_KINGDOM_CULTURE} people?"
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control",
                inputToken: ThirdReasonToken,
                outputToken: AfterThirdReasonToken,
                text: "{PLAYER_KINGDOM} holds a large portion of {HERO_KINGDOM_CULTURE} lands under its control.",
                condition: PlayerAnyTraitsCondition,
                clickableCondition: PlayerControllingCultureTownsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control_no_traits",
                inputToken: ThirdReasonToken,
                outputToken: OathStartToken,
                text: "{PLAYER_KINGDOM} holds a large portion of {HERO_KINGDOM_CULTURE} lands under its control.",
                condition: PlayerNoTraitsCondition,
                clickableCondition: PlayerControllingCultureTownsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_only_two_reasons",
                inputToken: ThirdReasonToken, outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Nevermind
            );
            starter.AddDialogLine(
                id: "annexation_ask_fourth_reason",
                inputToken: AfterThirdReasonToken,
                outputToken: FourthReasonToken,
                text: "Please continue."
            );
            starter.AddPlayerLine(
                id: "annexation_honor",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "You may trust in my word - the prosperity of the {HERO_KINGDOM_CULTURE} will be my foremost concern.",
                condition: PlayerTraitCondition.Honorable
            );
            starter.AddPlayerLine(
                id: "annexation_cruel",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "Should you refuse to acknowledge my rightful rule, I shall see to it that you and all your supporters meet their demise.",
                condition: PlayerTraitCondition.Cruel
            );
            starter.AddPlayerLine(
                id: "annexation_generous",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "I shall generously reward those who remain loyal to me.",
                condition: PlayerTraitCondition.Generous
            );
            starter.AddPlayerLine(
                id: "annexation_fearless",
                inputToken: FourthReasonToken,
                outputToken: OathStartToken,
                text: "I have proven myself a valiant and fearless leader on numerous occasions on the fields of battle.",
                condition: PlayerTraitCondition.Fearless
            );
            starter.AddPlayerLine(
                id: "annexation_only_three_reasons",
                inputToken: FourthReasonToken,
                outputToken: NativeGameTokens.LordPreTalk,
                text: SharedTexts.Nevermind
            );
            starter.AddDialogLine(
                id: "annexation_oath_text",
                inputToken: OathStartToken,
                outputToken: "annexation_oath_2",
                text:
                "This is a difficult decision, but after careful consideration of all the circumstances, it is the only course of action available to me."
            );
            starter.AddDialogLine(
                id: "annexation_oath_text_2",
                inputToken: "annexation_oath_2",
                outputToken: "annexation_oath_3",
                text: "I swear homage to you as lawful liege of mine.");
            starter.AddDialogLine(
                id: "annexation_oath_text_3",
                inputToken: "annexation_oath_3",
                outputToken: "annexation_oath_4",
                text: "I will be at your side to fight your enemies should you need my sword.");
            starter.AddDialogLine(
                id: "annexation_oath_text_4",
                inputToken: "annexation_oath_4",
                outputToken: "annexation_oath_5",
                text: "And I shall defend your rights and the rights of your legitimate heirs.");
            starter.AddDialogLine(
                id: "annexation_oath_text_5",
                inputToken: "annexation_oath_5",
                outputToken: NativeGameTokens.LordPreTalk,
                text: "You are now my {HERO_KINGDOM_TITLE}.",
                consequence: KingdomAnnexationAction.ApplyByPlayerConversation
            );
        }

        private static void SetTextVariables()
        {
            var heroKingdom = Hero.OneToOneConversationHero.Clan?.Kingdom;
            var playerKingdom = Hero.MainHero.Clan?.Kingdom;
            if (heroKingdom == null || playerKingdom == null) return;
            var lowFiefsDescription = heroKingdom.Fiefs.IsEmpty() ? "holds no fiefdoms in its control" : "holds control over a negligible amount of fiefdoms";
            MBTextManager.SetTextVariable("PLAYER_KINGDOM_TITLE", GetHeroFactionRulerText(Hero.MainHero.Clan?.Kingdom));
            MBTextManager.SetTextVariable("HERO_KINGDOM_TITLE", GetHeroFactionRulerText(Hero.OneToOneConversationHero.Clan?.Kingdom));
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

        private static TextObject GetHeroFactionRulerText(IFaction? kingdom)
        {
            var playerGenderSuffix = Hero.MainHero.IsFemale ? "_f" : "";
            return GameTexts.FindText("str_faction_ruler",
                kingdom?.Culture?.StringId?.Add(playerGenderSuffix, false)
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
                explanation = new TextObject("{HERO_KINGDOM} is stronger than {PLAYER_KINGDOM}.");
            else if (!enoughStrengthAdvantage)
                explanation = new TextObject(
                    $"{heroKingdom.Name} has {strengthRatio}% of {playerKingdom.Name} strength. Needs to be less than {Settings.AnnexedKingdomMaxStrengthRatio}%."
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
                    $"You are controlling {playerControlledHeroCultureFiefsPercentage}% of {cultureName} fiefs ({requiredFiefsPercentage}% required)."
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
                    new TextObject($"{heroKingdom.Name} is controlling {fiefsCount} fiefs (maximum {maxFiefsCount}).");
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