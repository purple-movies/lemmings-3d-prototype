using com.draconianmarshmallows.boilerplate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : DraconianBehaviour
{
    public enum Behaviour { WALKER, BLOCKER }

    public Behaviour behaviour { get { return mBehaviour; } set { initializeBehviour(value); } }

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject blockObstaclePrefab;
    private LevelController levelController;
    private UIController uiController;
    private Collider goalCollider;
    private GameObject blockObstacle;
    private Behaviour mBehaviour = Behaviour.WALKER;

    public bool active { set { gameObject.SetActive(value); } }

    public void initialize(LevelController levelController)
    {
        reset();
        this.levelController = levelController;
        uiController = UIController.instance;
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

    //protected override void OnMouseDown()
    //{
    //    base.OnMouseDown();
    //    uiController.onAgentClicked(this);
    //}

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.Equals(goalCollider))
            levelController.agentReachedGoal(this);
        else if (other.tag.Equals(TagNames.AGENT_KILLER))
            destroy();
    }

    private void reset()
    {
        levelController = null;
        goalCollider = null;
    }

    private void initializeBehviour(Behaviour behaviour)
    {
        mBehaviour = behaviour;

        switch (behaviour)
        {
            case Behaviour.BLOCKER:
                startBlocking();
                break;
            default: break;
        }
    }

    private void startBlocking()
    {
        navMeshAgent.destination = transform.position;
        navMeshAgent.enabled = false;
        blockObstacle = Instantiate(blockObstaclePrefab, transform.position, transform.rotation);
    }
}
