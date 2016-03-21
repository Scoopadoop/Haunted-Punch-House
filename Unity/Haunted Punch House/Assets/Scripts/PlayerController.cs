using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public float enemies = 3;
    public GameObject button;
    public AudioClip PC_Attack;
    public AudioClip PC_Flinch;
    public AudioClip PC_Death;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded; //true on ground, false in air
    public float punchTimer = 1.0f; //duration of punch hitbox 
    public bool punchActive; //is punch active?
    public int playerHealth; //player's HP
    public bool isDead = false;

    public Sprite[] HealthSprites;
    public Sprite HealthUI;

    private Rigidbody2D rb2d;
    private PlayerController player;

    public bool knockFromRight;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerHealth = 20;
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }


    // Update is called once per frame
    void Update()
    {
        punchTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
  
        if ((Input.GetKeyDown(KeyCode.Space) && grounded || (Input.GetKeyDown(KeyCode.W)) && grounded))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);//gets value of character that he already has while moving sideways
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);//gets value of character that he already has while moving up or down
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            if (punchTimer <= 0.0f)
            {
                punchActive = false;
                punchTimer = 1.0f;
                transform.Find("a_wahler_pc_upperArm_Right").transform.Find("a_wahler_pc_foreArm_Right").GetComponent<Rigidbody2D>().velocity = new Vector2(50 * (transform.Find("Body").localScale.x), 25);
                gameObject.GetComponent<AudioSource>().PlayOneShot(PC_Attack);
            }
           
        }

        if (GetComponent<Rigidbody2D>().velocity.x > 3.0)
        {
            transform.Find("Body").transform.localScale = new Vector3(1f, 1f, 1f);
            /*Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.localScale = new Vector3(1, 1, 1);
            }
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);*/
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < -3.0)
        {
            transform.Find("Body").transform.localScale = new Vector3(-1f, 1f, 1f);
            /*Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.localScale = new Vector3(-1, 1, 1);
            }
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);*/
        }

        if (playerHealth <= 0)
        {
            if(!isDead)
            {
                StartCoroutine(PlayerDeath());
                isDead = true;
            }
            
        }
        if (enemies <= 2)
        {
            Destroy(GameObject.Find("Tentacle1"));
            Destroy(GameObject.Find("Tentacle 1"));
            if (enemies <= 1)
            {
                Destroy(GameObject.Find("Tentacle2"));
                Destroy(GameObject.Find("Tentacle 2"));
                if (enemies <= 0)
                {
                    Destroy(GameObject.Find("Tentacle3"));
                    Destroy(GameObject.Find("Tentacle 3"));
                    GameObject.Find("UrnNewFull").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("UrnNewFull").GetComponent<SpriteRenderer>().enabled = true;
                    button.SetActive(true);
                    Cursor.visible = true;
                }
            }
        }

        HealthUI = HealthSprites[player.playerHealth];
        

    }

    

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Finish")
        {
            
            Application.Quit();
        }

    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;

            if (knockFromRight)
            {
                rb2d.AddForce(new Vector3(knockbackDir.x * -500, 1000, transform.position.z));
            }
            if (!knockFromRight)
            {
                rb2d.AddForce(new Vector3(knockbackDir.x * 500, 1000, transform.position.z));
            }

			gameObject.GetComponent<Animation>().Play ("hurt_flash");
        }
        yield return 0;

    }
    public void KnockbackSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(PC_Flinch);
    }

    IEnumerator PlayerDeath()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(PC_Death);
        //gameObject.GetComponent<Animation>().Play("death animation");
        //insert the death animation in the above line

        LifeCounter.lives--;
        yield return new WaitForSeconds(4);

        Application.LoadLevel(Application.loadedLevel);
    }
       

}
