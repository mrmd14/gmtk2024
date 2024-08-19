using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Stage parentStage;


  [HideInInspector] public  List<RefrenceToAgent> refrenceToAgents = new List<RefrenceToAgent>();

   public  bool keepOn = false;
    private void Awake()
    {
        for(int i =0;i< transform.childCount; i++)
        {
            var newAgent = transform.GetChild(i).GetComponent<RefrenceToAgent>();

          
            if(newAgent != null)
            refrenceToAgents.Add(newAgent);
        }

        gameObject.SetActive(keepOn);
    }
}
