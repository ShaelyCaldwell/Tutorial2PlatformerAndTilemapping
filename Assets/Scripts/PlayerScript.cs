using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;

    public Text score;
    public Text livesText;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject enemyObject;
    public GameObject enemyObject2;

    private int scoreValue = 0;
    private int lives;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private bool facingRight = true;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;


    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        score.text = scoreValue.ToString();
        scoreValue = 0;
        lives = 3;

        musicSource.clip = musicClipOne;
        musicSource.Play();
        

        SetCountText();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        enemyObject.SetActive(true);
        enemyObject2.SetActive(true);
    }

    void SetCountText()
    {
        score.text = "Coins: " + scoreValue.ToString();
        if(scoreValue >= 8)
       {

           winTextObject.SetActive(true);
           enemyObject.SetActive(false);
           enemyObject2.SetActive(false);

           musicSource.Stop();
           musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
       }

        livesText.text = "Lives: " + lives.ToString();
        if(lives <= 0 && scoreValue <8)
       {
           loseTextObject.SetActive(true);
            Destroy(this);
       }
  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

    }

    void Update()
  
    {
        
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

         if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
          anim.SetInteger("State", 2);
        }

         if (Input.GetKeyUp(KeyCode.W))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
          
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
          
        }

         if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
         {
          Flip();
         }
        else if (facingRight == true && hozMovement < 0)
        {
           Flip();
         }

        
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            
            Destroy(collision.collider.gameObject);

            SetCountText();
        }

        if(collision.collider.tag == "Enemy")
        {
          collision.collider.gameObject.SetActive(false);
          lives = lives - 1;

          SetCountText();
        }
        
        if(scoreValue == 4 && collision.collider.tag == "Coin")
        {
            transform.position = new Vector2(78.5f, 0f);
            lives = 3;
        }


    }

      private void OnCollisionStay2D(Collision2D collision)
      {
        if (collision.collider.tag == "Ground" && isOnGround)
          {   
            if (Input.GetKey(KeyCode.W))
              {
              rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
              
             }

          }
          
    }

    

}
