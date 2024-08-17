using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Agent : MonoBehaviour
{




    public enum State
    {
        small  = 0,
        mid = 1,
        big = 2,
    }

    public State currentState = State.mid;

    public AttributeActionSequence makeBig;
    public AttributeActionSequence makeSmall;




    




    public void Init()
    {
        currentState = State.mid;
    }


    

    public  void GoBig()
    {
        if(currentState == State.big)
        {
            return;
        }
        if (!GamePlayManager.isPlayerTurn) return;
        makeBig.DoOnBase();

        GamePlayManager.isPlayerTurn = false;



    }

    public void GoSmall()
    {
        if (currentState == State.small)
        {
            return;
        }
        if (!GamePlayManager.isPlayerTurn) return;
        makeSmall.DoOnBase();

        GamePlayManager.isPlayerTurn = false;




    }





}
