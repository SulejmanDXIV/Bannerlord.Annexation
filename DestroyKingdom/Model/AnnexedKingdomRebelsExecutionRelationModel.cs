using DestroyKingdom.Data;
using DestroyKingdom.Shared;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace DestroyKingdom.Model;

public class AnnexedKingdomRebelsExecutionRelationModel : DefaultExecutionRelationModel
{
    public override int GetRelationChangeForExecutingHero(Hero victim, Hero hero, out bool showQuickNotification)
    {
        if (victim.Clan != null &&
            hero.Clan?.Kingdom != null &&
            true == AnnexationRebelClansStorage.Instance?.IsRebelClanAgainstAnnexingKingdom(
                victim.Clan,
                hero.Clan.Kingdom
            ))
        {
            var denominator = Settings.RebelExecutionRelationPenaltyDenominatorAnnexing;
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / denominator;
        }

        if (victim.Clan != null &&
            hero.Clan != null &&
            true == AnnexationRebelClansStorage.Instance?.IsAnnexationRebelClan(victim.Clan) &&
            !AnnexationRebelClansStorage.Instance.IsAnnexationRebelClan(hero.Clan) &&
            hero.Clan != victim.Clan
           )
        {
            var denominator = Settings.RebelExecutionRelationPenaltyDenominatorOthers;
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / denominator;
        }

        return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification);
    }
}