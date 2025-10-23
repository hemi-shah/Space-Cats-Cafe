using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatCatalog", menuName = "Scriptable Objects/CatCatalog")]
[System.Serializable]
public class CatCatalog : ScriptableObject
{
    public List<CatDefinition> playerCats;    // AsteroidCat, NeptuneCat, etc.
    public List<CatDefinition> customerCats;  // Different cats for customers
}