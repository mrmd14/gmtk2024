using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeData : MonoBehaviour
{

   

    public static float EnvironmentalCondition=> values[0];
    public static float Satisfaction => values[1];
    public static float Economy => values[2];
    public static float Health => values[3];
    public static float Spirituality => values[4];
    public static float Science => values[5];


 


    public static float[] values = new float[6];

   

  

}
public enum Attributes
{
    EnvironmentalCondition=0,
    Satisfaction=1,
    Economy=2,
    Health = 3,
    Spirituality  = 4,
    Science = 5,

}

