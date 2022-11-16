using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hero : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition, pos;

    [SerializeField] private bool grounded;

    private bool isDeath;

    public bool IsDeath 
    { 
        get 
        {
            return isDeath;
        }    
    }

    [SerializeField] ParticleSystem deathParticle;

    // Start is called before the first frame update
    void Start()
    {
        isDeath = false;
        grounded = true;
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        JumpAndAtack();
    }

    private void JumpAndAtack()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.y > startTouchPosition.y && grounded)
            {
                Jump();
            }

            if (endTouchPosition.x > startTouchPosition.x && grounded)
            {
               Attack();
            }
        }
    }

    void Attack()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2.5f, ForceMode2D.Impulse);
        GetComponent<Animator>().SetBool("atack", true);
        Invoke("NoAtack",0.35f);
        if (Time.timeScale != 0)
            AudioManager.S_PlaySound2D(SFXID.PLAYER_ATTACK);
    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7.5f, ForceMode2D.Impulse);
        GetComponent<Animator>().SetBool("jump", true);
        if (Time.timeScale != 0)
            AudioManager.S_PlaySound2D(SFXID.PLAYER_JUMP);
        grounded = false;
    }

    private void NoAtack()
    {
        GetComponent<Animator>().SetBool("atack", false);
        Invoke("Repos",2);
    }

    private void Repos()
    {
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
           Ground();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Hurt();
        }

        if (collision.gameObject.CompareTag("Potion"))
        {
            Lifes.Instance.Potion();
        }
    }

    void Ground()
    {
        if (!grounded)
            AudioManager.S_PlaySound2D(SFXID.PLAYER_GROUNDED);
            
        grounded = true;
        GetComponent<Animator>().SetBool("jump", false);
    }

    void Hurt()
    {
        GetComponent<Animator>().SetBool("Hurt", true);
        Lifes.Instance.Hurt();
        if(Lifes.Instance.LifeCounter == 0) {
            Death();
        } else {
            AudioManager.S_PlaySound2D(SFXID.PLAYER_RECEIVE_DAMAGE);
            Debug.Log("Receive damage");
        }
    }

    void ReRun()
    {
        GetComponent<Animator>().SetBool("Hurt", false);
    }

    void Death()
    {
        AudioManager.S_PlaySound2D(SFXID.PLAYER_DEATH);
        deathParticle.Play();
        Debug.Log("Muriooo");
        Invoke("SetDeathState", 1f);
    }

    private void SetDeathState() {
        isDeath = true;
    }

    private void EnableSwordWhenAttacking()
    {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }

    private void DisableSwordAfterAttack()
    {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }


}
