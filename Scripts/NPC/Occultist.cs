using Godot;
using System;

public partial class Occultist : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;
	public override void _Ready()
    {
        base._Ready();
		GameController.theSpeaker = this;
		if(GameController.currentTime != 1 
			|| GameController.GetDay(GameController.currentDay) == "Thursday" 
			|| GameController.GetDay(GameController.currentDay) == "Saturday") {
				textbox_system.Instance.Initialize(-100);
				NPCSprite.Visible = false;
				return;
			}
		animPlayer.Play("Intro");
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	public void Init() {
		
		
		if(GameController.teacherMemory[1] == 0) {
			textbox_system.Instance.Initialize(0);
			return;
		} else {
			textbox_system.Instance.Initialize(100);
		}
	}

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case -100:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*The Teacher isn't here.",
						"*You're not sure what to do. Better go back."
					},
					new Godot.Collections.Array{
						"test_1",
						"test_2",
						"test_3"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			case 0:
				GameController.teacherMemory[1] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*You see an adult surrounded by young children. The oldest of them can't be much more than 5 years old.",
						"*Their faces are dirty, their clothes old, and their knees scraped. Yet, what stands out are their widely smiling faces.",
						"*The object of their smiles: The Teacher, beaming down at them in turn.",
						"*As they see you, the Teacher turns the smile towards you as well.",
						"...",
						"Ah! What have we here?"
					},
					new Godot.Collections.Array{
						"test_1",
						"test_2",
						"test_3"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						1
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
