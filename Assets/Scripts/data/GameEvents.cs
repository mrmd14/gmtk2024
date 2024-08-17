using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvents", menuName = "GAME/GameEvents")]

public class GameEvents : ScriptableObject
{
    public List<GameEvent> mainPool;
}
