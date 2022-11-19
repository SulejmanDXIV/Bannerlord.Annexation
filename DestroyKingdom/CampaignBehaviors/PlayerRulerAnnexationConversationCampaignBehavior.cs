using DestroyKingdom.Actions;
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
                id: "annexation_demand_no_lords",
                inputToken: NativeGameTokens.HeroMainOptions,
                outputToken: AnnexationRequestedToken,
                text: "I demand you to recognize my authority as {HERO_KINGDOM_TITLE} of {HERO_KINGDOM_CULTURE}.",
                condition: PlayerIsRulerAndHeroIsRulerWithNoLordsCondition
            );
            starter.AddPlayerLine(
                id: "annexation_demand_lords",
                inputToken: NativeGameTokens.HeroMainOptions,
                outputToken: AnnexationRequestedToken,
                text:
                "I demand you and your lords to recognize my authority as {HERO_KINGDOM_TITLE} of {HERO_KINGDOM_CULTURE}.",
                condition: PlayerIsRulerAndHeroIsRulerWithLordsCondition
            );
            starter.AddDialogLine(
                id: "annexation_ask_first_reason",
                inputToken: AnnexationRequestedToken,
                outputToken: FirstReasonToken,
                text: "And why should I do that?"
            );
            starter.AddPlayerLine(
                id: "annexation_strength",
                inputToken: FirstReasonToken,
                outputToken: AfterFirstReasonToken,
                text: "{PLAYER_KINGDOM} is far stronger than {HERO_KINGDOM}.",
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
                text: "That's true, but I would need more reasons for that kind of decision."
            );
            starter.AddPlayerLine(
                id: "annexation_no_fiefs",
                inputToken: SecondReasonToken,
                outputToken: AfterSecondReasonToken,
                text: "{HERO_KINGDOM} does not control any fiefs.",
                clickableCondition: HeroKingdomNoFiefsClickableCondition
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
                text: "But why should YOU be the one to rule {HERO_KINGDOM_CULTURE} people?"
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control",
                inputToken: ThirdReasonToken,
                outputToken: AfterThirdReasonToken,
                text: "{PLAYER_KINGDOM} is controlling big part of {HERO_KINGDOM_CULTURE} lands.",
                condition: PlayerAnyTraitsCondition,
                clickableCondition: PlayerControllingCultureTownsClickableCondition
            );
            starter.AddPlayerLine(
                id: "annexation_fiefs_control_no_traits",
                inputToken: ThirdReasonToken,
                outputToken: "annexation_oath",
                text: "{PLAYER_KINGDOM} is controlling big part of {HERO_KINGDOM_CULTURE} lands.",
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
                text: "Go on."
            );
            starter.AddPlayerLine(
                id: "annexation_honor",
                inputToken: FourthReasonToken,
                outputToken: "annexation_oath",
                text: "You can trust my word - I will be taking care of the {HERO_KINGDOM_CULTURE}.",
                condition: PlayerTraitCondition.Honorable
            );
            starter.AddPlayerLine(
                id: "annexation_cruel",
                inputToken: FourthReasonToken,
                outputToken: "annexation_oath",
                text: "If you don't recognize my authority I will kill you and all your supporters.",
                condition: PlayerTraitCondition.Cruel
            );
            starter.AddPlayerLine(
                id: "annexation_generous",
                inputToken: FourthReasonToken,
                outputToken: "annexation_oath",
                text: "I will generously reward everyone who will be faithful to me.",
                condition: PlayerTraitCondition.Generous
            );
            starter.AddPlayerLine(
                id: "annexation_fearless",
                inputToken: FourthReasonToken,
                outputToken: "annexation_oath",
                text: "Many times I have shown my value as a fearless leader on the battlefields.",
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
                inputToken: "annexation_oath",
                outputToken: "annexation_oath_2",
                text:
                "This is hard decision, but after considering all circumstances that's the only thing that I can do."
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
            MBTextManager.SetTextVariable("HERO_KINGDOM_TITLE", GetHeroFactionRulerText());
            MBTextManager.SetTextVariable("PLAYER_KINGDOM", playerKingdom.Name);
            MBTextManager.SetTextVariable("HERO_KINGDOM", heroKingdom.Name);
            MBTextManager.SetTextVariable("HERO_KINGDOM_CULTURE", heroKingdom.Culture.Name);
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
            var enoughStrengthAdvantage = strengthRatio < KingdomAnnexationCondition.RequiredStrengthRatio;
            if (strengthRatio > 100)
                explanation = new TextObject("{HERO_KINGDOM} is stronger than {PLAYER_KINGDOM}.");
            else if (!enoughStrengthAdvantage)
                explanation = new TextObject(
                    $"{heroKingdom.Name} has {strengthRatio}% of {playerKingdom.Name} strength. Needs to be less than 25%."
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

        private static bool HeroKingdomNoFiefsClickableCondition(out TextObject? explanation)
        {
            var heroKingdom = Hero.OneToOneConversationHero?.Clan?.Kingdom;
            explanation = null;
            if (heroKingdom == null) return false;
            var fiefsCount = heroKingdom.Fiefs.Count;
            explanation = fiefsCount switch
            {
                1 => new TextObject($"{heroKingdom.Name} is controlling 1 fief."),
                > 0 => new TextObject($"{heroKingdom.Name} is controlling {fiefsCount} fiefs."),
                _ => null
            };
            return fiefsCount == 0;
        }

        private static bool PlayerIsRulerAndHeroIsRulerWithNoLordsCondition()
        {
            var areRulers = Hero.OneToOneConversationHero.IsRulerOfKingdom()
                            && Hero.MainHero.IsRulerOfKingdom()
                            && Hero.OneToOneConversationHero.Clan?.Kingdom?.VassalClansCount() == 0;
            if (areRulers) SetTextVariables();
            return areRulers;
        }

        private static bool PlayerIsRulerAndHeroIsRulerWithLordsCondition()
        {
            SetTextVariables();
            var areRulers = Hero.OneToOneConversationHero.IsRulerOfKingdom()
                            && Hero.MainHero.IsRulerOfKingdom()
                            && !PlayerIsRulerAndHeroIsRulerWithNoLordsCondition();
            if (areRulers) SetTextVariables();
            return areRulers;
        }
    }
}