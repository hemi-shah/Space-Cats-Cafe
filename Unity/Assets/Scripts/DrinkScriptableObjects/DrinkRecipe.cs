using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Coffee/DrinkRecipe")]
public class DrinkRecipe : ScriptableObject {
    public string baseName; // e.g. "Latte", "Americano"

    public TemperatureType temperature = TemperatureType.Hot;
    [Range(0,3)] public int iceCubes = 0; // used only if temperature == Iced

    public MilkDefinition milk; // assign "None" milk as an SO if needed
    public SyrupDefinition syrup; // can be null for no syrup
    public DrizzleDefinition drizzle; // can be null for no topping

    public bool hasWhippedCream;
    public bool isEmpty = false; // to represent empty cup asset state
    public Sprite overrideSprite; // optional override, otherwise compose or choose base sprite
    
    private ILogger logger = new DebugLogger();

    // helper: compute display name
    public string GetDisplayName() {
        if (isEmpty) 
            return "Empty Cup";
        
        StringBuilder parts = new System.Text.StringBuilder();
        
        if (temperature == TemperatureType.Cold) 
            parts.Append("Iced ");
        parts.Append(baseName);
        if (milk != null && milk.MilkType != MilkType.None) 
            parts.Append($" ({milk.MilkType})");
        if (syrup != null) 
            parts.Append($" with {syrup.SyrupType}");
        if (drizzle != null && hasWhippedCream) 
            parts.Append($" + {drizzle.DrizzleType}");
        
        return parts.ToString();
    }
}