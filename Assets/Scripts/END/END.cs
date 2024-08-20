using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class END : MonoBehaviour
{

    public static END instance;
    public CityLogo endLogo;

    public AddBtnToBox again;

    private void Awake()
    {
        instance = this;
     
    }

    private void Start()
    {
        again.action = GamePlayManager.instance.Init; 
    }

    public void Clear()
    {
        endLogo.gameObject.SetActive(false);
    }

    public static void End()
    {
        GamePlayManager.inGamePlay = false;

       instance.endLogo.transform.position = Vector3.left * 10;
        instance.endLogo.gameObject.SetActive(true);
    }
}
