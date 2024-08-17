using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeData : MonoBehaviour
{

   

 


    public static float[] values = new float[maxLen];
    public static float[] CurrentBaseValue = new float[maxLen];


    private const int maxLen = 8;

   

  

}
public enum Attributes
{
    EnvironmentalCondition=0,
    Satisfaction=1,
    Economy=2,
    Health = 3,
    Spirituality  = 4,
    Science = 5,
    intelegence = 6,
    temperature = 7,

}







