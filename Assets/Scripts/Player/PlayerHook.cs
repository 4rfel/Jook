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
    const float distance = 17f;
    const float step = 0.02f;
    const float ratio = 2f;

    PlayerPause playerPause;


    void Start() {
        playerPause = GetComponent<PlayerPause>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        hook.enabled = false;
    }

    void Update() {
        if (playerPause.paused)
            return;

        if (joint.distance > .5f) {
            joint.distance -= step;
        } else {
            hook.enabled = false;
            joint.enabled = false;
        }


        if (Input.GetMouseButtonDown(1)) {

            RaycastHit2D hit = Physics2D.Raycast(looking.position, gun.transform.right, distance, mask);

            if (hit.collider != null && hit.collider.gameObject.tag == "wall") {
                joint.enabled = true;

                joint.connectedAnchor = hit.point;

                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(gun.transform.position, hit.point);

                hook.enabled = true;

                hook.SetPosition(0, gun.transform.position);
                hook.SetPosition(1, hit.point);


                float scaleX = Vector3.Distance(gun.transform.position, grabPos) / ratio;
                hook.GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(scaleX, 3f);
            }
        }

        if (joint.connectedBody != null) {
            var trans = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);

            hook.SetPosition(1, trans);
        }

        if (Input.GetMouseButton(1)) {
            // Debug.DrawLine(hook.GetPosition(0), hook.GetPosition(1));
            // Debug.DrawLine(joint.connectedAnchor, joint.connectedAnchor + new Vector2(0, 2));

            hook.SetPosition(0, gun.transform.position);
        }


        if (Input.GetMouseButtonUp(1)) {
            joint.enabled = false;
            hook.enabled = false;
        }
    }
}
