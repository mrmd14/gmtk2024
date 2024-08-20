using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Stage> Stages;

    public Stage current;


  public   float ScrollVal = 0;

    public static StageManager instance;




    public void Init()
    {
        foreach(var item in Stages)
        {
            foreach(var agent in item.refrenceToAgents)
            {
                agent.agent.Init();
            }
        }

        TurnOnStage(Stages[0],null,true);
    }


    private void Awake()
    {
        instance = this;

        foreach(var item in Stages)
        {
            item.init();
        }
    }

    public void TurnOnStage(Stage stage, Transform targetFollow, bool Force = false ) {

        if (stage == null) return;


        StartCoroutine(
                ZoomAndWait(stage, Force, targetFollow));
    }


    IEnumerator ZoomAndWait(Stage stage,bool force , Transform targetFollow) {
        if (!force)
        {
            CamZoom.InitZoom(targetFollow);
            while (CamZoom.zooming) yield return null;
        }
        CamZoom.ReZoomAndRecenter();
        current = stage;
        foreach (var item in Stages)
        {
            item.gameObject.SetActive(false);
        }
        stage.gameObject.SetActive(true);

        Camera.main.orthographicSize = 5;
        CamZoom.ZoomTarget = 5;


    }



    private void Update()
    {
     

        if(Input.mouseScrollDelta.y == 0)
        {
            ScrollVal = 0;
            return;
        }
        ScrollVal += Input.mouseScrollDelta.y;



        if (current == null || current.parentStage == null)
        {

            if (ScrollVal <= -1)
            {
                CamZoom.target = 10f;
            }

            return;

        }


        if (ScrollVal <= -1)
        {
            TurnOnStage(current.parentStage, null, true);
        }

    
    }


}
