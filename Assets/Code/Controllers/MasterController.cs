using com.draconianmarshmallows.boilerplate.controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : BaseMasterController
{
    protected override void Start()
    {
        base.Start();
        startGame();
    }
}
