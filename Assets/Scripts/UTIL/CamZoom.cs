using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class CamZoom : MonoBehaviour
{

    public Volume postProcessVolume;



    public float MoveToTargetSpeed = 5;

    public float speed = 3;
    static  float CurrentSpeedCo = 3;

   public  static float target;
    public static float ZoomTarget;

    public static Transform targetFollow;


    private ChromaticAberration chromaticAberration;


    public static bool zooming => Mathf.Abs(ZoomTarget - Camera.main.orthographicSize) > .1f;


    private void Start()
    {
        postProcessVolume.profile.TryGet(out chromaticAberration);
    }

    
    public static void InitZoom(Transform follow )
    {
        CamZoom.ZoomTarget = 2.1f;
        targetFollow = follow;
        if (follow == null) return;
        CurrentSpeedCo = follow.position.magnitude ;
     
    }

    public static void ReZoomAndRecenter()
    {
        Camera.main.orthographicSize = 5;
        target = 4.8f;
        ZoomTarget = 5;
        Camera.main.transform.position = Vector3.forward * -10;
        targetFollow =null;

    }

    private Vector3 MyLerp(Vector3 a , Vector3 b ,  float t )
    {
        return a + (b - a).normalized * t;
    }


    //  public PostProcessVolume postProcessVolume;
    //  private MotionBlur motionBlur;


    // Update is called once per frame
    void FixedUpdate()
    {

        float zoomLeft = Mathf.Clamp01(Mathf.Abs(ZoomTarget - Camera.main.orthographicSize));

        if ( targetFollow != null && Vector2.Distance(targetFollow.position, Camera.main.transform.position)<.05f )
        {
            Camera.main.transform.position = targetFollow.position;
        }
        else
        {
            var targetPos = Vector3.forward * -10 + (targetFollow == null ? Vector3.zero : targetFollow.position);


            Camera.main.transform.position = MyLerp(Camera.main.transform.position, targetPos, Time.fixedDeltaTime * MoveToTargetSpeed * CurrentSpeedCo);
        }
        chromaticAberration.intensity.value = zoomLeft*.5f;
        target = Mathf.Lerp(target, ZoomTarget, Time.deltaTime*speed );
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, target, Time.fixedDeltaTime* speed  );
    }
}
