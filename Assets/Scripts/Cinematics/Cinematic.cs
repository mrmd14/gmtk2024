using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public static Cinematic instance;


    

    public static bool anyCinematic = false;
    public static bool canRunText = false;

    private static  bool firstTime = true;

    public RefrenceToAgent earth;
    public RefrenceToAgent west ;

    public CityLogo westLogo;

    public Transform tajob;

    public Transform uiTop;
    public Transform uiBot;


    private void Awake()
    {
        instance = this;

    }

    public static void Init()
    {
        anyCinematic = false;
        canRunText = true;
        instance.StartCoroutine(initCombat());

        if (firstTime)
        {
            instance.StartCoroutine(FirstTimePlayed());

            firstTime = false;
        }
    }


    static IEnumerator initCombat()
    {

        
        while (instance.uiTop.transform.localPosition.y > 6.02f)
        {

            instance.uiTop.transform.position -= Time.deltaTime * Vector3.up * 15f;
            yield return null;
        }
        while (instance.uiBot.transform.localPosition.y < -4.59f)
        {

            instance.uiBot.transform.position += Time.deltaTime * Vector3.up * 15f;
            yield return null;
        }
    }



    static IEnumerator FirstTimePlayed()
    {

        anyCinematic = true;
        canRunText = false;


        yield return new WaitForSeconds(1.5f);


        instance.earth.TurnOnMyStage();

        while (CamZoom.zooming) yield return null;
        yield return new WaitForSeconds(.5f);
        instance.west.TurnOnMyStage();

        yield return new WaitForSeconds(1);

        instance.westLogo.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        while (instance.tajob.transform.localPosition.y < 1.5f)
        {

            instance.tajob.transform.position += Time.deltaTime * Vector3.up * 15f;
            yield return null;
        }
        canRunText = true;

        anyCinematic = false;

    }
}
