using System.Threading;
using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private BoxCollider bc;
    private Rigidbody rb;
    public GameObject trailRenderer;
    public float speed;
    private Cut Cut;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        bc.enabled = false;

        Cut = GetComponent<Cut>();
    }
    public void FireArrow()
    {
        transform.parent = null;
        trailRenderer.SetActive(true);
        bc.enabled = true;
    }
    private void Update()
    {
        if (rb.useGravity == false && transform.parent == null)
            rb.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) return;

        var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        trailRenderer.transform.parent = null;
        Invoke("DestroyTrayl", 2);
        Destroy(gameObject, 3);

        rb.useGravity = true;

        if (collision.gameObject.layer != 3)
        {
            FindOptiuns(collision.collider.transform); 

            if (collision.collider.GetComponent<MeshRenderer>() != null)
            {
                Cut.Cutt(collision.collider.gameObject, timeLimit);
            }
            else
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                gameObject.transform.parent = collision.transform;
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
            if (collision.gameObject.GetComponent<EnemyOptiuns>() != null)
            {
                Cut.enemyOptiuns = collision.GetComponent<EnemyOptiuns>();
                break;
            }

            collision = collision.parent;
        }
    }
}
