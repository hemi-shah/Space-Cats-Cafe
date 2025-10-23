using UnityEngine;

[CreateAssetMenu(fileName = "CatDefinition", menuName = "Scriptable Objects/CatDefinition")]
public class CatDefinition : ScriptableObject
{
    public string catId;
    public string catName;
    public Sprite catSprite;
}

