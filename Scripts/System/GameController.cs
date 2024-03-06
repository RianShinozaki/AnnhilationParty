using Godot;
using System;



public partial class GameController : Node
{
    [Signal]
    public delegate void SwitchSceneTransitionBeginEventHandler(string newScene);
    [Signal]
    public delegate void SwitchSceneEventHandler();
    [Signal]
    public delegate void MoneyChangedEventHandler();

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
    public static Speaker theSpeaker;
    public static Location currentLocation;
    public static GameState currentState;


    const int MORNING = 0;
    const int EVENING = 1;
    const int NIGHTFALL = 2;

    public static int currentTime = 0;

    public static int currentDay = 1;

    public const int BUTCHER = 0;
    public const int SOFTWARE = 1;
    public const int TEACHER = 2;
    public const int BARTENDER = 3;
    public const int OCCULTER = 4;
    public const int OLDGUARD = 5;

    public static float money;
    public static Godot.Collections.Array items = new Godot.Collections.Array();

    public static float[] trustLevels = new float[5];

    public static short[] butcherMemory = new short[10];
    //0 -- marital status

    public override void _Ready()
    {
        base._Ready();
        wishSplitX = 225;
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://MainGame2D.tscn");
        Instance = this;

        for(int i = 0; i < trustLevels.Length; i++) {
            trustLevels[i] = 0;
        }

        money = 200;
    }
    public static void SetSplitX(float x) {
		wishSplitX = x;
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		splitX = Mathf.Lerp(splitX, wishSplitX, (float)delta*5);
		split2X = Mathf.Lerp(split2X, wishSplitX-2, (float)delta*4);
		split3X = Mathf.Lerp(split3X, wishSplitX-4, (float)delta*3);
    }

    public void OnSwitchScene() {
        EmitSignal(SignalName.SwitchScene);
    }
    public void OnSwitchSceneTransitionBegin(string newScene) {
        EmitSignal(SignalName.SwitchSceneTransitionBegin, newScene);
        if(currentLocation == Location.Office) {
            wishSplitX = 225;
            currentState = GameState.Office;
            currentTime++;
        } else {
            wishSplitX = 50;
            currentState = GameState.SuspectLocation;
        }
    }
    public static void AddToInventory(Item item) {
        items.Add(item);
    }
    public void ChangeMoney(float amount) {
        money += amount;
        EmitSignal(SignalName.MoneyChanged);
    }
    public void SetMoney(float amount) {
        money = amount;
        EmitSignal(SignalName.MoneyChanged);
    }
}

