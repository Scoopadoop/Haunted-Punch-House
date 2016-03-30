using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyPathing : MonoBehaviour
{

    public float moveSpeed = 1;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public int ghost;

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
            transform.Find("Body").transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveSpeed < 0)
        {
            transform.Find("Body").transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            moveSpeed *= -1;

            if (ghost == 1)
            {
                GameObject.Find("PlayerDetector1").GetComponent<EnemyDetectPlayer>().change = true;
            }
            else if (ghost == 2)
            {
                GameObject.Find("PlayerDetector2").GetComponent<EnemyDetectPlayer>().change = true;
            }
            else if (ghost == 3)
            {
                GameObject.Find("PlayerDetector3").GetComponent<EnemyDetectPlayer>().change = true;
            }
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
            col.gameObject.GetComponent<PlayerController>().KnockbackSound();
            
        }

    }

}