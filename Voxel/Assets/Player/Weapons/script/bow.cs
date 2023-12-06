using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Transform top;
    public Transform bot;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, top.position);
        lineRenderer.SetPosition(1, bot.position);
    }
}
