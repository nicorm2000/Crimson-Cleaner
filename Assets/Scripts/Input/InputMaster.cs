//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/InputMaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMaster: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5db76ec4-680c-4bd6-a9d2-9b257493d4f4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""70799dc0-314b-4f28-8031-55897c0b484f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""5d8dd195-d62f-433a-abd7-120aa15b7a3a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""bd477d2f-3bc8-41db-b6b7-b87ecbb85f4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Clean"",
                    ""type"": ""Button"",
                    ""id"": ""0876eb9c-63bb-4223-af96-ba66ba907dc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""5ba1d509-8cf5-42f3-a372-7ddec8d3cbe6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""5a6fb3b5-35f2-42bc-8666-75611a9b5800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CleaningList"",
                    ""type"": ""Button"",
                    ""id"": ""c6f6e0dc-9a90-4b88-a07d-70b2d6e75f71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ac387ae-d80d-4e3d-92c6-991a76e6260f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""c008ad94-5ed5-4e3b-84eb-48aff0114f6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tutorial"",
                    ""type"": ""Button"",
                    ""id"": ""39be201b-d974-426d-b785-acb0ad45b002"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeRotationAxis"",
                    ""type"": ""Button"",
                    ""id"": ""a3ad6d32-55bf-4f56-a0bc-342454fae1ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotatePos"",
                    ""type"": ""Button"",
                    ""id"": ""212d4de4-849e-4101-8f1a-7885171c4560"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateNeg"",
                    ""type"": ""Button"",
                    ""id"": ""c1c0d7e3-9ba9-4251-9131-12a46a15e96c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToolWheel"",
                    ""type"": ""Button"",
                    ""id"": ""0e29cc62-c2ca-40ea-be0d-e5a5605c15ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DispatchBag"",
                    ""type"": ""Button"",
                    ""id"": ""bb7a5d49-df3a-44e0-9bd8-5f930a33b607"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StoreObject"",
                    ""type"": ""Button"",
                    ""id"": ""d8dde6fd-c851-497d-89ac-6d996491db35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""511b015c-63d7-482f-b012-bb3693b253ce"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""30bc317e-ca16-43b3-827a-f953d6060a7f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""fc516ec0-6599-4583-b3a4-0bb1867c273e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""723979b3-b1dc-4a34-889b-118b9ea7bb3a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""d199f423-4222-4fd4-a813-0c30189d70fa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cd1b05a7-6ef2-4395-8984-16b846df05a5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2987b70-51d9-49c4-a6ab-ea0350da7e2f"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffbf5c3f-0764-4727-b716-e7b7499b640b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed503483-2103-450d-9f08-e4c526183427"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50eca980-b548-4a89-96ac-abdd456d85ae"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cb3b146-b360-4a25-aab9-0187119f82de"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1ebd2b6-025d-4c59-833b-42020a0f8bb8"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49ba4e5c-05b9-4ba4-8006-1081e77353be"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed5623ad-2e97-4799-a91d-2f5a70c59aa9"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd742fcf-5a59-4882-b5a4-fc151f6426ac"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8df82dce-2103-4ef2-9416-79eb6a507395"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b218d9d2-1b44-47e7-95b9-5d35fa55b7d9"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f99e1b9c-1d41-4d63-ae55-f3c3e82231ac"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ed80e5a-ac82-45dd-a5d5-0034a9f82fe6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37ccdd21-794e-4899-b7fb-42cc783b722c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cc69dba-7ac4-4231-9662-9e8bcfaa615c"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c455bb73-a97c-400c-b55b-c9ac2d5e1700"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CleaningList"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""106b3306-160a-4176-b614-e41c848ffeb8"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65f2fe7a-67cc-4172-ad85-64e0b9165d9e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""902ee121-383d-4370-b137-b1ebe948d468"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tutorial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08164b85-5d0f-4aa4-8ef3-56251c70e577"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeRotationAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f59cf138-2e1e-4849-8502-2b7d46c4b0ed"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotatePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d3b1988-9b5c-441e-8e44-d79584e0d851"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateNeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a6ffae4-5c79-453e-a65d-40445ae16c97"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToolWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea866c35-a420-4cfa-a0b3-56eac4b4fc16"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DispatchBag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7a0da03-e629-444b-85a9-95af8e95e584"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StoreObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pause"",
            ""id"": ""f78e6d7d-8abd-4296-90c6-7a4091c0aa18"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4e7d14e0-d8b4-4384-aa1c-b75f1c899da3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0602ae2a-9b65-4bed-98f8-02b270dff49c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ef1fe82-f74f-4e83-b8d9-1de13f9e8bc5"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15b10c41-d48b-47a1-a2a9-43e30a26bf01"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43ec4891-6c00-4c41-b4f1-83fd11138be2"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Clean = m_Player.FindAction("Clean", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_PickUp = m_Player.FindAction("PickUp", throwIfNotFound: true);
        m_Player_CleaningList = m_Player.FindAction("CleaningList", throwIfNotFound: true);
        m_Player_MouseScroll = m_Player.FindAction("MouseScroll", throwIfNotFound: true);
        m_Player_Throw = m_Player.FindAction("Throw", throwIfNotFound: true);
        m_Player_Tutorial = m_Player.FindAction("Tutorial", throwIfNotFound: true);
        m_Player_ChangeRotationAxis = m_Player.FindAction("ChangeRotationAxis", throwIfNotFound: true);
        m_Player_RotatePos = m_Player.FindAction("RotatePos", throwIfNotFound: true);
        m_Player_RotateNeg = m_Player.FindAction("RotateNeg", throwIfNotFound: true);
        m_Player_ToolWheel = m_Player.FindAction("ToolWheel", throwIfNotFound: true);
        m_Player_DispatchBag = m_Player.FindAction("DispatchBag", throwIfNotFound: true);
        m_Player_StoreObject = m_Player.FindAction("StoreObject", throwIfNotFound: true);
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_Pause = m_Pause.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Clean;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_PickUp;
    private readonly InputAction m_Player_CleaningList;
    private readonly InputAction m_Player_MouseScroll;
    private readonly InputAction m_Player_Throw;
    private readonly InputAction m_Player_Tutorial;
    private readonly InputAction m_Player_ChangeRotationAxis;
    private readonly InputAction m_Player_RotatePos;
    private readonly InputAction m_Player_RotateNeg;
    private readonly InputAction m_Player_ToolWheel;
    private readonly InputAction m_Player_DispatchBag;
    private readonly InputAction m_Player_StoreObject;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Clean => m_Wrapper.m_Player_Clean;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @PickUp => m_Wrapper.m_Player_PickUp;
        public InputAction @CleaningList => m_Wrapper.m_Player_CleaningList;
        public InputAction @MouseScroll => m_Wrapper.m_Player_MouseScroll;
        public InputAction @Throw => m_Wrapper.m_Player_Throw;
        public InputAction @Tutorial => m_Wrapper.m_Player_Tutorial;
        public InputAction @ChangeRotationAxis => m_Wrapper.m_Player_ChangeRotationAxis;
        public InputAction @RotatePos => m_Wrapper.m_Player_RotatePos;
        public InputAction @RotateNeg => m_Wrapper.m_Player_RotateNeg;
        public InputAction @ToolWheel => m_Wrapper.m_Player_ToolWheel;
        public InputAction @DispatchBag => m_Wrapper.m_Player_DispatchBag;
        public InputAction @StoreObject => m_Wrapper.m_Player_StoreObject;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Clean.started += instance.OnClean;
            @Clean.performed += instance.OnClean;
            @Clean.canceled += instance.OnClean;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @PickUp.started += instance.OnPickUp;
            @PickUp.performed += instance.OnPickUp;
            @PickUp.canceled += instance.OnPickUp;
            @CleaningList.started += instance.OnCleaningList;
            @CleaningList.performed += instance.OnCleaningList;
            @CleaningList.canceled += instance.OnCleaningList;
            @MouseScroll.started += instance.OnMouseScroll;
            @MouseScroll.performed += instance.OnMouseScroll;
            @MouseScroll.canceled += instance.OnMouseScroll;
            @Throw.started += instance.OnThrow;
            @Throw.performed += instance.OnThrow;
            @Throw.canceled += instance.OnThrow;
            @Tutorial.started += instance.OnTutorial;
            @Tutorial.performed += instance.OnTutorial;
            @Tutorial.canceled += instance.OnTutorial;
            @ChangeRotationAxis.started += instance.OnChangeRotationAxis;
            @ChangeRotationAxis.performed += instance.OnChangeRotationAxis;
            @ChangeRotationAxis.canceled += instance.OnChangeRotationAxis;
            @RotatePos.started += instance.OnRotatePos;
            @RotatePos.performed += instance.OnRotatePos;
            @RotatePos.canceled += instance.OnRotatePos;
            @RotateNeg.started += instance.OnRotateNeg;
            @RotateNeg.performed += instance.OnRotateNeg;
            @RotateNeg.canceled += instance.OnRotateNeg;
            @ToolWheel.started += instance.OnToolWheel;
            @ToolWheel.performed += instance.OnToolWheel;
            @ToolWheel.canceled += instance.OnToolWheel;
            @DispatchBag.started += instance.OnDispatchBag;
            @DispatchBag.performed += instance.OnDispatchBag;
            @DispatchBag.canceled += instance.OnDispatchBag;
            @StoreObject.started += instance.OnStoreObject;
            @StoreObject.performed += instance.OnStoreObject;
            @StoreObject.canceled += instance.OnStoreObject;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Clean.started -= instance.OnClean;
            @Clean.performed -= instance.OnClean;
            @Clean.canceled -= instance.OnClean;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @PickUp.started -= instance.OnPickUp;
            @PickUp.performed -= instance.OnPickUp;
            @PickUp.canceled -= instance.OnPickUp;
            @CleaningList.started -= instance.OnCleaningList;
            @CleaningList.performed -= instance.OnCleaningList;
            @CleaningList.canceled -= instance.OnCleaningList;
            @MouseScroll.started -= instance.OnMouseScroll;
            @MouseScroll.performed -= instance.OnMouseScroll;
            @MouseScroll.canceled -= instance.OnMouseScroll;
            @Throw.started -= instance.OnThrow;
            @Throw.performed -= instance.OnThrow;
            @Throw.canceled -= instance.OnThrow;
            @Tutorial.started -= instance.OnTutorial;
            @Tutorial.performed -= instance.OnTutorial;
            @Tutorial.canceled -= instance.OnTutorial;
            @ChangeRotationAxis.started -= instance.OnChangeRotationAxis;
            @ChangeRotationAxis.performed -= instance.OnChangeRotationAxis;
            @ChangeRotationAxis.canceled -= instance.OnChangeRotationAxis;
            @RotatePos.started -= instance.OnRotatePos;
            @RotatePos.performed -= instance.OnRotatePos;
            @RotatePos.canceled -= instance.OnRotatePos;
            @RotateNeg.started -= instance.OnRotateNeg;
            @RotateNeg.performed -= instance.OnRotateNeg;
            @RotateNeg.canceled -= instance.OnRotateNeg;
            @ToolWheel.started -= instance.OnToolWheel;
            @ToolWheel.performed -= instance.OnToolWheel;
            @ToolWheel.canceled -= instance.OnToolWheel;
            @DispatchBag.started -= instance.OnDispatchBag;
            @DispatchBag.performed -= instance.OnDispatchBag;
            @DispatchBag.canceled -= instance.OnDispatchBag;
            @StoreObject.started -= instance.OnStoreObject;
            @StoreObject.performed -= instance.OnStoreObject;
            @StoreObject.canceled -= instance.OnStoreObject;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Pause
    private readonly InputActionMap m_Pause;
    private List<IPauseActions> m_PauseActionsCallbackInterfaces = new List<IPauseActions>();
    private readonly InputAction m_Pause_Pause;
    public struct PauseActions
    {
        private @InputMaster m_Wrapper;
        public PauseActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Pause_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void AddCallbacks(IPauseActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseActionsCallbackInterfaces.Add(instance);
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IPauseActions instance)
        {
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseActions @Pause => new PauseActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnClean(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnCleaningList(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnTutorial(InputAction.CallbackContext context);
        void OnChangeRotationAxis(InputAction.CallbackContext context);
        void OnRotatePos(InputAction.CallbackContext context);
        void OnRotateNeg(InputAction.CallbackContext context);
        void OnToolWheel(InputAction.CallbackContext context);
        void OnDispatchBag(InputAction.CallbackContext context);
        void OnStoreObject(InputAction.CallbackContext context);
    }
    public interface IPauseActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
