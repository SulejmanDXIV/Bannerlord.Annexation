using DestroyKingdom.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace DestroyKingdom.CampaignBehaviors
{
    internal class FixEmptyKingdomLeaderCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, FixEmptyKingdomLeader);
        }

        private void FixEmptyKingdomLeader(CampaignGameStarter starter)
        {
            foreach (var kingdom in Kingdom.All)
            {
                var leader = kingdom.Leader;
                if (leader == null)
                {
                    var newLeaderKingdom = KingdomExtensions
                        .AllActiveKingdomsFactions()
                        .MaxBy((potentialLeaderKingdom) => TownExtensions
                            .KingdomControlledCultureFiefs(potentialLeaderKingdom, kingdom.Culture)
                            .Count
                        );
                    kingdom.RulingClan = newLeaderKingdom.RulingClan;
                }
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}