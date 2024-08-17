using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class AttributeActionSequence 
{
    public List<AttributeAction> attributeActions;

    public void Do()
    {
        foreach(var item in attributeActions)
        {
            item.Do();
        }
    }
}

[System.Serializable]
public class AttributeAction
{
    public float AddValue = 0;
    public Attributes attribute;

    public void Do()
    {
        AttributeData.values[(int)attribute] += AddValue;
    }
}