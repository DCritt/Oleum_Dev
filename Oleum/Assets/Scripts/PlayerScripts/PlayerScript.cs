using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{

    //timers
    private float staminaTime = 0;
    private float healthTime = 0;
    private float hurtScreenTime = 0;

    [SerializeField] private float hurtScreenTimer;

    //alive bool
    [SerializeField] private bool alive = true;

    //movement
    Vector2 move;
    [SerializeField] private float moveSpeed;
    private float baseSpeed;
    private float sprintSpeed;
    [SerializeField] private float sprintMulti;

    //stamina
    [SerializeField] private float stamina;
    [SerializeField] private float staminaDrain;
    [SerializeField] private float staminaGain;
    [SerializeField] private float staminaDelay;

    //health
    [SerializeField] private float health;
    [SerializeField] private float healthGain;
    [SerializeField] private float healthDelay;

    //physics
    public Rigidbody2D body;
    public CircleCollider2D collide;
    [SerializeField] private GameObject hurtScreen;
    [SerializeField] private GameObject flashlight;

    //function used to return the current player stamina
    public float getStamina()
    {

        return stamina;

    }

    //function used to return the current player health
    public float getHealth()
    {

        return health;

    }

    //function used to damage the player with a float parameter called damage
    public void damagePlayer(float damage)
    {
        
        if (health < damage)
        {

            health = 0;
            alive = false;
            hurtScreen.SetActive(true);       

        }
        else
        {

            health -= damage;
            healthTime = 0;
            hurtScreen.SetActive(true);
            if (health == 0)
            {

                alive = false;

            }

        }

    }

    [ContextMenu("testDamage")]
    public void testDamage()
    {

        if (health < 10)
        {

            health = 0;
            alive = false;

        }
        else
        {

            health -= 10;
            healthTime = 0;
            if (health == 0)
            {

                alive = false;

            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        baseSpeed = moveSpeed;
        sprintSpeed = moveSpeed * sprintMulti;

    }

    // Update is called once per frame
    void Update()
    {

        //points the character towards the players mouse while the character is alive
        if (alive)
        {

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            transform.up = direction;

        }

        //if the hurtscreen is active, deactivate it after a timer
        if (hurtScreenTime < hurtScreenTimer && hurtScreen.activeInHierarchy)
        {

            hurtScreenTime += Time.deltaTime;

        }
        else
        {

            hurtScreen.SetActive(false);
            hurtScreenTime = 0;

        }

        //continually check player x and y inputs while the player is alive
        if (alive)
        {

            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        }
        else
        {

            move = new Vector2(0, 0).normalized;

        }

        //if the player is alive and doesnt have max health, check to see if the health delay is over
        if (!(health >= 100) && alive)
        {

            //if the health delay is over begin the health gain process
            if (healthTime >= healthDelay)
            {

                if (health + healthGain * Time.deltaTime > 100)
                {

                    health = 100;

                }
                else
                {

                    health += healthGain * Time.deltaTime;

                }

            }
            else
            {

                healthTime += Time.deltaTime;

            }

        }
        
        //if the player is honding the shift key and a directional input then begin draining stamina and set moveSpeed to sprintSpeed
        if (alive && Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1))
        {
            
            if (stamina > 0)
            {
                if (stamina - staminaDrain * Time.deltaTime < 0)
                {

                    stamina = 0;

                }
                else
                {

                    moveSpeed = sprintSpeed;
                    stamina -= staminaDrain * Time.deltaTime;
                    staminaTime = 0;

                }

            }
            else
            {

                moveSpeed = baseSpeed;

            }

        }
        //if the player isnt sprinting and is alive check and see if the stamina delay period is over
        else if (!(stamina >= 100) && alive)
        {

            //if the stamina delay is over begin the stamina gain process
            if (staminaTime >= staminaDelay)
            {

                if (stamina + staminaGain * Time.deltaTime > 100)
                {

                    stamina = 100;

                }
                else
                {

                    stamina += staminaGain * Time.deltaTime;

                }

            }
            else
            {

                staminaTime += Time.deltaTime;

            }

        }

        //if the player lets go of the shift key, return moveSpeed back to baseSpeed
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            moveSpeed = baseSpeed;

        }
        


    }

    void FixedUpdate()
    {

            body.velocity = move * moveSpeed;

    }
}
