using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour
{

    public float moveSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded; //true on ground, false in air

    private PlayerController player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }


    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);

        if (moveSpeed > 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1f);
        }
        else if (moveSpeed < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1f);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            moveSpeed *= -1;
        }


        if (col.gameObject.tag == "Player")
        {
            if (col.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }

            StartCoroutine(player.Knockback(0.02f, 300, player.transform.position));
            player.playerHealth -= 1;
        }

    }


}