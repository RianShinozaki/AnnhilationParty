using Godot;
using System;

public partial class InvestigationScene : Node2D
{
	Sprite2D Fade;
	PackedScene toScene;
	Node instantiatedScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instantiatedScene = GetNode("SpecificSceneHolder").GetNode("Office");

		GameController.Instance.SwitchSceneTransitionBegin += OnSwitchSceneTransitionBegin;

		Fade = GetNode<Sprite2D>("Fade");
		Fade.Modulate = new Color(0, 0, 0, 1);

		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(Fade, "modulate", new Color(0, 0, 0, 0), 1.0f).SetTrans(Tween.TransitionType.Sine);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnSwitchSceneTransitionBegin(string newScene) {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(Fade, "modulate", Colors.White, 1.0f).SetTrans(Tween.TransitionType.Sine);
		tween.Finished += OnSwitchScene;
		toScene = GD.Load<PackedScene>(newScene);
	}
	public void OnSwitchScene() {
		GameController.Instance.OnSwitchScene();
		GameController.Instance.SwitchSceneTransitionBegin -= OnSwitchSceneTransitionBegin;
		instantiatedScene.QueueFree();
		instantiatedScene = toScene.Instantiate();
		GetNode("SpecificSceneHolder").AddChild(instantiatedScene);

		GD.Print("Here");
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(Fade, "modulate", new Color(0, 0, 0, 0), 1.0f).SetTrans(Tween.TransitionType.Sine);

		GD.Print("Here2");
	}
}
