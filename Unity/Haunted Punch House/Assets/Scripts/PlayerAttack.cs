using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public bool attacking = false;
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
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            anim.SetTrigger("punch");
            attacking = true;
            attackTimer = attackCD;
            attackTrigger.enabled = true;
            //transform.Find("AttackTrigger").localPosition = new Vector3((2.23f * (transform.Find("Body").localScale.x)), 0f, 0f);
            //attackTrigger.enabled = true;
        }

        if (attacking)
        {
            anim.SetTrigger("punchOver");
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
