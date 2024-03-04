using Godot;
using System;

public partial class DebugScreenSlider : Clickable
{
    public override void OnClick()
    {
        base.OnClick();
        GameController.SetSplitX(Position.X);
        GD.Print("hm");
    }
}
