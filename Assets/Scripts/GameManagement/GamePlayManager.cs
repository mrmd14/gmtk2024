using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GamePlayManager : MonoBehaviour
{

    public GameEvents gameEvents;
    public GameEvents backUpEvent;

    public static bool isPlayerTurn = false;

    private List<GameEvent> runtTimeGameEvents = new List<GameEvent>();
    private List<GameEvent> runtTimeBackUp = new List<GameEvent>();





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

    public void TriggerEvent(GameEvent gameEvent, bool skiped )
    {
        triggered[gameEvent] = true;
        LastEventText.text = (skiped? "" :  gameEvent.ResolveText)+   gameEvent.eventText;
        gameEvent.ResultSequence.DoOnBase();

        foreach(var item in gameEvent.deBuffs)
        debuffManager.all.Add(item);

        foreach (var item in gameEvent.deBuffsToResolve)
            debuffManager.all.Remove(item);


        foreach(var item in gameEvent.RemoveAgent)
        {
            print(item);
            AgentManager.TurnOff(item);
        }

        isPlayerTurn = true;
    }


    private void Start()
    {
        Init();
    }

    private void Init()
    {


        debuffManager.Init();

        triggered.Clear();

        StageManager.instance.Init();

        // set map 

        AttributeInitValMap.Clear();

        foreach (var item in  data.attributeInitVals)
        {
            
            AttributeInitValMap[item.attribute] = item;
        }
        foreach(var item in AttributeInitValMap)
        {
            AttributeData.CurrentBaseValue[item.Key] = item.Value.statsInitalVal;
            AttributeData.values[item.Key] = item.Value.statsInitalVal;
        }


        runtTimeGameEvents.Clear();
        runtTimeBackUp.Clear();



        foreach (var item in gameEvents.mainPool)
        {
            runtTimeGameEvents.Add(item);
        }

        foreach (var item in backUpEvent.mainPool)
        {
            runtTimeBackUp.Add(item);
        }


        // init agent UI 

        foreach (var item in stages)
        {
            foreach(var agentUI in item.refrenceToAgents)
            {
               
                agentUI.init();
            }
        }

        RunRandomEvent();
 

    }

    private bool  RunScoredEvent(bool skiped , bool runBackUp   )
    {


        if (last == null) return false;

       
        isPlayerTurn = true;

        // float Set Score and find max 
        float maxi = -1000;
        var destList = runBackUp? runtTimeBackUp  :last.ReadFromForNext ;
       




        foreach (var item in destList)
        {
         
           maxi  = Mathf.Max(maxi, item.SetScore());
         

        }

        randomList.Clear();
     
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


            

            return RunScoredEvent(false, true);
        }


        var lastLast = last;

        last = randomList[Random.Range(0, randomList.Count)];


        

        if(destList == runtTimeGameEvents)
        runtTimeGameEvents.Remove(last);
        runtTimeBackUp.Remove(last);


        
        
        // can skip 
        if(RunScoredEvent(true, false))
        {

            triggered[lastLast] = true ;
            return true;
        }
        TriggerEvent(last, skiped);

        return true;

      
    }



    public void DoEnv()
    {
        RunScoredEvent(false, false);
    }


    private void RunRandomEvent()
    {


        randomList.Clear();

        foreach (var item in runtTimeGameEvents)
        {
            item.SetScore();
            if (item.currentScore == -1000) continue;

            randomList.Add(item);
        }
        if (randomList.Count == 0) return;


        last = randomList[Random.Range(0, randomList.Count)];

        runtTimeGameEvents.Remove(last);
        TriggerEvent(last, false);
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
            statText += $"  {item.Key} = {AttributeData.values[item.Key]} ";
        }
        stats.text = statText;

     

    }


    public static void EndPlayerTurn()
    {
        instance.SetValues();
        debuffManager.RunDebuffs();
        GamePlayManager.isPlayerTurn = false;

    }




    private void SetValues()
    {
        foreach(var item in AttributeData.CurrentBaseValue)
        {
            AttributeData.values[item.Key] = item.Value;
        }


        foreach(var item in data.HidenToMainAttribute)
        {
            item.Set();
        }
    }


}
