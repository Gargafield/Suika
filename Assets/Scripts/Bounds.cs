using UnityEngine;

public class Bounds : MonoBehaviour
{
    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Fruit")) {
            _ = GameManager.Instance.GameOver();
        }
    }
}
