using com.draconianmarshmallows.boilerplate.controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : BaseLevelController
{
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private Transform goalPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int totalAgents;
    [SerializeField] private int agentsToWinCount;

    private int agentsSpawned;
    private Transform agentsParent;
    private List<AgentController> activeAgents = new List<AgentController>();
    private List<AgentController> savedAgents = new List<AgentController>();

    public void onAgentDestroyed(AgentController agent)
    {
        if (resetting) return;
        activeAgents.Remove(agent);
    }

    public override void startLevel()
    {
        base.startLevel();
        StartCoroutine(spawnAgent());
    }

    protected override void reset()
    {
        base.reset();
        agentsSpawned = 0;
        agentsParent = agentsParent == null ? 
            new GameObject("Agents").transform : agentsParent;

        foreach (var agent in activeAgents)
            removeAgent(activeAgents, agent);

        foreach (var agent in savedAgents)
            removeAgent(savedAgents, agent);

        endResetting();
    }

    private IEnumerator spawnAgent()
    {
        while (agentsSpawned < totalAgents)
        {
            Debug.Log("spawning agent !");
            agentsSpawned ++;
            GameObject agentGameObject = Instantiate(agentPrefab, spawnPoint.position, 
                Quaternion.identity);

            AgentController agent = agentGameObject.GetComponent<AgentController>();
            activeAgents.Add(agent);
            agent.transform.SetParent(agentsParent);
            agent.destination = goalPoint.position;

            yield return new WaitForSeconds(3f);
        }
    }

    private void removeAgent(List<AgentController> activeAgents, AgentController agent)
    {
        activeAgents.Remove(agent);
        agent.destroy();
    }
}
