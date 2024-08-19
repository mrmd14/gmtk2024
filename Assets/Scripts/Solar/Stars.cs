using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stars : MonoBehaviour
{

    public List<SpriteRenderer> stars;
    List<float> starsParalaxCo = new List<float>();

    public  Vector2 RangeSize = new Vector2(.3f, 1f);

    public List<Vector2> lastPos = new List<Vector2>();

   public static Vector2 MousePos=> Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 lastMousePos = Vector2.zero;

    private void OnEnable()
    {
        SetPos();
      

     
    }

    private void SetPos()
    {


        while (starsParalaxCo.Count < 100)
        {

            starsParalaxCo.Add(Random.Range(0f, .35f));
        }

        for (int i = 0; i < stars.Count; i++)
        {
           
            if(i< lastPos.Count && i<stars.Count)
            lastPos[i] = stars[i].transform.position;

        }
        lastMousePos = MousePos;
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
        var delta = MousePos - lastMousePos;
        delta = delta.normalized;
        lastMousePos = MousePos;
        if (delta.magnitude > 1)
        {
            SetPos();
            return;
        }
        int i = 0;
        foreach(var item in stars)
        {
            item.transform.position +=  (Vector3)delta * starsParalaxCo[i++] * Time.fixedDeltaTime;
        }
    }
}
