using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public TypewriterText LastEventText;



    public GameEvent last;



    public static Dictionary<Attributes, AttributeInitVal> AttributeInitValMap = new Dictionary<Attributes, AttributeInitVal>();


  [SerializeField]  List<Stage> stages;

    public Dictionary<GameEvent,bool> triggered = new Dictionary<GameEvent, bool>();



    public static bool inGamePlay = false;

    public AddBtnToBox playBtn;


    public Agent sun;

    public static bool isSunBig => instance.sun.currentState == Agent.State.big;



    public static bool CanZoom => inGamePlay && !Cinematic.anyCinematic  ;
    public static bool CanScale => inGamePlay && !Cinematic.anyCinematic&& !TypewriterText.isTyping && !forceFeedBack;


   static bool forceFeedBack = false;

    public List<TurnOnIfTurnOff> switchs;

    private void Awake()
    {
        playBtn.action = Init;
        instance = this;
    }

    public void TriggerEvent(GameEvent gameEvent, bool skiped )
    {

        if (gameEvent.EndGame)
        {
            END.End();
        }

        if (gameEvent.ForceFeedBack)
        {
            forceFeedBack = gameEvent.ForceFeedBack;
        }

        else
        {
            HandHeldManager.Set(gameEvent);
        }


        triggered[gameEvent] = true;
        LastEventText.fullText = (skiped? "" :  gameEvent.ResolveText)+   gameEvent.eventText;
        LastEventText.Init();
        gameEvent.ResultSequence.DoOnBase();

        foreach(var item in gameEvent.deBuffs)
        debuffManager.all.Add(item);

        foreach (var item in gameEvent.deBuffsToResolve)
            debuffManager.all.Remove(item);


        foreach(var item in gameEvent.RemoveAgent)
        {
            print(item);
            AgentManager.SetVal(item, false);
        }

        isPlayerTurn = true;
    }


  

    public  void Init()
    {


        foreach(var item in switchs){
            item.Init();
        }

        forceFeedBack = false;
        playBtn.transform.parent.gameObject.SetActive(false);
        inGamePlay = true;
        debuffManager.Init();

        AgentManager.Init();

        END.instance.Clear();

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


        Cinematic.Init();

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
       
        if (randomList.Count == 0 )
        {


            if (runBackUp ||   last.ReadFromForNext.Count != 0 ) return false;

            return RunScoredEvent(false, true);
        }


        var lastLast = last;

        last = randomList[Random.Range(0, randomList.Count)];


        

        if(destList == runtTimeGameEvents)
        runtTimeGameEvents.Remove(last);
        runtTimeBackUp.Remove(last);


        
        
      
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
        if (forceFeedBack && Input.anyKeyDown)
        {
            forceFeedBack = false;
            return;
        }
        if (!inGamePlay) return;
        if (!isPlayerTurn)
        {
            DoEnv();
        }
        SetValues();
       
       
       

     

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
