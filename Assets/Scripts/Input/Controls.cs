// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c0b99ff8-d2f3-45f8-9e57-9d8c3c6320cf"",
            ""actions"": [
                {
                    ""name"": ""Pointer"",
                    ""type"": ""Value"",
                    ""id"": ""98435dc3-6bc3-4624-bf8c-2994226beaa3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""74c52f76-19da-4041-aeae-3a9506ab3b46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""Value"",
                    ""id"": ""8f543729-8568-4ffe-8ba1-0d0893169c53"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b88b48d5-77b6-4072-80b7-5503d2ba5e76"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83dcf55b-e347-4251-a165-d670e567102f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f363346f-f6b3-4ee5-872b-9bdd545a96cb"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delta"",
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
        m_Player_Pointer = m_Player.FindAction("Pointer", throwIfNotFound: true);
        m_Player_Tap = m_Player.FindAction("Tap", throwIfNotFound: true);
        m_Player_Delta = m_Player.FindAction("Delta", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Pointer;
    private readonly InputAction m_Player_Tap;
    private readonly InputAction m_Player_Delta;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pointer => m_Wrapper.m_Player_Pointer;
        public InputAction @Tap => m_Wrapper.m_Player_Tap;
        public InputAction @Delta => m_Wrapper.m_Player_Delta;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Pointer.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointer;
                @Pointer.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointer;
                @Pointer.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointer;
                @Tap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
                @Delta.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDelta;
                @Delta.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDelta;
                @Delta.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDelta;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pointer.started += instance.OnPointer;
                @Pointer.performed += instance.OnPointer;
                @Pointer.canceled += instance.OnPointer;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @Delta.started += instance.OnDelta;
                @Delta.performed += instance.OnDelta;
                @Delta.canceled += instance.OnDelta;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnPointer(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
    }
}
