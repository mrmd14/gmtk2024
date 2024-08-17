using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GeneralGameData", menuName = "GAME/GeneralGameData")]

[System.Serializable]
public class AttributeInitVal
{
    public Attributes attribute = 0;
    public float statsInitalVal = 4;
    public float MinVal = 4;
    public float MaxVal = 4;
}

public class GeneralGameData : ScriptableObject
{
    public float DistanceFromMaxToConsiderForEvent = 3;

    public List<AttributeInitVal> attributeInitVals;

  
}
