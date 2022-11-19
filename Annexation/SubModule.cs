using Annexation.CampaignBehaviors;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Annexation
{
    [UsedImplicitly]
    public class SubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (game.GameType is Campaign)
            {
                ((CampaignGameStarter)gameStarterObject).AddBehavior(new AnnexationConversationCampaignBehavior());
            }
        }
    }
}