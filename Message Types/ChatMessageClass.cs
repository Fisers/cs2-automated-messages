using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;


namespace AutomatedMessagesPlugin.ChatMessageType;
public class ChatMessageClass
{
    private BasePlugin? Parent { get; set; }

    public void Start(BasePlugin parent) {
        Parent = parent;
    }

    public void SendMessage(string message, float delay = 0) {
        if (Parent is null) return;

        foreach (CCSPlayerController player in Utilities.GetPlayers()) {
            if (player.IsBot) continue;
            
            Parent.AddTimer(delay, () => player.PrintToChat(message));
        }
    }

}
