using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text Lives;
    public GameObject winText;
    public GameObject loseText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    
    private int scoreValue = 0;
    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        Lives.text = "Lives: " + livesValue.ToString();
        winText.SetActive(false);
        loseText.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

    }

    void Update()
    {
        if(scoreValue == 8)
        {
            speed = 0;
            winText.SetActive(true);
            musicSource.loop = false;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        if(livesValue == 0)
        {
            loseText.SetActive(true);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
   {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
                // moving locations
                if(scoreValue == 4)
            {
                transform.position = new Vector2(56f, 1f);
                livesValue = 3;
                Lives.text = livesValue.ToString();
            }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            Lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }


   }
   private void OnCollisionStay2D(Collision2D collision)
   {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
            }
        }
   }
}
