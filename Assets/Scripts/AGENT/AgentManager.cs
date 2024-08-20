using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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




    public static void SetVal(Agent.Agents agentKey, bool val  )
    {
        Agent agent = null;
        if (!map.TryGetValue(agentKey, out agent)) return;
        print(agent.UI);
        if (agent.UI == null) return; 
        foreach (var item in agent.UI.links)
        {
            item.gameObject.SetActive(val);

        }
        agent.UI.gameObject.SetActive(val);
    }

    public static void Init()
    {
        for(int i = 0; i < 24; ++i)
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
