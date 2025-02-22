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
    [Signal]
    public delegate void BeginIntroSequenceEventHandler();

    public enum GameState {
        Office,
        SuspectLocation,
        Dialogue,
        Transitioning,
        SuspectLog,
        FinalChoice
    }

    public enum Location {
        Office,
        Butcher,
        Teacher,
        Engineer,
        Occultist
    }

    public static Godot.Collections.Array days = new Godot.Collections.Array{
		"Friday",
		"Saturday",
		"Sunday",
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday"
	};
    
	public static GameController Instance;
    
	public float splitX;
	public float split2X;
	public float split3X;
	float wishSplitX;
    public Speaker theSpeaker;
    public Location currentLocation;
    public GameState currentState;


    const int MORNING = 0;
    const int EVENING = 1;
    const int NIGHTFALL = 2;

    public const int BUTCHER = 0;
    public const int SOFTWARE = 1;
    public const int TEACHER = 2;
    public const int BARTENDER = 3;
    public const int OCCULTER = 4;
    public const int OLDGUARD = 5;



    //TO SAVE
    public bool shouldDoIntro = true;
    public int steaks = 0;
    public int hams = 0;
    public int brokenPhones = 0;

    public float money;
    public Godot.Collections.Array items = new Godot.Collections.Array();
    public int currentTime = 0;
    public int currentDay = 1;

    public float[] trustLevels = new float[5];

    public float[] butcherMemory = new float[10];
    //0 -- marital status
    //1 -- has met :: 0 -- no :: 1 -- yes

    public float[] teacherMemory = new float[10];
    //0 -- has kids :: 1 -- no :: 2 -- yes
    //1 -- has met :: 0 -- no :: 1 -- yes
    //2 -- worked w/o them :: 0 -- no :: 1 -- yes

    public float[] engineerMemory = new float[10];
    //0 -- know his fav anime ? :: 0 -- no :: 2 -- yes
    //1 -- has met :: 0 -- no :: 1 -- yes
    //2 -- know job :: 0 -- ? :: 1 -- writer :: 2 -- unemployed :: 3 -- engineer
    //3 -- final dialogue

    public float[] occultistMemory = new float[10];
    //0 -- has met :: 0 -- no :: 1 -- yes
    //1 -- fortunes pulled
    //2 -- final dialogue

    public Godot.Collections.Array<bool> engineerQuestionFlags = new Godot.Collections.Array<bool>(new bool[10]);
    public Godot.Collections.Array<bool> butcherQuestionFlags = new Godot.Collections.Array<bool>(new bool[10]);
    public Godot.Collections.Array<bool> OccultistQuestionFlags = new Godot.Collections.Array<bool>(new bool[10]);
    public Godot.Collections.Array<bool> teacherQuestionFlags = new Godot.Collections.Array<bool>(new bool[10]);

    public bool oldGuardCheck = false;
    
    public int timesCalledOldGuard = 0;

    public bool goodEnding = false;
    public bool canReturnButtonAppear = false;

    public override void _Ready()
    {
        base._Ready();
        wishSplitX = 215;
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://TitleScreen.tscn");
        Instance = this;

        for(int i = 0; i < trustLevels.Length; i++) {
            trustLevels[i] = 0;
        }

        SetMoney(150);
    }
    public void SetSplitX(float x) {
		wishSplitX = x;
	}
    public override void _Process(double delta)
    {
        

        base._Process(delta);
		splitX = Mathf.Lerp(splitX, wishSplitX, (float)delta*5);
		split2X = Mathf.Lerp(split2X, wishSplitX-2, (float)delta*4);
		split3X = Mathf.Lerp(split3X, wishSplitX-4, (float)delta*3);
    }

    public void DoIntro() {
        shouldDoIntro = false;
        wishSplitX = 600;
        splitX = 600;
        split2X = 600;
        split3X = 600;
        EmitSignal(SignalName.BeginIntroSequence);
    }

    public void OnSwitchScene() {
        EmitSignal(SignalName.SwitchScene);
        if(currentLocation == Location.Office && currentTime == 0 && currentDay%7==0) {
            SetMoney(150);
        }
        
    }
    public void OnSwitchSceneTransitionBegin(string newScene) {
        EmitSignal(SignalName.SwitchSceneTransitionBegin, newScene);
        if(currentLocation == Location.Office) {
            wishSplitX = 215;
            currentState = GameState.Office;
            currentTime++;
            if(currentTime > 1) {
                currentDay++;
                currentTime = 0;
                Save();
                GD.Print("saved");
            }
        } else {
            wishSplitX = 50;
            currentState = GameState.SuspectLocation;
        }
    }
    public void AddToInventory(Item item) {
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
    public string GetDay(int day) {
        return (string)days[(day-1)%7];
    }

    public void Save()
    {
        Godot.Collections.Dictionary<string, Variant> saveData = new Godot.Collections.Dictionary<string, Variant>()
        {
            { "shouldDoIntro", shouldDoIntro},
            { "steaks", steaks},
            { "hams", hams},
            { "brokenPhones", brokenPhones},
            { "money", money},
            { "items", items},
            { "currentDay", currentDay},
            { "trustLevels", trustLevels},
            { "butcherMemory", butcherMemory},
            { "teacherMemory", teacherMemory},
            { "engineerMemory", engineerMemory},
            { "occultistMemory", occultistMemory},
            { "engineerQuestionFlags", engineerQuestionFlags},
            { "butcherQuestionFlags", butcherQuestionFlags},
            { "OccultistQuestionFlags", OccultistQuestionFlags},
            { "teacherQuestionFlags", teacherQuestionFlags},
            { "oldGuardCheck", oldGuardCheck},
            { "timesCalledOldGuard", timesCalledOldGuard},
        };
        var jsonString = Json.Stringify(saveData);
        using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
        saveFile.StoreLine(jsonString);
    }
    public void Load() {
        using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
        var jsonString = saveFile.GetLine();
        GD.Print(jsonString);
        var json = new Json();
        var parseResult = json.Parse(jsonString);

        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
        }

        

        var saveData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

        foreach (var (key, value) in saveData) {
            Set(key, value);
            GD.Print(key, " = ", value);
            GD.Print(key, " = ", Get(key));
        }
    }
}

