using System.Collections.Generic;
using System.Linq;
using DestroyKingdom.Data;
using DestroyKingdom.Extensions;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace DestroyKingdom;

public class Cheats
{
    private const string VassalizeAllRebelsInfo =
        "You need to provide kingdom name as a parameter (spaces are ignored): 'annexation.vassalize_all_rebels [kingdom]'.";

    private const string VassalizeClanInfo =
        "You need to provide kingdom name and clan name as a parameters (spaces are ignored): 'annexation.vassalize_clan [kingdom] [clan]'.";

    [CommandLineFunctionality.CommandLineArgumentFunction("vassalize_all_rebels", "annexation")]
    [UsedImplicitly]
    public static string VassalizeAllRebels(List<string> strings)
    {
        if (strings.IsEmpty())
        {
            return VassalizeAllRebelsInfo;
        }

        var kingdom = KingdomExtensions.AllActiveKingdomsFactions().Find(kingdom =>
            kingdom.Name.ToString().ToLower().Replace(" ", "") == strings[0]
        );
        if (kingdom == null)
        {
            return $"Couldn't find kingdom with {strings[0]} name. {VassalizeAllRebelsInfo}";
        }

        var kingdomlessClans =
            Clan.All.Where(clan =>
                    !clan.IsEliminated &&
                    AnnexationRebelClansStorage.Instance?.IsRebelClanAgainstAnnexingKingdom(clan, kingdom) == true
                )
                .ToList();
        foreach (var clan in kingdomlessClans)
        {
            if (clan.GetStanceWith(kingdom).IsAtWar)
            {
                MakePeaceAction.Apply(clan, kingdom);
            }
            ChangeKingdomAction.ApplyByJoinToKingdom(clan, kingdom);
        }

        return $"{kingdomlessClans.Count} clans without kingdom joined {kingdom.Name}.";
    }

    [CommandLineFunctionality.CommandLineArgumentFunction("vassalize_clan", "annexation")]
    [UsedImplicitly]
    public static string VassalizeClan(List<string> strings)
    {
        if (strings.Count < 2)
        {
            return VassalizeClanInfo;
        }

        var kingdom = KingdomExtensions.AllActiveKingdomsFactions().Find(kingdom =>
            kingdom.Name.ToString().ToLower().Replace(" ", "") == strings[0]
        );
        if (kingdom == null)
        {
            return $"Couldn't find kingdom with {strings[0]} name. {VassalizeClanInfo}";
        }

        var clan = Clan.All.ToList().Find(clan =>
            clan.Name.ToString().ToLower().Replace(" ", "") == strings[1]
        );
        if (clan == null)
        {
            return $"Couldn't find clan with {strings[1]} name. {VassalizeClanInfo}";
        }

        if (clan.GetStanceWith(kingdom).IsAtWar)
        {
            MakePeaceAction.Apply(clan, kingdom);
        }
        ChangeKingdomAction.ApplyByJoinToKingdom(clan, kingdom);
        return $"{clan.Name} clan joined {kingdom.Name}.";
    }
}