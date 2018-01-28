using com.draconianmarshmallows.boilerplate.controllers;

public class MasterController : BaseMasterController
{
    protected override void Start()
    {
        base.Start();
        startGame();
    }
}
