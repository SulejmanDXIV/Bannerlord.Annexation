using DestroyKingdom.Data;
using TaleWorlds.CampaignSystem;

namespace DestroyKingdom.CampaignBehaviors;

public class SyncRebelClansStorageCampaignBehavior : CampaignBehaviorBase
{
    private AnnexationRebelClansStorage _annexationRebelClansStorage;

    public SyncRebelClansStorageCampaignBehavior()
    {
        _annexationRebelClansStorage = new AnnexationRebelClansStorage();
    }

    public override void RegisterEvents()
    {
    }

    public override void SyncData(IDataStore dataStore)
    {
        dataStore.SyncData("_annexationRebelClansStorage", ref _annexationRebelClansStorage);

        if (dataStore.IsLoading)
        {
            _annexationRebelClansStorage ??= new AnnexationRebelClansStorage();
            _annexationRebelClansStorage.Sync();
        }
    }
}