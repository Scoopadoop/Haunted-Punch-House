using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public float enemies = 3;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded; //true on ground, false in air
    public float punchTimer = 2.0f; //duration of punch hitbox 
    public bool punchActive; //is punch active?
    public int playerHealth; //player's HP

    private Rigidbody2D rb2d;

    public bool knockFromRight;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerHealth = 5;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && grounded)
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
        if (Input.GetKey(KeyCode.Space))
        {
            while (punchTimer > 0.0f)
            {
                // Decrease timeLimit.
                punchTimer -= Time.deltaTime;
                punchActive = true;
            }
            if (punchTimer <= 0.0f)
            {
                punchActive = false;
                punchTimer = 2.0f;
            }
            transform.Find("a_wahler_pc_upperArm_Right").transform.Find("a_wahler_pc_foreArm_Right").GetComponent<Rigidbody2D>().velocity = new Vector2(50 * (transform.Find("Body").localScale.x), 25);
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
            //gameObject.GetComponent<Animation>().Play("death animation");
            //insert the death animation in the above line
            Application.LoadLevel(Application.loadedLevel);
        }
        if (enemies == 0)
        {
            GameObject.Find("UrnNewFull").GetComponent<BoxCollider2D>().enabled = true;
        }

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
        }
        yield return 0;
    }

}
