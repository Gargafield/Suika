using UnityEngine;

public class Pointer : MonoBehaviour
{
    void Update() {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f);
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.x = Mathf.Clamp(pos.x, -0.875f, 0.875f);
        pos.y = -0.5f;
        pos.z = Mathf.Clamp(pos.z, -0.875f, 0.875f);
        transform.position = pos;
    }
}
