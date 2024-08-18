using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Stars : MonoBehaviour
{

    public List<SpriteRenderer> stars;

    public  Vector2 RangeSize = new Vector2(.3f, 1f);

    public List<Vector2> lastPos = new List<Vector2>(100);


    Vector2 lastCamPos = Vector2.zero;

    private void OnEnable()
    {
        SetPos();
    }

    private void SetPos()
    {


        
        for (int i = 0; i < stars.Count; i++)
        {
           
            if(i< lastPos.Count && i<stars.Count)
            lastPos[i] = stars[i].transform.position;
        }
        lastCamPos = Camera.main.transform.position;
    }


    private void Awake()
    {
        foreach(var item in stars)
        {
            item.transform.position =  Vector2.right *  Random.Range(-10,10) + Vector2.up * Random.Range(-10, 10);

            item.transform.localScale = Vector3.one * Random.Range(RangeSize.x, RangeSize.y);
        }
    }

    private void FixedUpdate()
    {
        var delta = (Vector2)Camera.main.transform.position - lastCamPos;
        lastCamPos = (Vector2)Camera.main.transform.position;
        if (delta.magnitude > 1)
        {
            SetPos();
            return;
        }
        foreach(var item in stars)
        {
            item.transform.position +=  item.transform.localScale.x * delta.x * .1f * Vector3.right ;
        }
    }
}
