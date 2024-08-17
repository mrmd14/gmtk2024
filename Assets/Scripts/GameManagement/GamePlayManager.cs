using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{

    public GameEvents gameEvents;

    public static bool isPlayerTurn = false;

    private List<GameEvent> runtTimeGameEvents = new List<GameEvent>();





    public static GamePlayManager instance;


    public GeneralGameData data;

    private List<GameEvent> randomList= new List<GameEvent>();

    public Text LastEventText;
    public Text stats;


    public GameEvent last;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerEvent(GameEvent gameEvent)
    {
        LastEventText.text =  gameEvent.eventText;
        foreach(var item in gameEvent.ResultSequence.attributeActions)
        {
            item.Do();
        }

        isPlayerTurn = true;
    }


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < 5; ++i)
        {
            AttributeData.values[i] = data.statsInitalVal;
        }


        runtTimeGameEvents.Clear();
        foreach (var item in gameEvents.mainPool)
        {
            runtTimeGameEvents.Add(item);
        }

        RunRandomEvent();
 

    }

    private void RunScoredEvent()
    {



        // float Set Score and find max 
        float maxi = -1000;
        var destList = last.ReadFromForNext.Count == 0 ? runtTimeGameEvents : last.ReadFromForNext;
        
        foreach (var item in destList)
        {
            
           maxi  = Mathf.Max(maxi, item.SetScore());

          
        }

        randomList.Clear();

        foreach (var item in destList)
        {
         
            if(Mathf.Abs( maxi - item.currentScore) <
                data.DistanceFromMaxToConsiderForEvent)
            {
                randomList.Add(item);
            }
        }

        if (randomList.Count == 0) return;

        last = randomList[Random.Range(0, randomList.Count)];

        if(destList == runtTimeGameEvents)
        runtTimeGameEvents.Remove(last);


        

        print($"{last.eventText} {last.currentScore}");

        TriggerEvent(last);

        isPlayerTurn = true;
    }



    public void DoEnv()
    {
        RunScoredEvent();
    }


    private void RunRandomEvent()
    {

       


       last = runtTimeGameEvents[Random.Range(0, runtTimeGameEvents.Count)];

        runtTimeGameEvents.Remove(last);
        TriggerEvent(last);
    }


    private void Update()
    {
        if (!isPlayerTurn)
        {
            DoEnv();
        }

        string statText = "";
         for(int i = 0; i < 6; ++i)
        {
            statText += $" {(Attributes)i} =  {AttributeData.values[i]}" ;
        }
        stats.text = statText;

    }
}
