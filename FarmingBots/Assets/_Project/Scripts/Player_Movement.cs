using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public float Speed;
    public float maxSpeed;
    private float initialSpeed;
    private Animator anim;

    private Rigidbody2D rb;

    public GameObject GameoverMenu;

    private float moveAmount;

    public float maxHealth;
    public float health;

    //For Jump
    public LayerMask groundLayerMask;
    float boxCastDistance = 0.03f;
    Collider2D col;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    [SerializeField]
    float jumpPower = 300f;
   


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        initialSpeed = Speed;
    }

    private void Update()
    {
       float moveInput = Input.GetAxisRaw("Horizontal");

        moveAmount = moveInput * Speed;
                if (moveInput!=0)
                {
                    anim.SetBool("isRunning",true);
                }
                else
                {
                    anim.SetBool("isRunning", false);
                }
        Jump();
    }
    private void FixedUpdate()
    {
        //  rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime); 
        rb.velocity = new Vector2(moveAmount * Time.deltaTime, rb.velocity.y);
    }


    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;
        if (health <= 0)
        {
            GameoverMenu.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        if (health+healAmount >maxHealth)
        {
            health = maxHealth;
        }
        else
        {
        health += healAmount;
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(transform.up * jumpPower);
      
        }
    }
    bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, boxCastDistance, groundLayerMask);
    }



    public void SpeedUp(float speedAmount)
    {
        if (Speed + speedAmount > maxSpeed)
        {
            Speed = maxSpeed;
        }
        else
        {
            Speed += speedAmount;
        }
        StartCoroutine(nameof(ResetInitialSpeed));
    }

    IEnumerator ResetInitialSpeed()
    {
        yield return new WaitForSeconds(5);
        Speed = initialSpeed;
        print("Speed Back to " + Speed);
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }
}
