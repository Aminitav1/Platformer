using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    public ParticleSystem death;

    [SerializeField] private AudioSource deathSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Spikes")) {
            deathSFX.Play();
            Die();
            CreateDeathDust();
            Invoke("RestartLevel",0.5f);
        }
    }

    private void Die() {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CreateDeathDust() {
        death.Play();
    }

}


    
