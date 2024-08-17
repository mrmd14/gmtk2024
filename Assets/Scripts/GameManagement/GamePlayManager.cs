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

    public Dictionary<GameEvent,bool> triggered = new Dictionary<GameEvent, bool>();


    private void Awake()
    {
        instance = this;
    }

    public void TriggerEvent(GameEvent gameEvent)
    {
        triggered[gameEvent] = true;
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

    private void RunScoredEvent(bool forceMain )
    {



        // float Set Score and find max 
        float maxi = -1000;
        var destList = last.ReadFromForNext.Count == 0 ? runtTimeGameEvents : last.ReadFromForNext;
        if (forceMain) destList = runtTimeGameEvents;




        foreach (var item in destList)
        {
            
           maxi  = Mathf.Max(maxi, item.SetScore());

          
        }

        randomList.Clear();
        print(maxi);
        foreach (var item in destList)
        {
            if (item.currentScore == -1000) continue;

            if(Mathf.Abs( maxi - item.currentScore) <
                data.DistanceFromMaxToConsiderForEvent)
            {
                randomList.Add(item);
            }
        }
   
        if (randomList.Count == 0)
        {
            if(destList != runtTimeGameEvents)
            {
                // force main 
                RunScoredEvent(true);
                return;
            }
        }

        last = randomList[Random.Range(0, randomList.Count)];

        if(destList == runtTimeGameEvents)
        runtTimeGameEvents.Remove(last);


        

        print($"{last.eventText} {last.currentScore}");

        TriggerEvent(last);

        isPlayerTurn = true;
    }



    public void DoEnv()
    {
        RunScoredEvent(false);
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
        SetValues();
        string statText = "";
        foreach(var item in AttributeInitValMap)
        {
            statText += $"  {item.Key} = {AttributeData.values[(int)item.Key]} ";
        }
        stats.text = statText;

     

    }




    private void SetValues()
    {
        for (int i = 0;i<AttributeData.CurrentBaseValue.Length;++i)
        {
            AttributeData.values[i] = AttributeData.CurrentBaseValue[i];
        }


        foreach(var item in data.HidenToMainAttribute)
        {
            item.Set();
        }
    }


}
