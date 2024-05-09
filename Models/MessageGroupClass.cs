namespace AutomatedMessagesPlugin;
public class MessageGroupClass {
    public required float Interval { get; init; }
    public List<MessageClass> Messages { get; init; } = [];

    private int CurrentMessageIndex = 0;

    public MessageClass GetNextMessage() {
        if (CurrentMessageIndex >= Messages.Count) CurrentMessageIndex = 0;

        return Messages[CurrentMessageIndex++];
    }
}
