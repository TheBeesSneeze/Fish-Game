//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Controller Stuff/Controls.inputactions
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
            ""name"": ""FishControls"",
            ""id"": ""c756280e-f599-423f-9746-14603da2a0cb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6e23ede9-9f40-46b7-b729-4a9c313298d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Toggle Light"",
                    ""type"": ""Button"",
                    ""id"": ""6bd67fdc-43fd-4c34-bee3-09560897e39b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""93a520f9-0059-4750-8119-3bd4e4ca1a9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""45b0cccd-6158-48be-8167-28539ae61ee7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Increase Light"",
                    ""type"": ""Button"",
                    ""id"": ""0a5cf21b-07a5-417b-aeab-33bad092da95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Decrease Light"",
                    ""type"": ""Button"",
                    ""id"": ""6c467be1-d805-4fbe-adfc-47e59c864750"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""25680229-bc24-40c5-8240-a8daa64029c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ce0582fc-7fbb-4e02-aa3a-0076ba0bd592"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""58dd7a7e-7dd0-46dd-93d4-566ed8feda81"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d07033ed-a44f-420b-9b8a-1b6daf1e57c2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5ddc7314-6947-41b6-8dae-02a61a310475"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f058037c-fe7e-4ee7-b86a-23743f6524cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5233c80d-3778-499b-8c6d-00a75f1bfbd4"",
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
                    ""id"": ""9df0e515-db6b-49f2-baa9-68e370e02b3e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22ad23e6-0c2f-487a-902d-2c001950305c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b682ab9-c441-45ca-bfe1-df2d86ab1b37"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fa5276f-79bd-4248-8b26-778efc9e3cf3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5e86912-e52b-485b-ab3b-58b767af9d1b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d630477-ddc7-4382-9e84-f988f8387341"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a70379dc-f946-491f-b005-872822e19a9f"",
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
                    ""id"": ""25028910-ac2c-4d09-a453-567d1c2e8f83"",
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
                    ""id"": ""e84d26b4-b74f-4832-b09e-64ec7878244c"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf4c96b7-baad-4b54-96b3-375670308647"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Increase Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8175eeee-bcdf-4b67-8804-91507cb0ca71"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Increase Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72723c50-0e32-4656-8b85-761e4298d782"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Increase Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""373c9126-0717-4de9-8d39-029a35773ec4"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decrease Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e2bbc1a-03b7-4de7-ac50-bf662eca6e01"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decrease Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f89edfd8-faa9-45a7-9edb-9d398e0c3ac2"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decrease Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ede16bff-435d-49b1-9267-9a40748aa6b5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d77b7316-9607-4b33-8934-471cb4f76731"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OctopusControls"",
            ""id"": ""61107d35-a1d9-4ba9-8d22-c39c8aa7036d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f70eef4c-4b5a-4a8c-a2ff-a220e56d6c19"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Strike"",
                    ""type"": ""Button"",
                    ""id"": ""15e946ac-65fb-4699-94c5-14eeb025f927"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0b294ae7-eb56-49ca-a19d-c53daa27690e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c90b287b-3b5b-4b77-9b4d-25c04a7a0499"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c50e7ce-6762-438b-a2c4-444208c0342f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3ace11a4-311d-4392-80bf-9965813ccd99"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""859b14f9-fd78-4e80-a276-a7b7814e49b9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""38f2fd6a-9603-4313-8d73-f5a8144ed49e"",
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
                    ""id"": ""e651e269-7394-46bd-a32d-a7822780d153"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51bd55d0-bf83-4c49-9949-747d39d19107"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""214a6418-0ed9-4ecb-a478-e2516ac1b0c9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e384d241-f8ff-432d-abad-30addc58c07c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FishControls
        m_FishControls = asset.FindActionMap("FishControls", throwIfNotFound: true);
        m_FishControls_Move = m_FishControls.FindAction("Move", throwIfNotFound: true);
        m_FishControls_ToggleLight = m_FishControls.FindAction("Toggle Light", throwIfNotFound: true);
        m_FishControls_Dash = m_FishControls.FindAction("Dash", throwIfNotFound: true);
        m_FishControls_Pause = m_FishControls.FindAction("Pause", throwIfNotFound: true);
        m_FishControls_IncreaseLight = m_FishControls.FindAction("Increase Light", throwIfNotFound: true);
        m_FishControls_DecreaseLight = m_FishControls.FindAction("Decrease Light", throwIfNotFound: true);
        m_FishControls_Select = m_FishControls.FindAction("Select", throwIfNotFound: true);
        // OctopusControls
        m_OctopusControls = asset.FindActionMap("OctopusControls", throwIfNotFound: true);
        m_OctopusControls_Move = m_OctopusControls.FindAction("Move", throwIfNotFound: true);
        m_OctopusControls_Strike = m_OctopusControls.FindAction("Strike", throwIfNotFound: true);
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

    // FishControls
    private readonly InputActionMap m_FishControls;
    private IFishControlsActions m_FishControlsActionsCallbackInterface;
    private readonly InputAction m_FishControls_Move;
    private readonly InputAction m_FishControls_ToggleLight;
    private readonly InputAction m_FishControls_Dash;
    private readonly InputAction m_FishControls_Pause;
    private readonly InputAction m_FishControls_IncreaseLight;
    private readonly InputAction m_FishControls_DecreaseLight;
    private readonly InputAction m_FishControls_Select;
    public struct FishControlsActions
    {
        private @Controls m_Wrapper;
        public FishControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_FishControls_Move;
        public InputAction @ToggleLight => m_Wrapper.m_FishControls_ToggleLight;
        public InputAction @Dash => m_Wrapper.m_FishControls_Dash;
        public InputAction @Pause => m_Wrapper.m_FishControls_Pause;
        public InputAction @IncreaseLight => m_Wrapper.m_FishControls_IncreaseLight;
        public InputAction @DecreaseLight => m_Wrapper.m_FishControls_DecreaseLight;
        public InputAction @Select => m_Wrapper.m_FishControls_Select;
        public InputActionMap Get() { return m_Wrapper.m_FishControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FishControlsActions set) { return set.Get(); }
        public void SetCallbacks(IFishControlsActions instance)
        {
            if (m_Wrapper.m_FishControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnMove;
                @ToggleLight.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnToggleLight;
                @ToggleLight.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnToggleLight;
                @ToggleLight.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnToggleLight;
                @Dash.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDash;
                @Pause.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnPause;
                @IncreaseLight.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnIncreaseLight;
                @IncreaseLight.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnIncreaseLight;
                @IncreaseLight.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnIncreaseLight;
                @DecreaseLight.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDecreaseLight;
                @DecreaseLight.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDecreaseLight;
                @DecreaseLight.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnDecreaseLight;
                @Select.started -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_FishControlsActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_FishControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ToggleLight.started += instance.OnToggleLight;
                @ToggleLight.performed += instance.OnToggleLight;
                @ToggleLight.canceled += instance.OnToggleLight;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @IncreaseLight.started += instance.OnIncreaseLight;
                @IncreaseLight.performed += instance.OnIncreaseLight;
                @IncreaseLight.canceled += instance.OnIncreaseLight;
                @DecreaseLight.started += instance.OnDecreaseLight;
                @DecreaseLight.performed += instance.OnDecreaseLight;
                @DecreaseLight.canceled += instance.OnDecreaseLight;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public FishControlsActions @FishControls => new FishControlsActions(this);

    // OctopusControls
    private readonly InputActionMap m_OctopusControls;
    private IOctopusControlsActions m_OctopusControlsActionsCallbackInterface;
    private readonly InputAction m_OctopusControls_Move;
    private readonly InputAction m_OctopusControls_Strike;
    public struct OctopusControlsActions
    {
        private @Controls m_Wrapper;
        public OctopusControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_OctopusControls_Move;
        public InputAction @Strike => m_Wrapper.m_OctopusControls_Strike;
        public InputActionMap Get() { return m_Wrapper.m_OctopusControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OctopusControlsActions set) { return set.Get(); }
        public void SetCallbacks(IOctopusControlsActions instance)
        {
            if (m_Wrapper.m_OctopusControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnMove;
                @Strike.started -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnStrike;
                @Strike.performed -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnStrike;
                @Strike.canceled -= m_Wrapper.m_OctopusControlsActionsCallbackInterface.OnStrike;
            }
            m_Wrapper.m_OctopusControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Strike.started += instance.OnStrike;
                @Strike.performed += instance.OnStrike;
                @Strike.canceled += instance.OnStrike;
            }
        }
    }
    public OctopusControlsActions @OctopusControls => new OctopusControlsActions(this);
    public interface IFishControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnToggleLight(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnIncreaseLight(InputAction.CallbackContext context);
        void OnDecreaseLight(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IOctopusControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnStrike(InputAction.CallbackContext context);
    }
}
