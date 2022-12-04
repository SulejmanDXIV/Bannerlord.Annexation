using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DestroyKingdom.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace DestroyKingdom.Actions.KingdomAnnexation;

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
        List<Clan> vassalClans = new(annexedKingdom.VassalClans());
        var clansCollaborationChance = ClansCollaborationChanceCalculator.GetChance(
            annexedKingdom: annexedKingdom,
            annexingKingdom: annexingKingdom
        );
        var orderedVassalClans =
            vassalClans.OrderByDescending(clan => clan.GetRelationWithClan(annexingKingdom.RulingClan)).ToList();
        var clansCount = orderedVassalClans.Count;
        var collaboratingClansCount = Math.Max(1, clansCount * clansCollaborationChance / 100);

        for (var index = 0; index < clansCount; index++)
        {
            if (index < collaboratingClansCount)
            {
                ChangeKingdomAction.ApplyByJoinToKingdom(orderedVassalClans[index], annexingKingdom, false);
            }
            else
            {
                DestroyClanAction.Apply(orderedVassalClans[index]);
            }
        }

        foreach (var clan in annexedClans)
        {
            if (clan.IsClanTypeMercenary)
            {
                ChangeKingdomAction.ApplyByLeaveKingdomAsMercenary(clan, showNotification: false);
            }
            else if (clan.IsMinorFaction)
            {
                ChangeKingdomAction.ApplyByLeaveKingdom(clan, showNotification: false);
            }
            else
            {
                if (showNotification && clan.Equals(annexedKingdom.RulingClan))
                {
                    SceneNotificationData scene = new JoinKingdomSceneNotificationItem(clan, annexingKingdom);
                    MBInformationManager.ShowSceneNotification(scene);
                    ChangeKingdomAction.ApplyByJoinToKingdom(clan, annexingKingdom, false);
                }
            }
        }

        if (annexedKingdom.GetStanceWith(annexingKingdom).IsAtWar)
        {
            MakePeaceAction.Apply(annexedKingdom, annexingKingdom);
        }

        var oldRulerName = annexedKingdom.RulingClan.Leader.Name;
        if (annexingLeader?.Clan?.Kingdom != annexedKingdom)
        {
            DestroyKingdomAction.Apply(annexedKingdom);
            annexedKingdom.RulingClan = annexingKingdom.RulingClan;
        }

        if (showNotification)
        {
            MBInformationManager.AddQuickInformation(new TextObject(
                $"{annexingKingdom} annexed {annexedKingdom}. {oldRulerName} and {collaboratingClansCount} out of {vassalClans.Count} vassal clans decided to collaborate."));
        }
    }
}