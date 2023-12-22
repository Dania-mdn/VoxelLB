using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttck : MonoBehaviour
{
    public EnemyOptiuns enemiOptions;

    public Transform Ray1;
    public Transform Ray2;
    private RaycastHit hit;

    public bool isArmored = false;
    private void Update()
    {
        if (isArmored) return;

        if (Physics.Raycast(Ray1.position, Ray1.position - transform.position, out hit, 1.5f))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("attacRight");
            }
        }
        else if (Physics.Raycast(Ray2.position, Ray2.forward, out hit, 1))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("attack");
            }
            enemiOptions.isStop = true;
        }
        else
        {
            enemiOptions.isStop = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(Ray1.transform.position, Ray1.position - transform.position * 1);
        Gizmos.DrawRay(Ray2.transform.position, Ray2.forward * 1);
    }
}
