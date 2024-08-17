using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ConditionSequence 
{
    public List<Condition> list;



    public float SumScore()
    {
        float res = 0;
        foreach(var item in list)
        {
            res += item.GetScore();
        }

        return res;
    }
}

[System.Serializable]
public class Condition
{
    public float score = 0;
    public Compare compare;
    public Attributes attribute;
    public float value = 0;


    public float GetScore()
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

        return (isMet ? score : 0);


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

