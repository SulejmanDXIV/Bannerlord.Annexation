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
            RebelClansStorage.Instance().IsRebelClanAgainstKingdom(victim.Clan, hero.Clan.Kingdom))
        {
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / 5;
        }

        if (victim.Clan != null && RebelClansStorage.Instance().IsRebelClan(victim.Clan) && hero.Clan != victim.Clan)
        {
            return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification) / 3;
        }

        return base.GetRelationChangeForExecutingHero(victim, hero, out showQuickNotification);
    }
}