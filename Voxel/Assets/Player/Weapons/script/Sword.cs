using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME.Extensions;
using System.Threading;
using System;

public class Sword : MonoBehaviour
{
	public WeaponHendler weaponHendler;

    private Cut Cut;

    public bool isCut = false;
	public ParticleSystem ParticleSystem;

    private void Start()
    {
        Cut = GetComponent<Cut>();
    }
    private void OnCollisionEnter(Collision collision)
    {
		if (!isCut) return;
		
        var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        ParticleSystem.Play(); 

		if (collision.collider.gameObject.layer == 3)
        {
            weaponHendler.SetArmored();
            return;
        }
        else
        {
            FindOptiuns(collision.collider.transform);
        }

        if (collision.collider.GetComponent<MeshRenderer>() != null)
        {
            Cut.Cutt(collision.collider.gameObject, timeLimit);
        }
    }
    private void FindOptiuns(Transform collision)
    {
        while (collision != null)
        {
            if (collision.gameObject.GetComponent<EnemyOptiuns>() != null)
            {
                Cut.enemyOptiuns = collision.GetComponent<EnemyOptiuns>();
                break;
            }

            collision = collision.parent;
        }
    }

}
