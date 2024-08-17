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

        if (!GamePlayManager.AttributeInitValMap.ContainsKey(attribute)) return;
        var range = GamePlayManager.AttributeInitValMap[attribute];

        AttributeData.values[(int)attribute] =  Mathf.Clamp(AttributeData.values[(int)attribute] + AddValue, range.MinVal,range.MaxVal ) ;
    }
}