using DestroyKingdom.Data;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace DestroyKingdom.Model;

public class AnnexedKingdomRebelsExecutionRelationModel : DefaultExecutionRelationModel
{
    public override int GetRelationChangeForExecutingHero(Hero victim, Hero hero, out bool showQuickNotification)
    {
        if (victim.Clan != null &&
            hero.Clan?.Kingdom != null &&
            AnnexationRebelClansStorage.Instance?.IsRebelClanAgainstAnnexingKingdom(victim.Clan, hero.Clan.Kingdom) == true)
        {
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / 5;
        }

        if (victim.Clan != null && AnnexationRebelClansStorage.Instance?.IsAnnexationRebelClan(victim.Clan) == true && hero.Clan != victim.Clan)
        {
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / 3;
        }

        return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification);
    }
}