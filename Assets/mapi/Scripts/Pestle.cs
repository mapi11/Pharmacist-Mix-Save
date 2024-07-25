using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    [SerializeField] private float maxRotationDegrees = 20f;
    private Rigidbody2D rb;

    [Space]
    [Header("Center of mass")]
    [SerializeField] private Vector2 CenterMass;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().centerOfMass = CenterMass;
    }

    void FixedUpdate()
    {
        ClampRotation();
    }

    void ClampRotation()
    {
        float zRotation = rb.rotation;
        zRotation = Mathf.Clamp(zRotation, -maxRotationDegrees, maxRotationDegrees);
        rb.rotation = zRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetComponent<Rigidbody2D>().worldCenterOfMass, 0.1f);
    }
}