using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;
using AutomatedMessagesPlugin.ChatMessageType;
using System.Text.Json;
using CounterStrikeSharp.API.Modules.Timers;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;
using AutomatedMessagesPlugin.CenterMessageType;


namespace AutomatedMessagesPlugin;
public class AutomatedMessagesPlugin : BasePlugin
{
    private readonly ChatMessageClass _chatMessageClass;
    private readonly CenterMessageClass _centerMessageClass;
    public AutomatedMessagesPlugin(ChatMessageClass chatMessageClass, CenterMessageClass centerMessageClass)
    {
        _chatMessageClass = chatMessageClass;
        _centerMessageClass = centerMessageClass;
    }


    public override string ModuleName => "Automated Messages";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Kwasiks";

    private Config Config { get; set; } = new();
    private List<Timer> MessageTimers { get; } = [];
    private List<MessageClass> WelcomeMessages { get; } = [];


    public override void Load(bool hotReload)
    {
        Logger.LogInformation("[Automated Messages] Loading...");

        Logger.LogInformation("[Automated Messages] Starting Chat Messages Module");
        _chatMessageClass.Start(this);

        Logger.LogInformation("[Automated Messages] Starting Center Messages Module");
        _centerMessageClass.Start(this);

        Logger.LogInformation("[Automated Messages] Loading Configuration");
        Config = LoadConfig();
        foreach (MessageGroupClass messageGroup in Config.MessageGroups)
        {
            if (messageGroup.Interval == 0) {
                WelcomeMessages.AddRange(messageGroup.Messages);
                continue;
            }

            MessageTimers.Add(AddTimer(messageGroup.Interval, () => ShowMessage(messageGroup), TimerFlags.REPEAT));
        }

        RegisterEventHandler<EventPlayerConnectFull>(EventPlayerConnectFull);
        base.Load(hotReload);
    }


    public override void Unload(bool hotReload)
    {
        Logger.LogInformation("[Automated Messages] Shutting down...");

        base.Unload(hotReload);
    }


    private HookResult EventPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player is null || !player.IsValid) return HookResult.Continue;

        foreach (MessageClass message in WelcomeMessages) {
            ShowMessage(message);
        }

        return HookResult.Continue;
    }

    private void ShowMessage(MessageGroupClass messageGroup) {
        MessageClass message = messageGroup.GetNextMessage();

        ShowMessage(message);
    }


    private void ShowMessage(MessageClass message) {
        switch (message.MessageType)
        {
            case MessageTypeEnum.CHAT:
                _chatMessageClass.SendMessage(TextParser.GetColoredText(Config.ChatMessagePrefix + message.Message), message.Delay);
                break;

            case MessageTypeEnum.CENTER:
                _centerMessageClass.SendMessage(message.Message, message.Delay, false);
                break;

            case MessageTypeEnum.CENTER_ALERT:
                _centerMessageClass.SendMessage(message.Message, message.Delay, true);
                break;

            default: 
                Logger.LogWarning($"[Automated Messages] Unknown message type: {message.MessageType}");
                break;
        }
    }


    private Config LoadConfig() {
        string am_config_folder = Path.Combine(Application.RootDirectory, "configs/plugins/AutomatedMessages");
        string am_config_path = Path.Combine(am_config_folder, "config.json");

        try {
            if (!File.Exists(am_config_path)) return CreateConfig(am_config_path);
        } catch (DirectoryNotFoundException) {
            Directory.CreateDirectory(am_config_folder);
            if (!File.Exists(am_config_path)) return CreateConfig(am_config_path);
        }

        Config config = JsonSerializer.Deserialize<Config>(
            File.ReadAllText(am_config_path), 
            new JsonSerializerOptions() { ReadCommentHandling = JsonCommentHandling.Skip }
        )!;

        return config;
    }

    private Config CreateConfig(string am_config_path) {
        Config config = new Config() {
            // Prefix in front of all messages
            ChatMessagePrefix = "",
            MessageGroups = [
                new MessageGroupClass() {
                    // Interval 0 is a welcome message
                    Interval = 0,
                    Messages = [
                        new MessageClass() {
                            Delay = 0,
                            // Message type: 0 - CHAT | 1 - CENTER | 2 - CENTER RED ALERT
                            MessageType = MessageTypeEnum.CHAT,
                            Message = "{red}Message that gets sent as soon as the player joins"
                        },
                        new MessageClass() {
                            // Delay after which the message will be sent
                            Delay = 10,
                            MessageType = MessageTypeEnum.CHAT,
                            Message = "Message that gets sent after 10 seconds"
                        }
                    ]
                },
                new MessageGroupClass() {
                    // Interval 5 will rotate through the provided messages every 5 seconds
                    Interval = 5,
                    Messages = [
                        new MessageClass() {
                            Delay = 0,
                            MessageType = MessageTypeEnum.CHAT,
                            Message = "{fadedred}1st message that gets sent every 5 seconds"
                        },
                        new MessageClass() {
                            MessageType = MessageTypeEnum.CHAT,
                            Message = "{lightpurple}2nd message that gets sent every 5 seconds"
                        }
                    ]
                },
                new MessageGroupClass() {
                    // Interval 5 will rotate through the provided messages every 5 seconds
                    Interval = 10,
                    Messages = [
                        new MessageClass() {
                            Delay = 0,
                            MessageType = MessageTypeEnum.CENTER,
                            Message = "1st center message"
                        },
                        new MessageClass() {
                            MessageType = MessageTypeEnum.CENTER_ALERT,
                            Message = "2nd center message with alert"
                        }
                    ]
                }
            ],
            
        };

        File.WriteAllText(am_config_path, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        Logger.LogWarning("[Automated Messages] No config file found, created a new one");
        Logger.LogWarning("[Automated Messages] New config file location: {PATH}", am_config_path);

        return config;
    }
}
