using UnityEngine;

public enum Drizzle
{
    None,
    Chocolate,
    Caramel
}

[CreateAssetMenu(menuName = "Coffee/Drizzle")]
public class DrizzleDefinition : ScriptableObject
{
    public Drizzle DrizzleType;
}