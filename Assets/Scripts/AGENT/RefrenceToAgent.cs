using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefrenceToAgent : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public Agent agent;

    public Stage stage;


    public TextMeshPro text;


    bool hovering = false;

    Vector2 orgianalScale = Vector2.one;




    [HideInInspector] Color targetColor= Color.cyan;

    public Color NotHoverColor = Color.gray;
    public Color HoverColor = Color.gray;



    public GameObject shadow;

    


      float  hoverScale = 3.59f;
     float NoHoverScale = 2.06f;



    public List<RefrenceToAgentLink> links;




    private void Awake()
    {
        if (agent == null) return;

        
        text.text = agent.name;
        agent.UI = this;
        
    }


    public void init()
    {
        hovering = false;
        SetScale (Vector3.one);
        
    }

    private void OnEnable()
    {
        
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


    public void SetScale(Vector3 newVal)
    {
        transform.localScale = newVal;

        foreach(var item in links)
        {
            item.transform.localScale = newVal;
        }
    }

    private void GoBig()
    {
        if (agent.currentState == Agent.State.big) return;
        SetScale( Vector3.one * 1.3f);
        


        agent.GoBig();
    }

    private void GoSmall()
    {
        if (agent.currentState == Agent.State.small) return;

    
        agent.GoSmall();
    }


    private void SetForSprite(SpriteRenderer sp, bool val)
    {
        sp.material.SetColor("_OutlineColor", val ? HoverColor: NotHoverColor  );
        sp.material.SetFloat("_OutlineThickness", (val ? hoverScale : NoHoverScale));
    }


    private bool anyHover()
    {
       var res =  hovering;

        foreach (var item in links)
        {
            if (item.hovering) res = true;
        }
        return res;

    }
    private void Update()
    {


        bool anyHovering = anyHover();

        if(shadow != null)
        {
            shadow.gameObject.SetActive(anyHovering);
        }

        SetForSprite(spriteRenderer, anyHovering);



        foreach (var item in links)
        {
            SetForSprite(item.vfx, anyHovering);
        }

        if (!anyHovering || agent == null) return;

        if (stage != null && StageManager.instance.ScrollVal >= 1)
        {
            StageManager.instance.TurnOnStage(stage, transform);
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
