using com.draconianmarshmallows.boilerplate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : DraconianBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private LevelController levelController;
    private Collider goalCollider;

    public bool active { set { gameObject.SetActive(value); } }

    public void initialize(LevelController levelController)
    {
        reset();
        this.levelController = levelController;
    }

    public void setGoal(Transform goalTransform)
    {
        goalCollider = goalTransform.GetComponent<Collider>();
        navMeshAgent.destination = goalTransform.position;
    }

    public void destroy()
    {
        levelController.onAgentDestroyed(this);
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.Equals(goalCollider))
            levelController.agentReachedGoal(this);
    }

    private void reset()
    {
        levelController = null;
        goalCollider = null;
    }
}
