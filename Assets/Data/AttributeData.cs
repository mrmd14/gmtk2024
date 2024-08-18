using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeData : MonoBehaviour
{

   

 


    public static float[] values = new float[maxLen];
    public static float[] CurrentBaseValue = new float[maxLen];


    private const int maxLen = 11;

   

  

}
public enum Attributes
{
    Population = 1,
    Welfare = 2,
    Happiness = 3,
    Spirituality = 4,
    Tech = 5,
    //hidden ones
    Temp = 6,
    Env = 7,
    Int = 8,
    Security = 9,
    Peace = 10,
    Resources = 11,

}







