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
        if (conditionSequence.isMet()) currentScore = conditionSequence.score;
        else currentScore = -1000;
        return currentScore;
    }

   [HideInInspector] public float currentScore = 0;


    

   
}
