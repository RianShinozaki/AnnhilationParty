using Godot;
using System;

public partial class Butcher : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;
	public override void _Ready()
    {
        base._Ready();
		animPlayer.Play("Intro");
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	public void Init() {
		GameController.theSpeaker = this;
		switch(Mathf.FloorToInt(GameController.trustLevels[GameController.BUTCHER])) {
			case 0:
				textbox_system.Instance.Initialize(0);
				break;
			case 1:
				textbox_system.Instance.Initialize(100);
				break;
		}

	}

	public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case 0:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, well! There's the fresh meat I ordered.",
						"Little bit fresher than I was expecting, but we'll make do!",
						"--Hah! The look on your face. It's just my little joke."
					},
					new Godot.Collections.Array{
						"butcher_0_1",
						"butcher_0_2",
						"butcher_0_3"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						1
					}
				);
				break;
			case 1:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"So, what can I do for you?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
					},
					new Godot.Collections.Array{
						"Purchase steak [$20]",
						"Just looking..."
					},
					new Godot.Collections.Array{
						2,
						3
					}
				);
				break;
			case 2:
				if(GameController.money >= 20) {
					GameController.money -= 20;
					GameController.trustLevels[GameController.BUTCHER] += 1;
					GameController.AddToInventory(steak);
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Can do!",
							"*The Butcher slams a hefty chunk of steak on the counter and wraps it up.",
							"*Acquired Steak."
						},
						new Godot.Collections.Array{
							"butcher_2_1",
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							-1
						}
					);
				}
				else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*You can't afford that..."
						},
						new Godot.Collections.Array{
							"nan"
						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							1,
						}
					);
				}
				break;
			case 100:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Trust Level 2 Dialogue"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			default:
				dialogueSet = null;
				break;
		}
		return dialogueSet;
	}
}
