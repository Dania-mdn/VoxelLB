using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : MonoBehaviour
{
    private EnemyOptiuns EnemyOptiuns;

    public WeaponHendler weaponHendler;
    public bool isBAAAM = false;
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public ParticleSystem particleSystem3;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isBAAAM) return;

        particleSystem1.Play();
        particleSystem2.Play();
        particleSystem3.Play();

        Rigidbody body = collision.transform.GetComponent<Rigidbody>();

        FindOptiuns(collision.collider.transform);

        if (body == null) return;

        if (!body.isKinematic)
        {
            collision.gameObject.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 700);
        }
        else
        {

            collision.collider.gameObject.transform.parent = null;
            if (collision.collider.gameObject.transform.GetComponent<Rigidbody>() != null)
                collision.collider.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            else
                collision.collider.gameObject.AddComponent<Rigidbody>();

            if (collision.collider.gameObject.layer == 3)
            {
                weaponHendler.SetArmored();
            }

            EnemyOptiuns.TakeHit();
            /*if (characterController != null)
            {
                characterController.enabled = false;
                characterController.GetComponent<Rigidbody>().isKinematic = false;

                Rigidbody[] childRigidbodies = characterController.GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rb in childRigidbodies)
                {
                    rb.isKinematic = false;
                }
            }*/
        }
    }
    private void FindOptiuns(Transform collision)
    {
        while (collision != null)
        {
            if (collision.gameObject.GetComponent<EnemyOptiuns>() != null)
            {
                EnemyOptiuns = collision.GetComponent<EnemyOptiuns>();
                break;
            }

            collision = collision.parent;
        }
    }
}
