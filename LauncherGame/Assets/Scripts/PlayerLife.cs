using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource levelCompleteSoundEffect;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool isDead;

    public Settings playerSettings;

    // Start is called before the first frame update
    private void Awake()
    {
        playerSettings = GameObject.Find("Player").GetComponent<Settings>();      
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("traps") && PlayerPrefs.GetInt("godmode") == 0)
        {
            Die();
        }
        else if(collision.gameObject.CompareTag("Finish"))
        {
            anim.SetTrigger("levelComplete");
            rb.bodyType = RigidbodyType2D.Static;
            levelCompleteSoundEffect.Play();
            Invoke("CompleteLevel", 2f);
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        boxCollider.enabled = false;
        deathSoundEffect.Play();
        // isDead = true;
        anim.SetTrigger("death");
    }

    private void CompleteLevel()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartLevel()
    {
        // transform.position = new Vector3(99f, -8f, 0f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = true;
        // isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
