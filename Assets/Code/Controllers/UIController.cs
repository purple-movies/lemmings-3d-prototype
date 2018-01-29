using com.draconianmarshmallows.boilerplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : DraconianBehaviour
{
    [SerializeField] private MasterController masterController;
    [SerializeField] private Camera currentCamera;

    public static UIController instance { get { return mInstance; } }
    private static UIController mInstance;

    protected override void Awake()
    {
        if (mInstance) Destroy(gameObject);
        mInstance = this;
    }

    protected override void Update()
    {
        base.Update();
        RaycastHit hit;
        var ray = currentCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2000) && Input.GetMouseButtonDown(0))
        {
            Collider hitCollider = hit.collider;
            if (hitCollider.tag.Equals(TagNames.AGENT))
                onAgentClicked(hitCollider.GetComponent<AgentController>());
        }
    }

    private void onAgentClicked(AgentController agent)
    {
        Debug.Log("Agent clicked :: " + agent);

        agent.behaviour = AgentController.Behaviour.BLOCKER;
    }
}
