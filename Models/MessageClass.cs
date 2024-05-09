namespace AutomatedMessagesPlugin;
public class MessageClass {
    public float Delay { get; init; } = 0;
    public MessageTypeEnum MessageType { get; init; }
    public required string Message { get; init; }
}
