using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent <Rigidbody2D>();

        if (rb != null)
        {
            StartCoroutine(FreezePositionForTime(rb, 0.05f));
        }

        other.transform.position = teleportTarget.position;

        TargetJoint2D targetJoint = other.GetComponent<TargetJoint2D>();

        if (targetJoint != null)
        {
            Destroy(targetJoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            StartCoroutine(FreezePositionForTime(rb, 0.05f));
        }

        other.transform.position = teleportTarget.position;

        TargetJoint2D targetJoint = other.GetComponent<TargetJoint2D>();

        if (targetJoint != null)
        {
            Destroy(targetJoint);
        }
    }

    IEnumerator FreezePositionForTime(Rigidbody2D rb, float time)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(time);

        rb.constraints = RigidbodyConstraints2D.None;
    }
}
