using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public static SetCursor instance;

    public Texture2D normal;
    public Texture2D zoom;
    public Texture2D zoomIn;
    public Texture2D zoomOut;


    private void Awake()
    {
        instance = this;

        SetCurserNormal();
    }

    

   public static void SetCurserNormal()
    {
        Cursor.SetCursor(instance.normal,Vector3.zero, CursorMode.ForceSoftware);
    }

    public static void SetCurserZoom()
    {
        Cursor.SetCursor(instance.zoom, Vector3.zero, CursorMode.ForceSoftware);
    }

    public static void SetCurserZoomIn()
    {
        Cursor.SetCursor(instance.zoomIn, Vector3.zero, CursorMode.ForceSoftware);
       instance.StartCoroutine(
              setBack());
    }

    public static void SetCurserZoomOut()
    {
        Cursor.SetCursor(instance.zoomOut, Vector3.zero, CursorMode.ForceSoftware);
        instance.StartCoroutine(
               setBack());
    }

    private static IEnumerator setBack()
    {
        yield return new WaitForSeconds(.5f);
        SetCurserZoom();
    }

    
}
