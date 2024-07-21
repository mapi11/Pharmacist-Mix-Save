using UnityEngine;

public class DrugAndDropScript : MonoBehaviour
{
    [SerializeField] private LayerMask _PhysicsLayer;

    [SerializeField] private float _damping = 1.0f;
    [SerializeField] private float _frequency = 5.0f;

    private TargetJoint2D TargetJoint2D;

    void Update()
    {
         var _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            var collider = Physics2D.OverlapPoint(_worldPos, _PhysicsLayer);
            if (!collider)
                return;

            var body = collider.attachedRigidbody;
            if (!body)
                return;

            TargetJoint2D = body.gameObject.AddComponent<TargetJoint2D>();

            TargetJoint2D.dampingRatio = _damping;
            TargetJoint2D.frequency = _frequency;
            TargetJoint2D.anchor = TargetJoint2D.transform.InverseTransformPoint(_worldPos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Destroy(TargetJoint2D);
            TargetJoint2D = null;
            return;
        }

        if (TargetJoint2D)
        {
            TargetJoint2D.target = _worldPos;
        }
    }
}