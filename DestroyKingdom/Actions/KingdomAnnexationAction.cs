using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace DestroyKingdom.Actions;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
internal static class KingdomAnnexationAction
{
    public static void ApplyByPlayerConversation()
    {
        var annexedKingdom = Hero.OneToOneConversationHero?.Clan?.Kingdom;
        var annexingKingdom = Hero.MainHero?.Clan?.Kingdom;
        if (annexingKingdom == null || annexedKingdom == null) return;
        Apply(annexedKingdom, annexingKingdom);
    }

    public static void Apply(Kingdom annexedKingdom, Kingdom annexingKingdom, bool showNotification = true)
    {
        var annexingLeader = annexingKingdom.Leader;
        List<Clan> annexedClans = new(annexedKingdom.Clans);
        foreach (var clan in annexedClans)
        {
            if (clan.IsClanTypeMercenary)
            {
                ChangeKingdomAction.ApplyByLeaveKingdomAsMercenary(clan, showNotification);
            }
            else if (clan.IsMinorFaction)
            {
                ChangeKingdomAction.ApplyByLeaveKingdom(clan, showNotification);
            }
            else
            {
                ChangeKingdomAction.ApplyByJoinToKingdom(clan, annexingKingdom, showNotification);
                if (showNotification && clan.Equals(annexedKingdom.RulingClan))
                {
                    SceneNotificationData scene = new JoinKingdomSceneNotificationItem(clan, annexingKingdom);
                    MBInformationManager.ShowSceneNotification(scene);
                }
            }
        }

        if (annexedKingdom.GetStanceWith(annexingKingdom).IsAtWar)
        {
            MakePeaceAction.Apply(annexedKingdom, annexingKingdom);
        }

        if (annexingLeader?.Clan?.Kingdom != annexedKingdom)
        {
            DestroyKingdomAction.Apply(annexedKingdom);
            annexedKingdom.RulingClan = annexingKingdom.RulingClan;
        }
    }
}