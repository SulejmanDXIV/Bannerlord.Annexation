using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace Annexation.Actions;

internal static class KingdomAnnexationAction
{
    public static void Apply()
    {
        var newKingdom = Hero.MainHero.Clan.Kingdom;
        var annexedKingdom = Hero.OneToOneConversationHero.Clan.Kingdom;
        List<Clan> annexedClans = new(annexedKingdom.Clans);
        foreach (var clan in annexedClans)
        {
            if (clan.IsClanTypeMercenary)
            {
                ChangeKingdomAction.ApplyByLeaveKingdomAsMercenary(clan);
            }
            else if (clan.IsMinorFaction)
            {
                ChangeKingdomAction.ApplyByLeaveKingdom(clan);
            }
            else
            {
                ChangeKingdomAction.ApplyByJoinToKingdom(clan, newKingdom);
                if (clan.Equals(annexedKingdom.RulingClan))
                {
                    SceneNotificationData scene = new JoinKingdomSceneNotificationItem(clan, newKingdom);
                    MBInformationManager.ShowSceneNotification(scene);
                }
            }
        }

        MakePeaceAction.ApplyPardonPlayer(annexedKingdom);
        DestroyKingdomAction.Apply(annexedKingdom);
        annexedKingdom.RulingClan = null;
    }
}