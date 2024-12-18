using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Camera Camera;
    public Vector3 Bounds;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Camera.transform.position = new Vector3(0, 3.5f, 0);
    }

    void Update() {
        float radius = Vector3.Magnitude(Bounds);
        float minD = Mathf.Sin(Mathf.Deg2Rad * Camera.fieldOfView / 2);
        Vector3 dir = Camera.transform.position / Vector3.Magnitude(Camera.transform.position - Vector3.zero);
        Camera.transform.position = (radius / minD) * dir;
        Camera.transform.LookAt(Vector3.zero);
        Camera.nearClipPlane = minD;
    }
}
