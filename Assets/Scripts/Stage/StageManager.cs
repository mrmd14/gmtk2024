using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Stage> Stages;

    public Stage current;


  public   float ScrollVal = 0;

    public static StageManager instance;




    public void Init()
    {
        TurnOnStage(Stages[0]);
    }


    private void Awake()
    {
        instance = this;
    }

    public void TurnOnStage(Stage stage) {

        if (stage == null) return;
        current = stage;
        foreach(var item in Stages)
        {
            item.gameObject.SetActive(false);
        }
        stage.gameObject.SetActive(true);
    }



    private void Update()
    {
     

        if(Input.mouseScrollDelta.y == 0)
        {
            ScrollVal = 0;
            return;
        }
        ScrollVal += Input.mouseScrollDelta.y;

        if (current == null || current.parentStage == null) return;

      
        if (ScrollVal <= -1)
        {
            TurnOnStage(current.parentStage);
        }

    
    }


}
