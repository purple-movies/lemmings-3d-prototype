using com.draconianmarshmallows.boilerplate.controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : BaseLevelController
{
    protected override void Start()
    {
        base.Start();

        Debug.Log("level started");
    }
}
