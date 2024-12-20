using UnityEngine;

public class FruitDisplay : MonoBehaviour
{
    public GameObject title;
    public GameObject score;
    public GameObject meshRenderer;
    public float speed = 0.25f;
    
    public Fruit Fruit;
    
    
    void Start() {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Fruit.Name;
        score.GetComponent<TMPro.TextMeshProUGUI>().text = Fruit.Score.ToString();
        meshRenderer.GetComponent<MeshFilter>().mesh = Fruit.Mesh;
        meshRenderer.GetComponent<MeshRenderer>().material = Fruit.Material;
    }

    void Update() {
        meshRenderer.transform.rotation = Quaternion.Euler(
            Mathf.Sin(Time.time * speed) * 360,
            Mathf.Cos(Time.time * speed) * 360,
            Mathf.Tan(Time.time * speed) * 360
        );
    }
}
