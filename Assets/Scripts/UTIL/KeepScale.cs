using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScale : MonoBehaviour
{


    Vector2 ParentScale = Vector2.one;

    public Transform Parent;
   
    private void Start()
    {
       
       
        ParentScale = Parent.localScale;
       
    }


    private void Update()
    {
        transform.localScale = ParentScale / Parent.localScale;
    }

    [ContextMenu("MAKE")]
    public void Do()
    {
      transform.position =   transform.GetChild(0).transform.position;
        transform.GetChild(0).transform.localPosition = Vector3.zero;
    }
}
