using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.Data;

public class RebelClansStorage
{
    private static RebelClansStorage? _instance;

    public static RebelClansStorage Instance()
    {
        return _instance ??= new RebelClansStorage();
    }

    private readonly Dictionary<Kingdom, List<Clan>> _rebelClans = new();

    public void AddRebelClan(Clan clan, Kingdom kingdom)
    {
        if (_rebelClans.ContainsKey(kingdom))
        {
            var currentRebels = _rebelClans[kingdom];
            currentRebels.Add(clan);
        }
        else
        {
            _rebelClans[kingdom] = new List<Clan> { clan };
        }
    }

    public bool IsRebelClanAgainstKingdom(Clan clan, Kingdom kingdom)
    {
        return _rebelClans.ContainsKey(kingdom) && _rebelClans[kingdom].Contains(clan);
    }

    public bool IsRebelClan(Clan clan)
    {
        return _rebelClans.Values.Any(clans => clans.Contains(clan));
    }
}