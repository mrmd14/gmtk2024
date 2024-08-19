using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistEffect : MonoBehaviour
{
    public static LensDistEffect instance;

    private float leftTime = 5;

    public Volume postProcessVolume;

     LensDistortion lensDistortion;

    public float SinSpeed = 3;

  public  float co = 1;

    public AnimationCurve curve;

    private void Awake()
    {
        instance = this;

        postProcessVolume.profile.TryGet(out lensDistortion);
    }


    public void Do(Vector2 worldPos) {

        leftTime = 0;
        var center  = Camera.main.WorldToViewportPoint(worldPos);
        lensDistortion.center.value = center;
    }


    private void Update()
    {
        if (leftTime >= .5f)
        {
            lensDistortion.intensity.value = 0;
            return;
        }
       
        lensDistortion.intensity.value = Mathf.Sin(leftTime * SinSpeed) *co * curve.Evaluate(leftTime/.5f);
        leftTime += Time.deltaTime;

    }
}
