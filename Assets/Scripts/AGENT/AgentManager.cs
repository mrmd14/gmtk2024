using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class AgentManager : MonoBehaviour
{
    [System.Serializable]
    public class AgentInstance
    {
        public Agent instance;
        public Agent.Agents agent;
    }

    public List<AgentInstance> list;

    List<Agent> agents;



    public static Dictionary<Agent.Agents, Agent> map = new Dictionary<Agent.Agents, Agent>();
    public static Dictionary<Agent, Stage> parentStage = new Dictionary<Agent, Stage>();






    public static void SetVal(Agent.Agents agentKey, bool val  )
    {
        Agent agent = null;
        if (!map.TryGetValue(agentKey, out agent)) return;
       
        if (agent.UI == null) return;


        if (!val && agentKey == Agent.Agents.WEST) TurnOnIfTurnOff.isWestActive = false;
        if (!val && agentKey == Agent.Agents.EAST) TurnOnIfTurnOff.isEastActive = false;

        foreach (var item in agent.UI.links)
        {
            item.gameObject.SetActive(val);

        }
        agent.UI.gameObject.SetActive(val);
    }

    public static void Init()
    {
        
        for (int i = 0; i < 25; ++i)
        {
            SetVal((Agent.Agents)i, true);
        }
    }

    private void Awake()
    {
        foreach(var item in list)
        {
            
            map[item.agent] = item.instance;
        }
    }

}
