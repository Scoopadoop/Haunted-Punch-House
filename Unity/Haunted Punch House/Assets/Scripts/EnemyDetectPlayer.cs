using UnityEngine;
using System.Collections;

public class EnemyDetectPlayer : MonoBehaviour {

    public int ghost;
    public bool change = false;
    float curPos;

    // Use this for initialization
    void Start()
    {
        curPos = transform.localPosition.x;
    }

        // Update is called once per frame
    void Update()
    {
        if (change == true)
        {
            curPos = transform.localPosition.x;
            curPos *= -1f;
            transform.Translate(curPos + curPos, 0f, 0f);

            change = false;
        }

    }

        void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Player"))
        {
            if (ghost == 1)
            {
                GameObject.Find("Ghost1").GetComponent<EnemyPathing>().moveSpeed *= -1;
                change = true;
            }
            else if (ghost == 2)
            {
                GameObject.Find("Ghost2").GetComponent<EnemyPathing>().moveSpeed *= -1;
                change = true;
            }
            else if (ghost == 3)
            {
                GameObject.Find("Ghost3").GetComponent<EnemyPathing>().moveSpeed *= -1;
                change = true;
            }
        }
    }
}
