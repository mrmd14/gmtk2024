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

    private void Awake()
    {
        foreach(var item in list)
        {
            
            map[item.agent] = item.instance;
        }
    }

}
