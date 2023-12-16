using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public EnemyAttck enemyAttck;

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag != "dontCut")
        {
            if (collision.collider.gameObject.tag == "Armour")
            {
                enemyAttck.SetArmored();
                Invoke("setArmored", 1);
            }
        }
    }
    private void setArmored()
    {
        enemyAttck.SetArmoredFalse();
    }*/
}
