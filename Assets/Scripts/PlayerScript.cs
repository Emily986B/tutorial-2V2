using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private int scoreValue = 0;
    private int flag;
    private bool facingRight = true;
   

    public int Lives;
    public float speed; 
    public Text score;
    public GameObject winTextObject; 
    public GameObject LoseTextObject;   
    public TextMeshProUGUI LivesText;
    public AudioClip gameMusic;
    public AudioClip winMusic;
    public AudioSource musicSource;
    
    Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        Lives = 3;
        flag = 0;
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();

         winTextObject.SetActive(false);
         LoseTextObject.SetActive(false);

        musicSource.clip = gameMusic;
        musicSource.Play();
       
       anim = GetComponent<Animator>();

        setCountText();



    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        
        if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        
        if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

          
    }

    // Update is called once per frame

    void setCountText()
    {
        LivesText.text = "lives: " + Lives.ToString();
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    

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
         if (collision.collider.tag == "Enemy")
         {
             Lives -= 1;
             LivesText.text = "lives: " + Lives.ToString();
             Destroy(collision.collider.gameObject);
             
         }
         if (Lives == 0)
         {
             Destroy(rd2d);
             LoseTextObject.SetActive(true);
         }
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue ==4 && flag == 0)
        {
            transform.position = new Vector3(38f, 0.18f, 0f);
            flag ++;
            Lives = 3;
            LivesText.text = "lives: " + Lives.ToString();
        }

        if (scoreValue >=8 && flag == 1)
		{
            winTextObject.SetActive(true);

            musicSource.Stop();

            musicSource.clip = winMusic;
            musicSource.Play();
            flag++;
		}
    
     }

     private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                anim.SetInteger("State", 2);
            }

            
        }
        

        
    }
     
}
