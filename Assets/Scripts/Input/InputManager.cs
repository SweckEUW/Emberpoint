// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""61df2300-a18c-4a7b-b72d-0caf57f4d0cd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e53f14fc-9a4f-4992-b2a2-670586317609"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Act"",
                    ""type"": ""Button"",
                    ""id"": ""7f62108a-06bd-4eba-a590-f86ddd06e6ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""63b6f97d-99ef-4ef4-b3cb-f3ef86806f07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""PassThrough"",
                    ""id"": ""743c7f85-8ce1-4897-8ed8-78968e7e2dfe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d6061c5a-7026-45c9-8275-99d31c412c04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equip1"",
                    ""type"": ""Button"",
                    ""id"": ""a9f84db3-9ad9-465c-b239-f4e8c356c151"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CreateZone"",
                    ""type"": ""Button"",
                    ""id"": ""2177a3c9-40ba-480a-b10b-8a421a4487b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenStandardInventory"",
                    ""type"": ""Button"",
                    ""id"": ""d1f751fa-ca28-4bb1-95b2-063d45f4f34f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenCraftingInventory"",
                    ""type"": ""Button"",
                    ""id"": ""8132db5f-a22b-43bd-886e-bda713a8f377"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""wasdMovement"",
                    ""id"": ""4fa4090c-0ad5-455a-a5d8-43268b009d0d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8d787a63-fc34-4fe1-9191-20133ab8293c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""100aa094-63bf-45ad-8048-3cccff9868cf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""40a78f0d-80dc-4a05-a395-97d8692695d5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46281eb4-cecf-4d85-b9be-e05c306bb116"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""103f7754-6db1-4a9d-929d-a3fd9616c182"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""993df7c3-c75a-4c2f-b63d-63b784ad0e7b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7f7dc0c-07d7-46ae-a79b-2dc85962bf65"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d12103c1-2482-4c3a-9cf8-d37aa28a7997"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98251459-2c0f-4c73-b734-76024b094016"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00919164-12d5-4fc9-b8dc-b007fffbd6d8"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52735a9f-4990-4d4f-a325-05d1fedd2ccf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CreateZone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f5a3b75-791f-4776-8bec-0661fc1b80d9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenStandardInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78432bb1-a896-4a2a-8134-8f380e6ef64c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCraftingInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Test"",
            ""id"": ""ac5af3aa-42d1-47cf-bdb7-d0b1565c68b5"",
            ""actions"": [
                {
                    ""name"": ""DepleteHealth"",
                    ""type"": ""Button"",
                    ""id"": ""6e1667c2-b814-425f-af1f-70ddaf46d437"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DepleteHunger"",
                    ""type"": ""Button"",
                    ""id"": ""261fcead-7bd7-4982-82ab-d1ce224c9bd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DepleteShield"",
                    ""type"": ""Button"",
                    ""id"": ""85be52da-8d4d-4860-8167-66a487294569"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseHealth"",
                    ""type"": ""Button"",
                    ""id"": ""c285dfee-8f69-41fe-b673-c1668c5467df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1ec2eb31-e920-4b35-ae6d-35f9b1c5f2ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hit"",
                    ""type"": ""Button"",
                    ""id"": ""5bec761b-9ee5-4e19-8746-d4a459babe21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b187a752-d31f-4779-8006-77dbfaddf120"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepleteHealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6393f1e-a816-4cc4-9ca8-17c4e2d90b08"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepleteHunger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70e8ca4f-cc72-429d-8282-35a12df0c62e"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepleteShield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef964113-b93c-438e-9197-ec6690feb9a3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""462cebc4-29ca-4dfe-ab76-ca9355e15a13"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbe4e39c-b3bc-4717-a34f-f4f382111085"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseHealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""StandardInventory"",
            ""id"": ""37ebf53e-ce4c-4f5a-a267-fc9afa884eac"",
            ""actions"": [
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""9c6a79ec-1c5e-49c7-a694-6f65dd4f5602"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""54533ec0-2bde-4f79-beee-0324a1ae9920"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""11206b6a-8ec4-44de-b51e-e9f0f3ab8509"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d978a55a-f24d-4cdb-856c-98ed8bb996c4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CraftingInventory"",
            ""id"": ""fade329f-9d7a-41b2-97fa-85cc1baee0cc"",
            ""actions"": [
                {
                    ""name"": ""SelectItem"",
                    ""type"": ""Button"",
                    ""id"": ""2b0c03af-a30e-4cc1-8d9d-91b643d36298"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UnselectItem"",
                    ""type"": ""Button"",
                    ""id"": ""d34791e4-53b7-4806-9ec2-ec5fbf3fa7f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da2099b9-de5f-4730-a805-10403c7ee2e2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e421fe2e-9f2d-418e-b2d1-7c84c9370155"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UnselectItem"",
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
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Act = m_Player.FindAction("Act", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Turn = m_Player.FindAction("Turn", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Equip1 = m_Player.FindAction("Equip1", throwIfNotFound: true);
        m_Player_CreateZone = m_Player.FindAction("CreateZone", throwIfNotFound: true);
        m_Player_OpenStandardInventory = m_Player.FindAction("OpenStandardInventory", throwIfNotFound: true);
        m_Player_OpenCraftingInventory = m_Player.FindAction("OpenCraftingInventory", throwIfNotFound: true);
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_DepleteHealth = m_Test.FindAction("DepleteHealth", throwIfNotFound: true);
        m_Test_DepleteHunger = m_Test.FindAction("DepleteHunger", throwIfNotFound: true);
        m_Test_DepleteShield = m_Test.FindAction("DepleteShield", throwIfNotFound: true);
        m_Test_IncreaseHealth = m_Test.FindAction("IncreaseHealth", throwIfNotFound: true);
        m_Test_MousePosition = m_Test.FindAction("MousePosition", throwIfNotFound: true);
        m_Test_Hit = m_Test.FindAction("Hit", throwIfNotFound: true);
        // StandardInventory
        m_StandardInventory = asset.FindActionMap("StandardInventory", throwIfNotFound: true);
        m_StandardInventory_UseItem = m_StandardInventory.FindAction("UseItem", throwIfNotFound: true);
        m_StandardInventory_DropItem = m_StandardInventory.FindAction("DropItem", throwIfNotFound: true);
        // CraftingInventory
        m_CraftingInventory = asset.FindActionMap("CraftingInventory", throwIfNotFound: true);
        m_CraftingInventory_SelectItem = m_CraftingInventory.FindAction("SelectItem", throwIfNotFound: true);
        m_CraftingInventory_UnselectItem = m_CraftingInventory.FindAction("UnselectItem", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Act;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Turn;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Equip1;
    private readonly InputAction m_Player_CreateZone;
    private readonly InputAction m_Player_OpenStandardInventory;
    private readonly InputAction m_Player_OpenCraftingInventory;
    public struct PlayerActions
    {
        private @InputManager m_Wrapper;
        public PlayerActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Act => m_Wrapper.m_Player_Act;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Turn => m_Wrapper.m_Player_Turn;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Equip1 => m_Wrapper.m_Player_Equip1;
        public InputAction @CreateZone => m_Wrapper.m_Player_CreateZone;
        public InputAction @OpenStandardInventory => m_Wrapper.m_Player_OpenStandardInventory;
        public InputAction @OpenCraftingInventory => m_Wrapper.m_Player_OpenCraftingInventory;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAct;
                @Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAct;
                @Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAct;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Turn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Equip1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquip1;
                @Equip1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquip1;
                @Equip1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquip1;
                @CreateZone.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreateZone;
                @CreateZone.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreateZone;
                @CreateZone.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreateZone;
                @OpenStandardInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStandardInventory;
                @OpenStandardInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStandardInventory;
                @OpenStandardInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStandardInventory;
                @OpenCraftingInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCraftingInventory;
                @OpenCraftingInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCraftingInventory;
                @OpenCraftingInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCraftingInventory;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Act.started += instance.OnAct;
                @Act.performed += instance.OnAct;
                @Act.canceled += instance.OnAct;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Equip1.started += instance.OnEquip1;
                @Equip1.performed += instance.OnEquip1;
                @Equip1.canceled += instance.OnEquip1;
                @CreateZone.started += instance.OnCreateZone;
                @CreateZone.performed += instance.OnCreateZone;
                @CreateZone.canceled += instance.OnCreateZone;
                @OpenStandardInventory.started += instance.OnOpenStandardInventory;
                @OpenStandardInventory.performed += instance.OnOpenStandardInventory;
                @OpenStandardInventory.canceled += instance.OnOpenStandardInventory;
                @OpenCraftingInventory.started += instance.OnOpenCraftingInventory;
                @OpenCraftingInventory.performed += instance.OnOpenCraftingInventory;
                @OpenCraftingInventory.canceled += instance.OnOpenCraftingInventory;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_DepleteHealth;
    private readonly InputAction m_Test_DepleteHunger;
    private readonly InputAction m_Test_DepleteShield;
    private readonly InputAction m_Test_IncreaseHealth;
    private readonly InputAction m_Test_MousePosition;
    private readonly InputAction m_Test_Hit;
    public struct TestActions
    {
        private @InputManager m_Wrapper;
        public TestActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @DepleteHealth => m_Wrapper.m_Test_DepleteHealth;
        public InputAction @DepleteHunger => m_Wrapper.m_Test_DepleteHunger;
        public InputAction @DepleteShield => m_Wrapper.m_Test_DepleteShield;
        public InputAction @IncreaseHealth => m_Wrapper.m_Test_IncreaseHealth;
        public InputAction @MousePosition => m_Wrapper.m_Test_MousePosition;
        public InputAction @Hit => m_Wrapper.m_Test_Hit;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @DepleteHealth.started -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHealth;
                @DepleteHealth.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHealth;
                @DepleteHealth.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHealth;
                @DepleteHunger.started -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHunger;
                @DepleteHunger.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHunger;
                @DepleteHunger.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteHunger;
                @DepleteShield.started -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteShield;
                @DepleteShield.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteShield;
                @DepleteShield.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnDepleteShield;
                @IncreaseHealth.started -= m_Wrapper.m_TestActionsCallbackInterface.OnIncreaseHealth;
                @IncreaseHealth.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnIncreaseHealth;
                @IncreaseHealth.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnIncreaseHealth;
                @MousePosition.started -= m_Wrapper.m_TestActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnMousePosition;
                @Hit.started -= m_Wrapper.m_TestActionsCallbackInterface.OnHit;
                @Hit.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnHit;
                @Hit.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnHit;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DepleteHealth.started += instance.OnDepleteHealth;
                @DepleteHealth.performed += instance.OnDepleteHealth;
                @DepleteHealth.canceled += instance.OnDepleteHealth;
                @DepleteHunger.started += instance.OnDepleteHunger;
                @DepleteHunger.performed += instance.OnDepleteHunger;
                @DepleteHunger.canceled += instance.OnDepleteHunger;
                @DepleteShield.started += instance.OnDepleteShield;
                @DepleteShield.performed += instance.OnDepleteShield;
                @DepleteShield.canceled += instance.OnDepleteShield;
                @IncreaseHealth.started += instance.OnIncreaseHealth;
                @IncreaseHealth.performed += instance.OnIncreaseHealth;
                @IncreaseHealth.canceled += instance.OnIncreaseHealth;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Hit.started += instance.OnHit;
                @Hit.performed += instance.OnHit;
                @Hit.canceled += instance.OnHit;
            }
        }
    }
    public TestActions @Test => new TestActions(this);

    // StandardInventory
    private readonly InputActionMap m_StandardInventory;
    private IStandardInventoryActions m_StandardInventoryActionsCallbackInterface;
    private readonly InputAction m_StandardInventory_UseItem;
    private readonly InputAction m_StandardInventory_DropItem;
    public struct StandardInventoryActions
    {
        private @InputManager m_Wrapper;
        public StandardInventoryActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseItem => m_Wrapper.m_StandardInventory_UseItem;
        public InputAction @DropItem => m_Wrapper.m_StandardInventory_DropItem;
        public InputActionMap Get() { return m_Wrapper.m_StandardInventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandardInventoryActions set) { return set.Get(); }
        public void SetCallbacks(IStandardInventoryActions instance)
        {
            if (m_Wrapper.m_StandardInventoryActionsCallbackInterface != null)
            {
                @UseItem.started -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnUseItem;
                @DropItem.started -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_StandardInventoryActionsCallbackInterface.OnDropItem;
            }
            m_Wrapper.m_StandardInventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
            }
        }
    }
    public StandardInventoryActions @StandardInventory => new StandardInventoryActions(this);

    // CraftingInventory
    private readonly InputActionMap m_CraftingInventory;
    private ICraftingInventoryActions m_CraftingInventoryActionsCallbackInterface;
    private readonly InputAction m_CraftingInventory_SelectItem;
    private readonly InputAction m_CraftingInventory_UnselectItem;
    public struct CraftingInventoryActions
    {
        private @InputManager m_Wrapper;
        public CraftingInventoryActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectItem => m_Wrapper.m_CraftingInventory_SelectItem;
        public InputAction @UnselectItem => m_Wrapper.m_CraftingInventory_UnselectItem;
        public InputActionMap Get() { return m_Wrapper.m_CraftingInventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CraftingInventoryActions set) { return set.Get(); }
        public void SetCallbacks(ICraftingInventoryActions instance)
        {
            if (m_Wrapper.m_CraftingInventoryActionsCallbackInterface != null)
            {
                @SelectItem.started -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnSelectItem;
                @SelectItem.performed -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnSelectItem;
                @SelectItem.canceled -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnSelectItem;
                @UnselectItem.started -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnUnselectItem;
                @UnselectItem.performed -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnUnselectItem;
                @UnselectItem.canceled -= m_Wrapper.m_CraftingInventoryActionsCallbackInterface.OnUnselectItem;
            }
            m_Wrapper.m_CraftingInventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectItem.started += instance.OnSelectItem;
                @SelectItem.performed += instance.OnSelectItem;
                @SelectItem.canceled += instance.OnSelectItem;
                @UnselectItem.started += instance.OnUnselectItem;
                @UnselectItem.performed += instance.OnUnselectItem;
                @UnselectItem.canceled += instance.OnUnselectItem;
            }
        }
    }
    public CraftingInventoryActions @CraftingInventory => new CraftingInventoryActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAct(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnEquip1(InputAction.CallbackContext context);
        void OnCreateZone(InputAction.CallbackContext context);
        void OnOpenStandardInventory(InputAction.CallbackContext context);
        void OnOpenCraftingInventory(InputAction.CallbackContext context);
    }
    public interface ITestActions
    {
        void OnDepleteHealth(InputAction.CallbackContext context);
        void OnDepleteHunger(InputAction.CallbackContext context);
        void OnDepleteShield(InputAction.CallbackContext context);
        void OnIncreaseHealth(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnHit(InputAction.CallbackContext context);
    }
    public interface IStandardInventoryActions
    {
        void OnUseItem(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
    }
    public interface ICraftingInventoryActions
    {
        void OnSelectItem(InputAction.CallbackContext context);
        void OnUnselectItem(InputAction.CallbackContext context);
    }
}
