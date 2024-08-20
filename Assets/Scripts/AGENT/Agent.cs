using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Agent : MonoBehaviour
{


    public Agent linkedAgent;


    public enum Agents
    {
        SUN =0,
        Earth = 1,
        Moon = 2,
        WEST = 3,
        EAST = 4,
        Pole = 5,
        HUMAN = 6,
        DOG = 7,
        ROBOT = 8,
        LAB = 9,
        TREE = 10,
        livestock = 11,
        Temple   = 12,
        HUMAN_BRAIN = 13,
        HUMAN_HEART = 14,
        HUMAN_eye = 15,
        HUMAN_ear = 16,
        leaf = 17,
        fruit = 18,
        livestock_brain=19,
        livestock_HEART=20,
        wool=21,
        cpu=22,
        emotionCircuit=23,
        Alien = 24,

    }

    public enum State
    {
        small  = 0,
        mid = 1,
        big = 2,
    }

    public State currentState = State.mid;

    public AttributeActionSequence makeBig;
    public AttributeActionSequence makeSmall;




    public RefrenceToAgent UI;





    public void Init()
    {
        currentState = State.mid;
    }

    public void PostEffect(State newState)
    {
        if(newState != currentState)
        {
            if(UI != null)
            LensDistEffect.instance.Do(UI.transform.position);
            else LensDistEffect.instance.Do(Vector2.zero);
        }
    }

    public void GoMid(bool val)
    {
       
        if (!GamePlayManager.isPlayerTurn) return;
        if (linkedAgent != null && val) linkedAgent.GoMid(false);
        PostEffect(State.mid);



        UI.SetScale(Vector3.one);
        currentState = State.mid;


        GamePlayManager.EndPlayerTurn();


    

    }


    public  void GoBig(bool val)
    {

       
        if(currentState == State.big)
        {
            return;
        }
        if(currentState == State.small)
        {
            GoMid(val);
            return;
        }

        if (linkedAgent != null && val) linkedAgent.GoBig(false);

        PostEffect(State.big);

       if(UI != null)
        UI.SetScale( Vector3.one * 1.3f);
        if (!GamePlayManager.isPlayerTurn) return;
        makeBig.DoOnBase();
        currentState = State.big;
        GamePlayManager.EndPlayerTurn();

       



    }

    public void GoSmall(bool val)
    {
        if (currentState == State.small)
        {
            return;
        }

        if (currentState == State.big)
        {
            GoMid(val);
            return;
        }
        if (linkedAgent != null && val) linkedAgent.GoSmall(false);
        PostEffect(State.small);
        UI.SetScale(Vector3.one * .7f);
        if (!GamePlayManager.isPlayerTurn) return;
        makeSmall.DoOnBase();
        currentState = State.small;
        GamePlayManager.EndPlayerTurn();


        if (linkedAgent != null && val) linkedAgent.GoSmall(false);




    }


    private void Update()
    {
        print($"{gameObject} {currentState}");
    }





}
