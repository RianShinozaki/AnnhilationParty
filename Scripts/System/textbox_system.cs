using Godot;
using System;
using System.Linq;

public partial class textbox_system : Control
{
	public DialogueSet dialogueSet;
	public int idx;
	public RichTextLabel dialogue;
	public float visibleT;
	public Button nextButton;
	bool responseMode = false;

	[Export] Control ResponseGroup;
	[Export] PackedScene ResponseButton;

    public override void _Ready()
    {
		dialogue = GetNode("Container").GetNode<RichTextLabel>("Dialogue");
		nextButton = GetNode<Button>("NextButton");

        base._Ready();
		GetNextDialogue(0);
    }

	public void GetNextDialogue(int id) {
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
						"No"
					},
					new Godot.Collections.Array{
						2,
						3
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
				QueueFree();
				break;
		}

		visibleT = 0;
		idx = 0;
		dialogue.Text = (string)dialogueSet.lines[idx];
		dialogue.VisibleCharacters = 0;
		GD.Print("test?");
	}

	//METHODS

    public override void _Process(double delta)
    {
        base._Process(delta);

		if(!responseMode) {
			//Typewriter effect
			if(dialogue.VisibleCharacters < dialogue.GetTotalCharacterCount()) {
				visibleT += (float)delta * 20;
				dialogue.VisibleCharacters = Mathf.RoundToInt(visibleT);
				nextButton.Visible = false;

			}
			else {
				//If no more letters to display, make the next button visible
				nextButton.Visible = true;
			}
		}
		else {
			nextButton.Visible = false;
		}
    }

	private void _on_next_button_pressed() {
		if(idx == dialogueSet.lines.Count-1) {
			//If there are any responses, switch to response mode. Otherwise, go to the next dialogue
			if(dialogueSet.responses.Count == 0)
				GetNextDialogue((int)dialogueSet.nextLines[0]);
			else {
				SetResponses();
			}
			return;
		}
		visibleT = 0;
		idx++;
		dialogue.Text = (string)dialogueSet.lines[idx];
		dialogue.VisibleCharacters = 0;
	}
	
	private void SetResponses() {
		dialogue.Visible = false;
		ResponseGroup.Visible = true;
		responseMode = true;

		//Clear existing responses
		foreach(Node n in ResponseGroup.GetChildren()) {
			n.QueueFree();
		}
		//Add each response as a button
		for(int i = 0; i < dialogueSet.responses.Count; i++) {
			ResponseButton rb = (ResponseButton)ResponseButton.Instantiate();
			ResponseGroup.AddChild(rb);
			rb.aNumber = (int)dialogueSet.nextLines[i];
			rb.Pressed += _on_response_pressed;
			rb.Text = (string)dialogueSet.responses[i];
		} 
	}
	private void _on_response_pressed(int nextLine) {
		GD.Print("woohoo");
		GetNextDialogue(nextLine);
		dialogue.Visible = true;
		ResponseGroup.Visible = false;
		responseMode = false;
	}
	
}
