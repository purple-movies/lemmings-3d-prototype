using com.draconianmarshmallows.boilerplate.controllers;
using UnityEngine;

public class MasterController : BaseMasterController
{
    protected override void Start()
    {
        base.Start();
        startGame();
    }

    public override void onLevelCompleted(bool levelWon)
    {
        if (levelWon) Debug.Log("won level !!!!");
        else Debug.Log("level failed !");
    }
}
