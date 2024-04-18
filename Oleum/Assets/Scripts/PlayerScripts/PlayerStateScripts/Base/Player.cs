using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IPlayerMoveable
{
    #region Health Variables
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float HealthGain { get; private set; }
    [field: SerializeField] public float HealthDrain { get; private set; }
    [field: SerializeField] public float HealthDelay { get; private set; }
    #endregion

    #region Stamina Variables
    [field: SerializeField] public float MaxStamina { get; set; }
    [field: SerializeField] public float Stamina { get; set; }
    [field: SerializeField] public float StaminaGain { get; private set; }
    [field: SerializeField] public float StaminaDrain { get; private set; }
    [field: SerializeField] public float StaminaDelay { get; private set; }
    #endregion

    #region Oxygen Variables
    [field: SerializeField] public float MaxOxygen { get; private set; }
    [field: SerializeField] public float Oxygen { get; private set; }
    [field: SerializeField] public float OxygenGain { get; private set; }
    [field: SerializeField] public float OxygenDrain { get; private set; }

    #endregion

    #region Movement Variables
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float SprintMulti { get; private set; }
    public Rigidbody2D RB { get; set; }
    [SerializeField] private GameObject playerHolder;
    [field: SerializeField] public Animator PlayerMaskAnim { get; private set; }
    [field: SerializeField] public Animator PlayerBodyAnim { get; private set; }
    [field: SerializeField] public Animator PlayerFeetAnim { get; private set; }
    #endregion

    #region Inventory

    public Inventory Inventory;
    public UIManagerScript UIManager;
    public GameObject normalHolder;
    public GameObject heavyHolder;

    #endregion

    #region Counter Variables
    public float StaminaCounter { get; set; }
    public float HealthCounter { get; set; }
    #endregion

    #region State Management Variables
    public PlayerStateMachine StateMachine { get; set; }

    public PlayerIdleState IdleState { get; set; }
    public PlayerWalkingState WalkingState { get; set; }
    public PlayerSprintState SprintState { get; set; }
    public PlayerDeadState DeadState { get; set; }

    public PlayerHealthFullState HealthFullState { get; set; }
    public PlayerHealthHealingState HealthHealingState { get; set; }
    public PlayerHealthDrainState HealthDrainState { get; set; }
    public PlayerHealthNotFullState HealthNotFullState { get; set; }
    public PlayerHealthDeadState HealthDeadState { get; set; }

    public PlayerStaminaFullState StaminaFullState { get; set; }
    public PlayerStaminaGainState StaminaGainState { get; set; }
    public PlayerStaminaDrainState StaminaDrainState { get; set; }
    public PlayerStaminaNotFullState StaminaNotFullState { get; set; }
    public PlayerStaminaDeadState StaminaDeadState { get; set; }

    public PlayerOxygenFullState OxygenFullState { get; set; }
    public PlayerOxygenGainState OxygenGainState { get; set; }
    public PlayerOxygenDrainState OxygenDrainState { get; set; }
    public PlayerOxygenEmptyState OxygenEmptyState { get; set; }
    public PlayerOxygenDeadState OxygenDeadState { get; set; }

    public PlayerInteractActiveState InteractActiveState { get; set; }
    public PlayerInteractDeactiveState InteractDeactiveState { get; set; }


    #endregion

    #region MonoBehavior Functions

    private void Awake()
    {

        Inventory = new Inventory(this);
        Inventory.stateMachine = new InventoryStateMachine();

        Inventory.ActiveState = new InventoryActiveState(Inventory, this, Inventory.stateMachine);
        Inventory.DeactiveState = new InventoryDeactiveState(Inventory, this, Inventory.stateMachine);
        Inventory.LockedState = new InventoryLockedState(Inventory, this, Inventory.stateMachine);

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        WalkingState = new PlayerWalkingState(this, StateMachine);
        SprintState = new PlayerSprintState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);

        HealthFullState = new PlayerHealthFullState(this, StateMachine);
        HealthHealingState = new PlayerHealthHealingState(this, StateMachine);
        HealthDrainState = new PlayerHealthDrainState(this, StateMachine);
        HealthNotFullState = new PlayerHealthNotFullState(this, StateMachine);
        HealthDeadState = new PlayerHealthDeadState(this, StateMachine);

        StaminaFullState = new PlayerStaminaFullState(this, StateMachine);
        StaminaGainState = new PlayerStaminaGainState(this, StateMachine);
        StaminaDrainState = new PlayerStaminaDrainState(this, StateMachine);
        StaminaNotFullState = new PlayerStaminaNotFullState(this, StateMachine);
        StaminaDeadState = new PlayerStaminaDeadState(this, StateMachine);

        OxygenFullState = new PlayerOxygenFullState(this, StateMachine);
        OxygenGainState = new PlayerOxygenGainState(this, StateMachine);
        OxygenDrainState = new PlayerOxygenDrainState(this, StateMachine);
        OxygenEmptyState = new PlayerOxygenEmptyState(this, StateMachine);
        OxygenDeadState = new PlayerOxygenDeadState(this, StateMachine);

        InteractActiveState = new PlayerInteractActiveState(this, StateMachine);
        InteractDeactiveState = new PlayerInteractDeactiveState(this, StateMachine);
        
        
    }

    private void Start()
    {

        Health = MaxHealth;
        Stamina = MaxStamina;
        Oxygen = MaxOxygen;

        RB = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState, HealthFullState, StaminaFullState, OxygenDrainState, InteractDeactiveState);

        Inventory.stateMachine.Initialize(Inventory.ActiveState);

    }

    private void Update()
    {

        StateMachine.CurrPlayerState.FrameUpdate();
        StateMachine.CurrPlayerHealthState.FrameUpdate();
        StateMachine.CurrPlayerStaminaState.FrameUpdate();
        StateMachine.CurrPlayerOxygenState.FrameUpdate();
        StateMachine.CurrPlayerInteractState.FrameUpdate();

        Inventory.stateMachine.CurrInventoryState.FrameUpdate();

    }

    private void FixedUpdate()
    {

        StateMachine.CurrPlayerState.PhysicsUpdate();
        StateMachine.CurrPlayerHealthState.PhysicsUpdate();
        StateMachine.CurrPlayerStaminaState.PhysicsUpdate();
        StateMachine.CurrPlayerOxygenState.PhysicsUpdate();
        StateMachine.CurrPlayerInteractState.PhysicsUpdate();

        Inventory.stateMachine.CurrInventoryState.PhysicsUpdate();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.isTrigger && collision.gameObject.TryGetComponent(out IInteractable interactObj))
        {

            StateMachine.ChangeState(InteractActiveState);
            InteractActiveState.AddInteractItem(interactObj.GetInteractText(this), interactObj.GetHashCode(), collision.gameObject, interactObj);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.isTrigger && collision.gameObject.TryGetComponent(out IInteractable interactObj))
        {

            InteractActiveState.RemoveInteractItem(interactObj.GetHashCode());

        }

    }

    #endregion

    #region Damageable Functions
    public void Damage(float damage)
    {

        HealthCounter = 0;
        StateMachine.ChangeState(HealthNotFullState);

        if (Health < damage)
        {

            Health = 0;
            Die();

        }
        else
        {

            Health -= damage;
            if (Health == 0)
            {

                Die();

            }

        }

    }

    public void Heal(float health)
    {

        if (Health + health > MaxHealth)
        {

            Health = MaxHealth;
            StateMachine.ChangeState(HealthFullState);

        }
        else
        {

            Health += health;
            if (Health == MaxHealth)
            {

                StateMachine.ChangeState(HealthFullState);

            }

        }

    }

    public void Die()
    {

        StateMachine.ChangeState(DeadState);
        StateMachine.ChangeState(HealthDeadState);
        StateMachine.ChangeState(StaminaDeadState);
        StateMachine.ChangeState(OxygenDeadState);
        StateMachine.ChangeState(InteractDeactiveState);

    }
    #endregion

    #region Movement Functions
    public void MovePlayer(Vector2 velocity)
    {

        RB.velocity = velocity;

    }

    public void FollowMouse()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - playerHolder.transform.position.x, mousePosition.y - playerHolder.transform.position.y);
        playerHolder.transform.up = direction;

    }

    public void SetAnimation(int anim)
    {

        PlayerMaskAnim.SetInteger("CurrAnim", anim);
        PlayerFeetAnim.SetInteger("CurrAnim", anim);

    }
    #endregion

    #region Drain/Gain Functions

    public void Rest(float stamina)
    {

        if (Stamina + stamina > MaxStamina)
        {

            Stamina = MaxStamina;
            StateMachine.ChangeState(StaminaFullState);

        }
        else
        {

            Stamina += stamina;
            if (Stamina == MaxStamina)
            {

                StateMachine.ChangeState(StaminaFullState);

            }

        }

    }

    public void Breath(float oxygen)
    {

        if (Oxygen + oxygen > MaxOxygen)
        {

            Oxygen = MaxOxygen;

        }
        else
        {

            Oxygen += oxygen;

        }

        StateMachine.ChangeState(OxygenDrainState);

    }

    public void DrainOxygen()
    {

        Oxygen -= OxygenDrain * Time.deltaTime;

    }

    public void GainOxygen()
    {

        Oxygen += OxygenGain * Time.deltaTime;

    }

    public void DrainStamina()
    {

        Stamina -= StaminaDrain * Time.deltaTime;

    }

    public void GainStamina()
    {

        Stamina += StaminaGain * Time.deltaTime;

    }

    public void GainHealth()
    {

        Health += HealthGain * Time.deltaTime;

    }

    public void DrainHealth()
    {

        Health -= HealthDrain * Time.deltaTime;

    }
    #endregion

    #region Have Functions
    public bool HaveStamina()
    {

        if (Stamina > 0)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
    public bool HaveHealth()
    {

        if (Health > 0)
        {

            return true;

        }
        else
        {

            return false;

        }
    }

    public bool HaveFullOxygen()
    {

        if (Oxygen == MaxOxygen)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
        #endregion

    #region Health/Stamina Couter Functions
        public bool TestStaminaCounter()
    {

        if (StaminaCounter >= StaminaDelay)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public void CountStaminaDelay()
    {

        StaminaCounter += Time.deltaTime;

    }

    public bool TestHealthCounter()
    {

        if (HealthCounter >= HealthDelay)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public void CountHealthDelay()
    {

        HealthCounter += Time.deltaTime;

    }
    #endregion

    #region Get Functions
    public float GetOxygen()
    {

        return Oxygen;

    }

    public float GetMaxOxygen()
    {

        return MaxOxygen;

    }
    public float GetStamina()
    {

        return Stamina;

    }

    public float GetHealth()
    {

        return Health;

    }

    public float GetMaxStamina()
    {

        return MaxStamina;

    }

    public float GetMaxHealth()
    {

        return MaxHealth;

    }
    #endregion

    #region Set Functions

    public void SetHealth(float value)
    {

        Health = value;

    }

    public void SetStamina(float value)
    {

        Stamina = value;

    }

    public void SetOxygen(float value)
    {

        Oxygen = value;

    }

    public void SetSpeedMultiplier(float multiplier)
    {

        MoveSpeed *= multiplier;

    }

    public void UnsetSpeedMultiplier(float divisor)
    {

        MoveSpeed /= divisor;

    }

    #endregion

    #region TestState Functions
    public bool IsIdle()
    {

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {

            return true;

        }
        else
        {

            return false;

        }
        

    }

    public bool IsWalking()
    {

        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !(Input.GetKey(KeyCode.LeftShift)))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool IsSprinting()
    {

        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift) && Stamina != 0)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
    #endregion

}
