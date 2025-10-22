using UnityEngine;

public enum TemperatureType
{
    Hot,
    Cold
}

[CreateAssetMenu(menuName = "Coffee/Temperature")]
public class TemperatureDefinition : ScriptableObject
{
    public TemperatureType TemperatureType;
}
