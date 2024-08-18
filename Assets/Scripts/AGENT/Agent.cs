using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Agent : MonoBehaviour
{





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

    public void GoMid()
    {

        if (!GamePlayManager.isPlayerTurn) return;
        UI.transform.localScale = Vector3.one;
        currentState = State.mid;


        GamePlayManager.isPlayerTurn = false;
    }
    

    public  void GoBig()
    {
        if(currentState == State.big)
        {
            return;
        }
        if(currentState == State.small)
        {
            GoMid();
            return;
        }
        print("why hee");
        UI.transform.localScale = Vector3.one * 1.3f;
        if (!GamePlayManager.isPlayerTurn) return;
        makeBig.DoOnBase();
        currentState = State.big;
        GamePlayManager.isPlayerTurn = false;



    }

    public void GoSmall()
    {
        if (currentState == State.small)
        {
            return;
        }

        if (currentState == State.big)
        {
            GoMid();
            return;
        }

        UI.transform.localScale = Vector3.one * .7f;
        if (!GamePlayManager.isPlayerTurn) return;
        makeSmall.DoOnBase();
        currentState = State.small;
        GamePlayManager.isPlayerTurn = false;




    }





}
