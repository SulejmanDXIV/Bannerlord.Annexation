using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.SaveSystem;

namespace DestroyKingdom.Data;

public class AnnexationRebelClansStorage
{
    public static AnnexationRebelClansStorage? Instance { get; private set; }

    [SaveableProperty(1)]
    [UsedImplicitly]
    public Dictionary<string, List<string>> AnnexationRebelClans { get; private set; }

    public AnnexationRebelClansStorage()
    {
        AnnexationRebelClans = new Dictionary<string, List<string>>();
        Instance = this;
    }

    internal void Sync()
    {
        Instance = this;
    }

    public void AddAnnexationRebelClan(Clan clan, Kingdom kingdom)
    {
        if (AnnexationRebelClans.ContainsKey(kingdom.StringId))
        {
            var currentRebels = AnnexationRebelClans[kingdom.StringId];
            currentRebels.Add(clan.StringId);
        }
        else
        {
            AnnexationRebelClans[kingdom.StringId] = new List<string> { clan.StringId };
        }
    }

    public bool IsRebelClanAgainstAnnexingKingdom(Clan clan, Kingdom kingdom)
    {
        return AnnexationRebelClans.ContainsKey(kingdom.StringId) &&
               AnnexationRebelClans[kingdom.StringId].Contains(clan.StringId);
    }

    public bool IsAnnexationRebelClan(Clan clan)
    {
        return AnnexationRebelClans.Values.Any(clans => clans.Contains(clan.StringId));
    }
}