//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Input/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""oneButton"",
            ""id"": ""e5135300-0ddd-4e8a-9b0d-a5c8f9316845"",
            ""actions"": [
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""b250600c-c8a3-4b7e-8728-4607c81fedd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""282ad65c-25ac-4f15-83dc-f455937763a9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // oneButton
        m_oneButton = asset.FindActionMap("oneButton", throwIfNotFound: true);
        m_oneButton_Action = m_oneButton.FindAction("Action", throwIfNotFound: true);
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

    // oneButton
    private readonly InputActionMap m_oneButton;
    private IOneButtonActions m_OneButtonActionsCallbackInterface;
    private readonly InputAction m_oneButton_Action;
    public struct OneButtonActions
    {
        private @Controls m_Wrapper;
        public OneButtonActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Action => m_Wrapper.m_oneButton_Action;
        public InputActionMap Get() { return m_Wrapper.m_oneButton; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OneButtonActions set) { return set.Get(); }
        public void SetCallbacks(IOneButtonActions instance)
        {
            if (m_Wrapper.m_OneButtonActionsCallbackInterface != null)
            {
                @Action.started -= m_Wrapper.m_OneButtonActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_OneButtonActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_OneButtonActionsCallbackInterface.OnAction;
            }
            m_Wrapper.m_OneButtonActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
            }
        }
    }
    public OneButtonActions @oneButton => new OneButtonActions(this);
    public interface IOneButtonActions
    {
        void OnAction(InputAction.CallbackContext context);
    }
}