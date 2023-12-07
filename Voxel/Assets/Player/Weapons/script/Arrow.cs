using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject trailRenderer;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void FireArrow()
    {
        transform.parent = null;
        rb.isKinematic = false;
        trailRenderer.SetActive(true);
        rb.AddForce(transform.right * speed, ForceMode.Impulse);
    }
}
