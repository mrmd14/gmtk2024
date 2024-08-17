using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{


    public AttributeActionSequence makeBig;
    public AttributeActionSequence makeSmall;



    [Header("DONT TOUCH")]

    public AddBtnToBox makeBigBTN;
    public AddBtnToBox makeSmallBTN;



    private void Start()
    {
        makeBigBTN.action = GoBig;
        makeSmallBTN.action = GoSmall;
    }

    private void GoBig()
    {
        if (!GamePlayManager.isPlayerTurn) return;
        makeBig.Do();

        GamePlayManager.isPlayerTurn = false;



    }

    private void GoSmall()
    {
        if (!GamePlayManager.isPlayerTurn) return;
        makeSmall.Do();

        GamePlayManager.isPlayerTurn = false;




    }





}
