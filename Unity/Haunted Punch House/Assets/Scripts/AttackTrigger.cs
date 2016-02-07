using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

    public int dmg = 5;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
