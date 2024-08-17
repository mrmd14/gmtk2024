using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ConditionSequence 
{

    public float score = 0;
    public List<Condition> list;


    public bool isMet()
    {
        foreach(var item in list)
        {
            if (item.IsMet()) return true;

        }

        return  (list.Count == 0) ;


    }



   
}

[System.Serializable]
public class Condition
{
    public List< GameEvent> gameEvents;

    public List<ConditionItem> list;

    public bool IsMet()
    {
        foreach(var item in gameEvents)
        {
            if (!GamePlayManager.instance.triggered.ContainsKey(item))
            {
                return false;
            }
        }
        foreach (var item in list)
        {
            if (!item.IsMet()) return false;
        }



        return true;



    }
}

[System.Serializable]
public class ConditionItem
{

    public Compare compare;
    public Attributes attribute;




    public float value = 0;


    public bool  IsMet()
    {
        float keyVal = AttributeData.values[(int)attribute];
        bool isMet = false;

        switch (compare)
        {
            case Compare.Less:
                isMet = keyVal < value ;
                break;
            case Compare.LessOrEqual:
                isMet = keyVal <= value;
                break;
            case Compare.Equal:
                isMet = value == keyVal;
                break;
            case Compare.GreaterOrEqual:
                isMet = keyVal >=  value ;
                break;
            case Compare.Greater:
                isMet = keyVal > value;
                break;
            
        }

        return isMet;


    }
}


public enum Compare
{
    Less = 0,
    LessOrEqual = 1,
    Equal = 2,
    GreaterOrEqual = 3,
    Greater = 4,
 

}

