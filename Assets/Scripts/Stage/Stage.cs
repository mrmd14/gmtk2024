using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Stage parentStage;


  [HideInInspector] public  List<RefrenceToAgent> refrenceToAgents;


    private void Start()
    {
        for(int i =0;i< transform.childCount; ++i)
        {
            refrenceToAgents.Add(transform.GetChild(i).GetComponent<RefrenceToAgent>());
        }
    }
}
