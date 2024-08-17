using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

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



    public static Dictionary<Attributes, AttributeInitVal> AttributeInitValMap = new Dictionary<Attributes, AttributeInitVal>();


  [SerializeField]  List<Stage> stages;


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


        StageManager.instance.Init();

        // set map 

        AttributeInitValMap.Clear();

        foreach (var item in  data.attributeInitVals)
        {
            
            AttributeInitValMap[item.attribute] = item;
        }
        foreach(var item in AttributeInitValMap)
        {
            AttributeData.values[(int)item.Key] = item.Value.statsInitalVal;
        }


        runtTimeGameEvents.Clear();
        foreach (var item in gameEvents.mainPool)
        {
            runtTimeGameEvents.Add(item);
        }


        // init agent UI 

        foreach(var item in stages)
        {
            foreach(var agentUI in item.refrenceToAgents)
            {
                agentUI.init();
            }
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
        print(maxi);
        foreach (var item in destList)
        {
            print(Mathf.Abs(maxi - item.currentScore));
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
         for(int i = 0; i < AttributeInitValMap.Count; ++i)
        {
            statText += $" {(Attributes)i} =  {AttributeData.values[i]}" ;
        }
        stats.text = statText;

    }
}
