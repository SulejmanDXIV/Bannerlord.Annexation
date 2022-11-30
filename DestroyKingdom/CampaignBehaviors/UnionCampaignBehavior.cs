using System.Collections.Generic;
using System.Linq;
using DestroyKingdom.Extensions;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;

namespace DestroyKingdom.CampaignBehaviors
{
    internal class UnionCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.WarDeclared.AddNonSerializedListener(this, UnionReactionToWar);
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, CreateMockUnions);
        }

        private static void CreateMockUnions(CampaignGameStarter _)
        {
            var playerKingdom = KingdomExtensions.AllActiveKingdomsFactions()
                .First((kingdom) => kingdom.Clans.Contains(Hero.MainHero.Clan));
            var union = new List<Kingdom>
            {
                playerKingdom,
                KingdomExtensions.AllActiveKingdomsFactions()[0],
                KingdomExtensions.AllActiveKingdomsFactions()[1],
            };
            UnionsManager.CreateUnion(union);
            var unionDescription = union.Select((kingdom => kingdom.Name)).Join();
            InformationManager.DisplayMessage(new InformationMessage(unionDescription));
        }

        private static void UnionReactionToWar(IFaction attackingFaction, IFaction attackedFaction)
        {
            if (attackingFaction is Kingdom kingdom)
            {
                var unionMembers = UnionsManager.GetUnionMembers(kingdom);
                if (unionMembers.Contains(attackedFaction))
                {
                    InformationManager.DisplayMessage(
                        new InformationMessage("Declaring a war against member of your union")
                    );
                    foreach (var unionMember in unionMembers.Where(unionMember =>
                                 !unionMember.IsAtWarWith(attackingFaction) && unionMember != attackingFaction
                             ))
                    {
                        DeclareWarAction.Apply(unionMember, attackingFaction);
                        // Beside being attacked by whole union you will be kicked out of union with huge relationship drop and some dialogues referencing to this        
                    }
                }
                else if (unionMembers.Count > 0)
                {
                    InformationManager.DisplayMessage(
                        new InformationMessage("Declaring a war without agreeing this with your union")
                    );
                    // You will be kicked out of union with relationship drop and some dialogues referencing to this                    
                }
            }

            if (attackedFaction is Kingdom otherKingdom)
            {
                var unionMembers = UnionsManager.GetUnionMembers(otherKingdom);
                foreach (var unionMember in unionMembers.Where(unionMember =>
                             !unionMember.IsAtWarWith(attackingFaction) && unionMember != attackingFaction
                         ))
                {
                    DeclareWarAction.Apply(unionMember, attackingFaction);
                }
            }
        }


        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}