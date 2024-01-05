using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxeAttack : MonoBehaviour
{
    public EnemyOptiuns enemiOptions;
    public EnemySword enemySword;

    public Transform Ray1;
    public Transform Ray2;
    private RaycastHit hit;

    public float rangeAttack;

    private void Update()
    {
        if (Physics.Raycast(Ray1.position, Ray1.forward, out hit, rangeAttack))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("AxeLeft");
            }
        }
        else if (Physics.Raycast(Ray2.position, Ray2.forward, out hit, rangeAttack))
        {
            if (hit.transform.tag == "Player")
            {
                enemiOptions.animatorBody.Play("AxeRight");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemiOptions.Player = other.gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemiOptions.Player = null;
    }
    public void Cuttrue()
    {
        enemySword.cut = true;
    }
    public void Cutfalse()
    {
        enemySword.cut = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(Ray1.transform.position, Ray1.forward * 2.5f);
        Gizmos.DrawRay(Ray2.transform.position, Ray2.forward * 2.5f);
    }
}
