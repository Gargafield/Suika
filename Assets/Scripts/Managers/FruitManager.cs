
using System.Collections;
using System.Linq;
using UnityEngine;

public class FruitManager : MonoBehaviour {
    public static FruitManager Instance;
    
    public FruitCollection FruitCollection;
    public GameObject FruitPrefab;
    public Vector3 DefaultSize = new Vector3(0.125f, 0.125f, 0.125f);
    private long index = 0;
    private Debouncer _debouncer = new Debouncer(0.05f);
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public GameObject SpawnFirst(Vector3 position) {
        return Spawn(FruitCollection.Fruits.First(), position);
    }
    
    public GameObject Spawn(Fruit fruit, Vector3 position) {
        // Random rotation
        GameObject fruitObject = Instantiate(FruitPrefab, position, Quaternion.Euler(Random.Range(0, 360), 0, 0));
        fruitObject.GetComponent<MeshFilter>().mesh = fruit.Mesh;
        fruitObject.GetComponent<MeshRenderer>().material = fruit.Material;
        fruitObject.GetComponent<Rigidbody>().mass = fruit.Size * fruit.Size;
        fruitObject.transform.localScale = DefaultSize * fruit.Size;
        fruitObject.GetComponent<FruitController>().Fruit = fruit;
        fruitObject.GetComponent<FruitController>().Index = index++;
        
        fruitObject.GetComponent<SphereCollider>().enabled = true;
        
        GameManager.Instance.AddScore(fruit.Score);
        return fruitObject;
    }
    
    public void HandleCollision(GameObject fruit1, GameObject fruit2) {
        Debug.Log(GameManager.Instance.State);
        
        if (GameManager.Instance.State != GameState.Playing || !_debouncer.CanExecute()) {
            return;
        }
        
        FruitController fruitController1 = fruit1.GetComponent<FruitController>();
        Fruit fruit = fruitController1.Fruit;
        int nextIndex = FruitCollection.Fruits.IndexOf(fruit) + 1;
        if (nextIndex >= FruitCollection.Fruits.Count) {
            return;
        }
        
        _debouncer.Reset();
        Fruit nextFruit = FruitCollection.Fruits[nextIndex];
        GameManager.Instance.SubtractScore(fruit.Score * 2);
        
        Vector3 position = (fruit1.transform.position + fruit2.transform.position) / 2;
        Destroy(fruit1);
        Destroy(fruit2);
        Spawn(nextFruit, position);
    }
    
    public async Awaitable Reset() {
        index = 0;
        
        GameObject fruitObject = GameObject.FindGameObjectWithTag("Fruit");
        while (fruitObject != null) {
            Destroy(fruitObject);
            await Awaitable.WaitForSecondsAsync(0.05f);
            fruitObject = GameObject.FindGameObjectWithTag("Fruit");
        }
    }
}