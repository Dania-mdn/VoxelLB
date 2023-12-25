using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    public GameObject trailRenderer;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
    }
    public void FireArrow()
    {
        transform.parent = null;
        rb.isKinematic = false;
        trailRenderer.SetActive(true);
        rb.AddForce(transform.right * speed, ForceMode.Impulse);
        bc.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) return;

        if (other.transform.GetComponent<Rigidbody>() != null)
            other.transform.GetComponent<Rigidbody>().AddForce(this.transform.right * 200);

        trailRenderer.transform.parent = null;
        Invoke("setTrailRenderer", 2);

        if (other.gameObject.layer == 3)
        {
            bc.isTrigger = false;
            rb.velocity = Vector3.zero;
            transform.position = transform.position - transform.right * 2;
        }
        else
        {
            gameObject.transform.parent = other.transform;
            rb.isKinematic = true;
            transform.position = transform.position - transform.right * 1;
            bc.enabled = false;
        }
    }
    private void setTrailRenderer()
    {
        trailRenderer.SetActive(false);
    }
}
