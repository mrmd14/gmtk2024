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

    public void DoOnBase()
    {
        foreach (var item in attributeActions)
        {
            item.DoOnBase();
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

        AttributeData.values[attribute] =  Mathf.Clamp(AttributeData.values[attribute] + AddValue, range.MinVal,range.MaxVal ) ;
    }

    public void DoOnBase()
    {
        
        if (!GamePlayManager.AttributeInitValMap.ContainsKey(attribute)) return;
        var range = GamePlayManager.AttributeInitValMap[attribute];

        AttributeData.CurrentBaseValue[attribute] = Mathf.Clamp(AttributeData.CurrentBaseValue[attribute] + AddValue, range.MinVal, range.MaxVal);
    }
}