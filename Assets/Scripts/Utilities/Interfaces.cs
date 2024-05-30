
public interface IPick
{
    bool isObjectPickedUp { get; }
    string PickUpMessage { get; }
    string DropMessage { get; }
    string ThrowMessage { get; }
    string RotateMessage { get; }
}

public interface IOpenable
{
    bool _isOpen { get; }
    string OpenCloseMessage { get; }
}

public interface ICleanable
{
    string CleanMessage { get; }
}

public interface IToggable
{
    string ToggleOnOffMessage { get; }
}
