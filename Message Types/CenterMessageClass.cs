using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;


namespace AutomatedMessagesPlugin.CenterMessageType;
public class CenterMessageClass
{
    private BasePlugin? Parent { get; set; }

    public void Start(BasePlugin parent) {
        Parent = parent;
    }

    public void SendMessage(string message, float delay = 0, bool alert = false) {
        if (Parent is null) return;

        foreach (CCSPlayerController player in Utilities.GetPlayers()) {
            if (player.IsBot) continue;
            
            if (alert) Parent.AddTimer(delay, () => player.PrintToCenterAlert(message));
            else Parent.AddTimer(delay, () => player.PrintToCenterHtml(message));
        }
    }

}
