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


    public  float  SetScore()
    {
        currentScore = 0;
        foreach(var item in conditionSequence.list)
        {
            currentScore += item.GetScore();
        }

        return currentScore;
    }

   [HideInInspector] public float currentScore = 0;


    

   
}
