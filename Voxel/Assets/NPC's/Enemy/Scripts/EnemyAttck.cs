using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttck : MonoBehaviour
{
    public EnemyOptiuns enemiOptions;

    public GameObject Ray1;
    public GameObject Ray2;
    private RaycastHit hit;

    public bool isArmored = false;
    private void Update()
    {
        if (isArmored) return;

        if (Physics.Raycast(Ray1.transform.position, Ray1.transform.position - transform.position, out hit, 1))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("attacRight");
            }
        }
        else if (Physics.Raycast(Ray2.transform.position, Ray2.transform.position - transform.position, out hit, 1))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("attack");
            }
            enemiOptions.SetPausaMow(true);
        }
        else
        {
            enemiOptions.SetPausaMow(false);
        }
    }
    /*public void SetArmored()
    {
        enemiOptions.animatorBody.Play("armored");
        isArmored = true;
    }
    public void SetArmoredFalse()
    {
        isArmored = false;
    }*/
}
