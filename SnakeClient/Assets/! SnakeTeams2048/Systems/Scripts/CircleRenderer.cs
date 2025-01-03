using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public int SubDivisions = 10;

    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            Refresh();
        }
    }

    public void Refresh()
    {
        float angleStep = 2f * Mathf.PI / SubDivisions;
        lineRenderer.positionCount = SubDivisions;

        for (int i = 0; i < SubDivisions; i++)
        {
            var postionX = transform.localScale.x * 0.5f * Mathf.Cos(angleStep * i);
            var postionY = transform.localScale.y * 0.5f * Mathf.Sin(angleStep * i);
            lineRenderer.SetPosition(i, transform.position + new Vector3(postionX, postionY, 0));
        }
    }
}
