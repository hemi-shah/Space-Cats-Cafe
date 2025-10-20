using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatCatalog", menuName = "Scriptable Objects/CatCatalog")]
public class CatCatalog : ScriptableObject
{
    public List<CatDefinition> cats;
}
