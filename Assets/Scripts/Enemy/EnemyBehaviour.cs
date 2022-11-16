using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] SFXID sfxidAttack;
    [SerializeField] float speed = 10;
    [SerializeField] float leftBound = -12;

    [SerializeField] ParticleSystem deathParticles;

    void Start()
    {
        Invoke("PlayAttackSFX", speed * 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * Random.Range(speed -1,  speed));

        if (transform.position.x < leftBound && transform.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Sword"))
        {
            Death();
        }
    }

    void Death()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        deathParticles.Play();
        AudioManager.S_PlaySound2D(SFXID.ENEMY_DEATH);
    }

    private void PlayAttackSFX()
    {
        AudioManager.S_PlaySound2D(sfxidAttack);
    }

}
