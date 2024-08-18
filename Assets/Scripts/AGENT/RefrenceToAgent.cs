using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RefrenceToAgent : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public Agent agent;

    public Stage stage;


    public TextMeshPro text;



    bool hovering = false;

    Vector2 orgianalScale = Vector2.one;
   

    

    private void Awake()
    {
        if (agent == null) return;

        agent.UI = this;
        
    }


    public void init()
    {
        hovering = false;
        transform.localScale = Vector3.one;
        
    }

    private void OnEnable()
    {
        text.text = gameObject.name;
        hovering = false;
    }


    private void OnMouseEnter()
    {
        hovering = true;

        StageManager.instance.ScrollVal = 0;



    }

    private void OnMouseExit()
    {
        hovering = false;

    }

    private void GoBig()
    {
        if (agent.currentState == Agent.State.big) return;
        transform.localScale = Vector3.one * 1.1f;
        agent.GoBig();
    }

    private void GoSmall()
    {
        if (agent.currentState == Agent.State.small) return;

        transform.localScale = Vector3.one * .9f;
        agent.GoSmall();
    }

    private void Update()
    {
        if (!hovering || agent == null) return;

        if (stage != null && StageManager.instance.ScrollVal >= 1)
        {
            StageManager.instance.TurnOnStage(stage);
        }

        if (!GamePlayManager.isPlayerTurn) return;
        if (Input.GetMouseButtonDown(1))
        {
            GoBig();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            GoSmall();
        }
       
       
    }




}
