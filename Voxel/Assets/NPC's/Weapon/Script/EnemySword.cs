using BLINDED_AM_ME.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public bool cut = false;

    private PlayerOptions PlayerOptions;
    public EnemyOptiuns enemyOptiuns;
    public EnemyCut enemyCut;

    private float time; 
    public bool cuttime = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!cut) return;

        var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        FindOptiuns(collision.collider.transform);

        if (collision.collider.gameObject.layer == 3)
        {
            return;
        }

        if (PlayerOptions != null)
        {
            if (PlayerOptions.TakeHit(enemyOptiuns.Damage))
            {
                if (collision.collider.GetComponent<MeshRenderer>() != null)
                {
                    enemyCut.Cutt(collision.collider.gameObject, timeLimit);
                }
            }
            else
            {
                EventSystem.DoAttackHealth(enemyOptiuns.Damage);
                cut = false;
            }
        }
        else
        {
            if (collision.collider.GetComponent<MeshRenderer>() != null)
            {
                enemyCut.Cutt(collision.collider.gameObject, timeLimit);
            }
        }
    }
    private void FindOptiuns(Transform collision)
    {
        while (collision != null)
        {
            if (collision.gameObject.GetComponent<PlayerOptions>() != null)
            {
                PlayerOptions = collision.GetComponent<PlayerOptions>();
                break;
            }

            collision = collision.parent;
        }
    }
}
