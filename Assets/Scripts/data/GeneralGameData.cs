using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GeneralGameData", menuName = "GAME/GeneralGameData")]

public class GeneralGameData : ScriptableObject
{
    public float DistanceFromMaxToConsiderForEvent = 3;

    public float statsInitalVal = 4;
}
