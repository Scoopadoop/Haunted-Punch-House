using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded; //true on ground, false in air
    public float punchTimer = 2.0f; //duration of punch hitbox 
    public bool punchActive; //is punch active?
    

	// Use this for initialization
	void Start () 
    {
	
	}
	
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }


	// Update is called once per frame
	void Update () 
    {
	
        if(Input.GetKeyDown (KeyCode.W) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);//gets value of character that he already has while moving sideways
        }

        if (Input.GetKey (KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);//gets value of character that he already has while moving up or down
        }

        if (Input.GetKey (KeyCode.A))
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
        }

        if(GetComponent<Rigidbody2D>().velocity.x > 0.5)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            }
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < -0.5)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            }
        }
	}
}
