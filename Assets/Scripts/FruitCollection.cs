using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FruitCollection", order = 1)]
public class FruitCollection : ScriptableObject
{
    public List<Fruit> Fruits;
}
