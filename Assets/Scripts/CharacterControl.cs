using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public Image filler;
    

    public float counter; // Countrs from 0 to 2
    public float maxCounter; // 2sec, defines how fast healthbar moves to new arvoon.
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Ground test, testing if touching ground
        if(Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
            
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Either a or d pressed
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);

        } 
        
        else
        {   
            // Not moving
            animator.SetBool("Walk", false);
        }
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
        }

        if(counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth/GameManager.manager.maxHealth, GameManager.manager.health/GameManager.manager.maxHealth, counter/maxCounter);

    } // update ends here

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("You hit the trap");
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AddHealth"))
        {
            Destroy(collision.gameObject);
            Heal(10);
        }

        if(collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(50);
        }
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }

    void AddMaxHealth(float amt)
    {
        GameManager.manager.maxHealth += amt;
    }

    void Heal(float amt)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 1;
        GameManager.manager.health += amt;
        if(GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        } 
    }
    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;
    }

}
