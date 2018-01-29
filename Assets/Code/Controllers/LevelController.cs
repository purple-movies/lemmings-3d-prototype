using com.draconianmarshmallows.boilerplate.controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : BaseLevelController
{
    [Header("Prefabs")]
    [SerializeField] private GameObject agentPrefab;

    [Header("Positions")]
    [SerializeField] private Transform goalPoint;
    [SerializeField] private Transform spawnPoint;

    [Header("Other")]
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
        checkForLevelCompletion();
    }

    public override void startLevel()
    {
        base.startLevel();
        StartCoroutine(spawnAgent());
    }

    public void agentReachedGoal(AgentController agent)
    {
        activeAgents.Remove(agent);
        savedAgents.Add(agent);
        agent.active = false;
        checkForLevelCompletion();
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

    private void checkForLevelCompletion()
    {
        if (activeAgents.Count < 1)
        {
            masterController.onLevelCompleted(savedAgents.Count >= agentsToWinCount);
        }
    }

    private IEnumerator spawnAgent()
    {
        while (agentsSpawned < totalAgents)
        {
            agentsSpawned ++;
            GameObject agentGameObject = Instantiate(agentPrefab, spawnPoint.position, 
                Quaternion.identity);

            AgentController agent = agentGameObject.GetComponent<AgentController>();
            activeAgents.Add(agent);
            agent.initialize(this);
            agent.transform.SetParent(agentsParent);
            agent.setGoal(goalPoint);

            yield return new WaitForSeconds(3f);
        }
    }

    private void removeAgent(List<AgentController> activeAgents, AgentController agent)
    {
        activeAgents.Remove(agent);
        agent.destroy();
    }
}
