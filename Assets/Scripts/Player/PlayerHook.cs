using MLAPI;
using UnityEngine;

// https://github.com/DonHaul/2D-Tuts/blob/master/Assets/51,52%20-%20GrapplingHook/grapplinghook.cs
public class PlayerHook : NetworkedBehaviour {
    [SerializeField] LineRenderer hook;
    [SerializeField] LayerMask mask;
    [SerializeField] Vector3 grabPos;
    [SerializeField] Transform looking;
    [SerializeField] Camera cam;
    [SerializeField] GameObject gun;



    DistanceJoint2D joint;
    const float distance = 100f;
    const float step = 0.02f;
    const float ratio = 2f;


    void Start() {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        hook.enabled = false;
    }

    void Update() {
        if (joint.distance > .5f) {
            joint.distance -= step;
        } else {
            hook.enabled = false;
            joint.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.E)) {

            // RaycastHit2D hit = Physics2D.Raycast(looking.position, looking.right, distance, mask);
            RaycastHit2D hit = Physics2D.Raycast(looking.position, gun.transform.right, distance, mask);

            // if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {
            if (hit.collider != null) {
                joint.enabled = true;
                Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                connectPoint.x /= hit.collider.transform.localScale.x;
                connectPoint.y /= hit.collider.transform.localScale.y;


                joint.connectedAnchor = connectPoint;

                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                // joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                hook.enabled = true;
                hook.SetPosition(0, transform.position);
                hook.SetPosition(1, hit.point);


                float scaleX = Vector3.Distance(transform.position, grabPos) / ratio;
                hook.GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(scaleX, 1f);


            }
        }

        if (joint.connectedBody != null) {
            var trans = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);

            hook.SetPosition(1, trans);
        }

        if (Input.GetKey(KeyCode.E))
            hook.SetPosition(0, transform.position);


        if (Input.GetKeyUp(KeyCode.E)) {
            joint.enabled = false;
            hook.enabled = false;
        }
    }
}
