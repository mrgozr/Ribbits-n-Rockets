using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isDead;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("traps"))
        {
            Die();
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        // isDead = true;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        // transform.position = new Vector3(99f, -8f, 0f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        // isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
