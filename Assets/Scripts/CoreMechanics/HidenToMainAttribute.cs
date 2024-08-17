using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HidenToMainAttribute", menuName = "GAME/HidenToMainAttribute")]

public class HidenToMainAttribute  : ScriptableObject
{
    public Attributes keyAttribute;
    public List<HidenToMainAttributeItem> list;


    public void Set()
    {
        float currentValue = AttributeData.CurrentBaseValue[(int)keyAttribute];
        foreach (var item in list)
        {
            if ((currentValue > 0 && currentValue >= item.position) || (currentValue < 0 && currentValue <= item.position))
            {
                item.sequence.Do();
            }
        }
    }
}
