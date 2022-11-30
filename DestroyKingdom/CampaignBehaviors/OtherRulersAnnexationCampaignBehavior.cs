using DestroyKingdom.Actions;
using DestroyKingdom.Conditions;
using DestroyKingdom.Extensions;
using DestroyKingdom.Shared;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace DestroyKingdom.CampaignBehaviors
{
    internal class OtherRulersAnnexationCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, ConsiderAnnexations);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private static void ConsiderAnnexations()
        {
            var playerKingdom = Hero.MainHero?.Clan?.Kingdom;
            var fieflessKingdom = KingdomExtensions
                .AllActiveKingdomsFactions()
                .GetRandomElementWithPredicate((kingdom) => kingdom != playerKingdom && kingdom.Fiefs.Count == 0);
            if (fieflessKingdom == null) return;
            var potentialAnnexingKingdom = KingdomExtensions
                .AllActiveKingdomsFactions()
                .GetRandomElementWithPredicate((kingdom) => CanAnnexKingdom(fieflessKingdom, kingdom));
            if (potentialAnnexingKingdom == null) return;
            KingdomAnnexationAction.Apply(fieflessKingdom, potentialAnnexingKingdom, false);
        }

        private static bool CanAnnexKingdom(Kingdom annexedKingdom, Kingdom annexingKingdom)
        {
            var hasFiefs = annexingKingdom.Fiefs.Count > Settings.AnnexedKingdomMaxFiefsAmount;
            var strengthRatio = KingdomExtensions.KingdomsStrengthRatio(annexedKingdom, annexingKingdom);
            var isStronger = strengthRatio < Settings.AnnexedKingdomMaxStrengthRatio;
            var isControllingEnoughLand = KingdomAnnexationCondition.ControllingEnoughCultureLands(
                annexingKingdom: annexingKingdom,
                annexedKingdom: annexedKingdom
            );
            var playerIsNotRuler = annexingKingdom.RulingClan != Hero.MainHero?.Clan;
            return hasFiefs && isStronger && playerIsNotRuler && isControllingEnoughLand;
        }
    }
}