using System.Threading;
using System;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{
    private BoxCollider bc;
    private Rigidbody rb;
    public GameObject trailRenderer;
    public float speed;
    private EnemyCut enemyCut;
    public float Damage;
    private PlayerOptions PlayerOptions;

    private bool isCut = true;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        bc.enabled = false;

        enemyCut = GetComponent<EnemyCut>();
    }
    public void FireArrow()
    {
        transform.parent = null;
        trailRenderer.SetActive(true);
        bc.enabled = true;
    }
    private void Update()
    {
        if (rb.useGravity == false && bc != null)
            rb.velocity = transform.forward * speed;

        if(!isCut && transform.parent == null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isCut) return;

        if (collision.gameObject.layer == 8) return;

        var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        trailRenderer.transform.parent = null;
        Invoke("DestroyTrayl", 2);
        Destroy(gameObject, 3);

        rb.useGravity = true;

        if (collision.gameObject.layer != 3)
        {
            FindOptiuns(collision.collider.transform);

            if (PlayerOptions != null)
            {
                if (PlayerOptions.TakeHit(Damage))
                {
                    if (collision.collider.GetComponent<MeshRenderer>() != null)
                    {
                        enemyCut.Cutt(collision.collider.gameObject, timeLimit);
                    }
                    else
                    {
                        rb.useGravity = false;
                        rb.isKinematic = true;
                        gameObject.transform.parent = collision.transform;
                    }
                    isCut = false;
                }
                else
                {
                    EventSystem.DoAttackHealth(Damage);
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    gameObject.transform.parent = collision.transform;
                    isCut = false;
                }
            }
            else
            {
                if (collision.collider.GetComponent<MeshRenderer>() != null)
                {
                    enemyCut.Cutt(collision.collider.gameObject, timeLimit);
                }
                else
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    gameObject.transform.parent = collision.transform;
                }
            }
            
            Destroy(bc); 
        }

        if (collision.transform.GetComponent<Rigidbody>() != null)
            collision.transform.GetComponent<Rigidbody>().AddForce(this.transform.right * 200);
    }
    private void DestroyTrayl()
    {
        Destroy(trailRenderer);
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
            else if (collision.gameObject.GetComponent<EnemyOptiuns>() != null)
            {
                enemyCut.enemyOptiuns = collision.GetComponent<EnemyOptiuns>();
                break;
            }

            collision = collision.parent;
        }
    }
}
