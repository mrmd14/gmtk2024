using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameEvent", menuName = "GAME/GameEvent")]

public class GameEvent : ScriptableObject
{
    [TextArea] public  string eventText = "";

    public ConditionSequence conditionSequence;
  
    public AttributeActionSequence ResultSequence;

    public  List<GameEvent>  ReadFromForNext;




    public string MakeStr()
    {
        var res = "";
        foreach(var item in conditionSequence.list)
        {
            foreach(var ge in item.gameEvents)
            {
                res += $"<color=#00FFFF>" + ge.eventText + "& </color>";
            }
            res += "& ";
            foreach(var cond in item.list)
            {
                res += $" <color=#CCFFCC> {cond.attribute} </color> {cond.compare}   <color=#CC9933>{cond.value} </color> & ";
            }
        }


        res += $"  *** {eventText} ***";

        foreach (var item in ResultSequence.attributeActions)
        {

            res += $" <color=#FF3333> {item.attribute} </color>   <color=#CC33CC>{item.AddValue} </color>  ";
        }
        Debug.Log(res);
        return res;
    }

    public  float  SetScore()
    {
        if (conditionSequence.isMet()) currentScore = conditionSequence.score;
        else currentScore = -1000;
        return currentScore;
    }

   [HideInInspector] public float currentScore = 0;


    

   
}
