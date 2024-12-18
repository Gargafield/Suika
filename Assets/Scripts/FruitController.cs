using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public Fruit Fruit;
    public long Index;
    private List<GameObject> _touching = new List<GameObject>();
    
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Fruit")) {
            _touching.Add(collision.gameObject);
        }
    }
    
    public void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Fruit")) {
            _touching.Remove(collision.gameObject);
        }
    }
    
    public void Pop() {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        Destroy(gameObject, 1);
    }
    
    public void Update() {
        foreach (GameObject touching in _touching) {
            if (touching == null) {
                _touching.Remove(touching);
                break;
            }
            
            if (touching.GetComponent<FruitController>().Fruit.Score == Fruit.Score
                && touching.GetComponent<FruitController>().Index > Index) {

                // Get FruitManager component from the manager object
                FruitManager.Instance.HandleCollision(touching, gameObject);
            }
        }
    }
}
