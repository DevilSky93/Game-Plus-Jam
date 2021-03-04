using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate() {
        // Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPostion = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        // transform.position = smoothedPostion;
        transform.position = target.position + offset;
    }

}