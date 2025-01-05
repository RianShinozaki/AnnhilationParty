using Godot;
using System;

public partial class SuspectLogs : Clickable
{
    public override void OnClick()
    {
        SuspectLog.Instance.Init();
    }
    public override void CheckActive()
    {
        active = GameController.Instance.currentState == GameController.GameState.Office;
    }
}
