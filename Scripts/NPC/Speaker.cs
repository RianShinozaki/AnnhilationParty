using Godot;
using System;

public partial class Speaker : Control
{
	public bool initialized = false;
    public override void _Ready()
    {
        base._Ready();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
		if(!initialized) {
			initialized = true;
			GameController.theSpeaker = this;
			textbox_system.Instance.Initialize(0);
		}
    }
    public virtual DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case 0:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Dialogue line 1",
						"Dialogue line 2",
						"Dialogue line 3"
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
			case 1:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Let me ask you a question...",
						"Yes or no?",
					},
					new Godot.Collections.Array{
						"test_1_1",
						"test_1_2",
						"test_1_3"
					},
					new Godot.Collections.Array{
						"Yes",
						"No",
						"options",
						"it depends",
						"oscillating"
					},
					new Godot.Collections.Array{
						2,
						3,
						4,
						5,
						6,
						
					}
				);

				break;
			case 2:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Hard agree.",
					},
					new Godot.Collections.Array{
						"test_2_1"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);

				break;
			case 3:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Disappointing..."
					},
					new Godot.Collections.Array{
						"test_3_1"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1,
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
