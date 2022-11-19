using System.Diagnostics.CodeAnalysis;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;

namespace DestroyKingdom.Extensions;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
public static class CampaignGameStarterExtensions
{
    public static ConversationSentence AddPlayerLine(
        this CampaignGameStarter starter,
        string id,
        string inputToken,
        string outputToken,
        string text,
        ConversationSentence.OnConditionDelegate? condition = null,
        ConversationSentence.OnConsequenceDelegate? consequence = null,
        int priority = 100,
        ConversationSentence.OnClickableConditionDelegate? clickableCondition = null,
        ConversationSentence.OnPersuasionOptionDelegate? persuasion = null)
    {
        return starter.AddPlayerLine(
            id, inputToken, outputToken, text, condition, consequence, priority, clickableCondition, persuasion
        );
    }

    public static ConversationSentence AddDialogLine(
        this CampaignGameStarter starter,
        string id,
        string inputToken,
        string outputToken,
        string text,
        ConversationSentence.OnConditionDelegate? condition = null,
        ConversationSentence.OnConsequenceDelegate? consequence = null,
        int priority = 100,
        ConversationSentence.OnClickableConditionDelegate? clickableCondition = null)
    {
        return starter.AddDialogLine(
            id, inputToken, outputToken, text, condition, consequence, priority, clickableCondition
        );
    }
}