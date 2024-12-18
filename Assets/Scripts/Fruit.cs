using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Fruit", order = 1)]
public class Fruit : ScriptableObject
{
    public string Name;
    public Material Material;
    public Mesh Mesh;   
    public int Score;
    public float Size;
}
