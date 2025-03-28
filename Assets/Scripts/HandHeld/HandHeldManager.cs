using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class HandHeldManager : MonoBehaviour
{

    public Transform startPos;

    public Vector3 deltaPosHandHeld = Vector2.up * 3;

    public List<HandHeldItem> handhelds;

    public static HandHeldManager instance;

  static  List<Agent.Agents> managed = new List<Agent.Agents>();

    public Stage west;
    public Stage east;

    private void Awake()
    {
        instance = this;
    }
    public static void Set(GameEvent gameEvent)
    {

        managed.Clear();
        for (int i = 0; i < 4; ++i)
        {
            instance.handhelds[i].transform.position = instance.startPos.position  + instance.deltaPosHandHeld * i;
            instance.handhelds[i].gameObject.SetActive(false);
        }

        int k = 0;

        foreach(var next in gameEvent.ReadFromForNext)
        {
            foreach(var cond in next.conditionSequence.list)
            {
                foreach(var agentKey in cond.AgentInSize)
                {
                    if (managed.Contains(agentKey.agent)) continue;

                    Agent agent = null;
                    if (!AgentManager.map.TryGetValue(agentKey.agent, out agent))
                        continue;

                    instance.handhelds[k].title.text = agent.gameObject.name;
                    
                    if(agent.UI != null)
                    instance.handhelds[k].vfx.sprite = agent.UI.icon;

                    instance.handhelds[k].gameObject.SetActive(true);
                    managed.Add(agentKey.agent);
                    Stage stage = null;
                    if (agentKey.agent == Agent.Agents.HUMAN && !TurnOnIfTurnOff.isWestActive) stage = instance.east;
                    else if (agentKey.agent == Agent.Agents.HUMAN && !TurnOnIfTurnOff.isEastActive) stage = instance.west;

                    else  if (!AgentManager.parentStage.TryGetValue(agent, out stage))
                    {
                        print(agentKey.agent);
                        instance.handhelds[k].btn.action = null;
                        k++;
                        continue;
                    }



                    stage = ManageEastWest(stage);

                    instance.handhelds[k].btn.action  = ()=> StageManager.instance.TurnOnStage(stage, null, Force:true);
                    k++;
                }
               
            }
        }
    }

    public static Stage ManageEastWest(Stage stage)
    {
      
        if (stage == instance.west && !TurnOnIfTurnOff.isWestActive)
        {
            return  instance.east;
        }
        if (stage == instance.east && !TurnOnIfTurnOff.isEastActive)
        {
             return instance.west;
        }

        return stage;

    }
}
