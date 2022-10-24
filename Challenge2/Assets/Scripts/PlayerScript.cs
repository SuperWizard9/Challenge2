using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;

    public Text score;
    public Text livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    
    private int scoreValue = 0;
    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        livesText.text = "Lives: " + livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipTwo;
        musicSource.Play();

        SetCountText();
    }

     void SetCountText(){
           // scoreValue += 1;
            score.text = "Score: "+ scoreValue.ToString();
            //Destroy(collision.collider.gameObject);
            if(scoreValue == 4){
                transform.position = new Vector2(20.0f, 5.05f);
                livesValue = 3;
            }
            if(scoreValue >= 8){
                winTextObject.SetActive(true);
                musicSource.clip = musicClipTwo;
                musicSource.Stop();
                musicSource.clip = musicClipOne;
                musicSource.Play();
                musicSource.loop = false;
        }
            livesText.text = "Lives: " + livesValue.ToString();
            if(livesValue == 0){
                loseTextObject.SetActive(true);
                speed = 0;
            }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
           scoreValue += 1;
           SetCountText();
           Destroy(collision.collider.gameObject);
            
        }


        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
            
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}