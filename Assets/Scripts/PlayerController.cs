using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private bool isOnGround;
    public Transform groundcheck;
    private float moveHorizontal;
    private float moveVertical;
    public Text winText;
    public Text score;
    private int scoreValue = 0;
    private AudioController musicController;
    public bool isWinMusicNotPlaying = true;
    public bool isLoseMusicNotPlaying = true;
    public AudioClip coinSound;
    public float checkRadius;
    public LayerMask allGround;
    AudioSource audioSource;
    public GameObject coinPickUpPrefab;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 3f;
        jumpForce = 60f;
        isJumping = false;
        winText.text = "";
        score.text = scoreValue.ToString();
        musicController = FindObjectOfType<AudioController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coins")
        {
            scoreValue += 1;
            PlaySound(coinSound);
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            GameObject coinPickUp = Instantiate(coinPickUpPrefab, transform.position, Quaternion.identity);
            if (scoreValue >= 6)
            {
                winText.text = "You win! Game created by Michael Harris.";
                Time.timeScale = 0;
                if (isWinMusicNotPlaying)
                {
                    musicController.WinMusic();
                    isWinMusicNotPlaying = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
             rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
             rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }

}
