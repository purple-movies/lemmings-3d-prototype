using com.draconianmarshmallows.boilerplate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : DraconianBehaviour
{
    public Vector3 destination { set { navMeshAgent.destination = value; } }

    [SerializeField] private NavMeshAgent navMeshAgent;
    private LevelController levelController;

    public void initialize(LevelController levelController)
    {
        reset();
        this.levelController = levelController;
    }

    public void destroy()
    {
        levelController.onAgentDestroyed(this);
        Destroy(gameObject);
    }

    private void reset()
    {
        levelController = null;
    }
}
