using UnityEngine;

public interface IInteractable
{
    Sprite InteractMessage { get; }
    void Interact(PlayerController playerController);
}

public interface IInmersible : IInteractable
{

}

public interface IPickable
{
    bool IsObjectPickedUp { get; }
    Sprite PickUpMessage { get; }
    Sprite DropMessage { get; }
    Sprite ThrowMessage { get; }
    Sprite RotateMessage { get; }
}

public interface IRetrievable : IInteractable
{
}

public interface IOpenable : IInteractable
{
    bool IsOpen { get; }
}

public interface ICleanable : IInteractable
{
    event System.Action Cleaned;
}

public interface IToggable : IInteractable
{
    
}

public interface IDisposableInterface
{
    event System.Action Disposed;
}
