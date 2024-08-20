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



    public Sprite icon;

    private void Awake()
    {
        if (agent == null) return;


        if(icon == null)
        {
            icon = spriteRenderer.sprite;
        }
        
        text.text = agent.name;
        agent.UI = this;
        
    }


    public void init()
    {
        
        agent.UI = this;
        hovering = false;
        SetScale (Vector3.one);
        
    }

    private void OnEnable()
    {
        
        hovering = false;
    }


    private void OnMouseEnter()
    {

        SetCursor.SetCurserZoom();

        hovering = true;

        StageManager.instance.ScrollVal = 0;



    }

    private void OnMouseExit()
    {
        SetCursor.SetCurserNormal();

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

        SetCursor.SetCurserZoomIn();

        agent.GoBig();
    }

    private void GoSmall()
    {
        if (agent.currentState == Agent.State.small) return;

        SetCursor.SetCurserZoomOut();

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

    public void TurnOnMyStage()
    {
        StageManager.instance.TurnOnStage(stage, transform);
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

        if ( GamePlayManager.CanZoom &&  stage != null && StageManager.instance.ScrollVal >= 1)
        {
            TurnOnMyStage();
        }

        if (!GamePlayManager.isPlayerTurn) return;
        if (!GamePlayManager.CanScale) return;
        if (Input.GetMouseButtonDown(0))
        {
            GoBig();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            GoSmall();
        }

     



    }




}
