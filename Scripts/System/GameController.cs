using Godot;
using System;



public partial class GameController : Node
{
    [Signal]
    public delegate void SwitchSceneTransitionBeginEventHandler(string newScene);
    [Signal]
    public delegate void SwitchSceneEventHandler();

    public enum GameState {
        Office,
        SuspectLocation,
        Dialogue
    }

    public enum Location {
        Office,
        Butcher
    }
    
	public static GameController Instance;
    
	public static float splitX;
	public static float split2X;
	public static float split3X;
	static float wishSplitX;

    public override void _Ready()
    {
        base._Ready();
        wishSplitX = 225;
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
        Instance = this;
    }
    public static void SetSplitX(float x) {
		wishSplitX = x;
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		splitX = Mathf.Lerp(splitX, wishSplitX, 0.1f);
		split2X = Mathf.Lerp(split2X, wishSplitX-2, 0.08f);
		split3X = Mathf.Lerp(split3X, wishSplitX-4, 0.06f);
    }

    public void OnSwitchScene() {
        EmitSignal(SignalName.SwitchScene);
    }
    public void OnSwitchSceneTransitionBegin(string newScene) {
        EmitSignal(SignalName.SwitchSceneTransitionBegin, newScene);
        wishSplitX = 50;
    }
}

