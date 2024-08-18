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
        float currentValue = AttributeData.CurrentBaseValue[keyAttribute];
        foreach (var item in list)
        {
            if ((currentValue > 0 && currentValue >= item.position && item.position>0  ) 
                || (currentValue < 0 && currentValue <= item.position && item.position<0  ))
            {
                item.sequence.Do();
            }
        }
    }
}
