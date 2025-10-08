using System.Collections.Generic;
using UnityEngine;

public class Drink
{
    private int _orderNumber;
    private bool _isHot;
    private bool _isCold;
    private List<string> _customizations = new List<string>();
    private List<string> _assembly = new List<string>();
    private List<string> _toppings = new List<string>();
    
    public Drink(bool isHot)
    {
        _isHot = isHot;
        _isCold = !isHot;
        _customizations = new List<string>();
        _assembly = new List<string>();
        _toppings = new List<string>();
    }
    public Drink(int orderNumber, bool isHot, bool isCold, List<string> customizations, List<string> assembly, List<string> toppings)
    {
        _orderNumber = orderNumber;
        _isHot = isHot;
        _isCold = isCold;
        _customizations = customizations;
        _assembly = assembly;
        _toppings = toppings;
    }
}
