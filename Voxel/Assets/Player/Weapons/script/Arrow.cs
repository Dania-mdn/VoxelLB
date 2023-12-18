using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    public GameObject trailRenderer;
    public float speed;

    //[SerializeField] float aimSmoothSpeed = 20;

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
    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        /*if (Physics.Raycast(transform.position, transform.right, out RaycastHit hit, Mathf.Infinity))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);*/
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponent<Rigidbody>() != null)
            other.transform.GetComponent<Rigidbody>().AddForce(this.transform.right * 200);
        Invoke("setTrailRenderer", 2);

        if (other.gameObject.tag != "Armour")
        {
            gameObject.transform.parent = other.transform;
            rb.isKinematic = true;
            //transform.position = transform.position - transform.right * 1;
            bc.enabled = false;
        }

    }
    private void setTrailRenderer()
    {
        trailRenderer.SetActive(false);
    }
}
