using System;
using UnityEngine;
using UnityEngine.UI;

public interface IPickable
{
    bool isObjectPickedUp { get; }

    Sprite PickUpMessage { get; }
    Sprite DropMessage { get; }
    Sprite ThrowMessage { get; }
    Sprite RotateMessage { get; }
}

public interface IRetrievable
{
    Sprite PickUpMessage { get; }
}

public interface IOpenable
{
    bool _isOpen { get; }

    Sprite InteractMessage { get; }
}

public interface ICleanable
{
    event Action Cleaned;

    Sprite CleanMessage { get; }
}

public interface IToggable
{
    Sprite ToggleOnOffMessage { get; }
}

public interface IDisposable
{
    event Action Disposed;
}
