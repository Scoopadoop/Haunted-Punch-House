using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;
    private float attackTimer = 1;
    private float attackCD = 0.3f;
    private Animator anim;

    public Collider2D attackTrigger;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            attacking = true;
            attackTimer = attackCD;
            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
    }
}
