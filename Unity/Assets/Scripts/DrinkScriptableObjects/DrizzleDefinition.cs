using UnityEngine;

public enum DrizzleType
{
    None,
    Chocolate,
    Caramel
}

[CreateAssetMenu(menuName = "Coffee/Drizzle")]
public class DrizzleDefinition : ScriptableObject
{
    public DrizzleType DrizzleType;
}