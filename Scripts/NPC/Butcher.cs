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
		/*GameController.Instance.ChangeMoney(-20);
		GameController.trustLevels[GameController.BUTCHER] += 1;
		GameController.AddToInventory(steak);
		*/
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
						"So, what can I do for you?",
						"*A wide range of cuts hang from the ceiling or lie beneat the counter's glass.",
						"*The prices are at a premium.",
						"*... You get the feeling the Butcher is appraising your order."
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Looking for something nice.",
						"Looking for some deli hams.",
						"How expensive."
					},
					new Godot.Collections.Array{
						2,
						6,
						99
					}
				);
				break;
			case 2:
				if(GameController.butcherMemory[0] != 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"And why not? I could cut you a rib-eye -- that's real nice.",
							"Good, yes?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Sounds good. [$120]",
							"Actually, on second thought..."
						},
						new Godot.Collections.Array{
							5,
							1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"And why not? I could cut you a rib-eye -- that's real nice.",
							"You have a nice little somebody to impress?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Yep.",
							"Nope."
						},
						new Godot.Collections.Array{
							3,
							4
						}
					);
				}
				break;
			case 3:
				GameController.butcherMemory[0] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Hoho! Well, you came to the right man. My artistic cuts of steak are known to capture hearts.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$120]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						5,
						1,
					}
				);
				break;
			case 4:
				GameController.butcherMemory[0] = 3;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, a man has to treat themselves too.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$120]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						5,
						1,
					}
				);
				break;
			case 5:
				if(GameController.money >= 120) {
					GameController.Instance.ChangeMoney(-120);
					GameController.trustLevels[GameController.BUTCHER] += 1;
					GameController.AddToInventory(steak);
					if(GameController.butcherMemory[0] == 1) {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher cuts a prime chunk of steak down and wraps it up. It takes seconds.",
								"To a fine evening for you, eh?",
								"*Acquired Rib-Eye Steak."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					} else {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher cuts a prime chunk of steak down and wraps it up. It takes seconds.",
								"Here you are.",
								"*Acquired Rib-Eye Steak."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					}
					
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
			
			case 6:
				if(GameController.butcherMemory[0] != 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Yes, yes, good choice. Healthy stuff.",
							"Good, yes?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Sounds good. [$80]",
							"Actually, on second thought..."
						},
						new Godot.Collections.Array{
							9,
							1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"Yes, yes, good choice. Healthy stuff. You have kids?",
						},
						new Godot.Collections.Array{
							"butcher_2_2",
							"nan"
						},
						new Godot.Collections.Array{
							"Yep.",
							"Nope."
						},
						new Godot.Collections.Array{
							7,
							8
						}
					);
				}
				break;
			case 7:
				GameController.butcherMemory[0] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Doesn't matter what they say -- the real stuff's always better.",
						"Hard to find out here, but you'd always want the best for your kids, right?",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$80]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						9,
						1,
					}
				);
				break;
			case 8:
				GameController.butcherMemory[0] = 2;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well then, this'll keep you in good health for a while.",
						"So, good then?"
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
						"Sounds good. [$80]",
						"Actually, on second thought..."
					},
					new Godot.Collections.Array{
						9,
						1,
					}
				);
				break;
			case 9:
				if(GameController.money >= 80) {
					GameController.Instance.ChangeMoney(-80);
					GameController.trustLevels[GameController.BUTCHER] += 1;
					GameController.AddToInventory(steak);
					if(GameController.butcherMemory[0] == 2) {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher slams on the counter a hefty sack of deli ham.",
								"To their good health.",
								"*Acquired Deli Ham."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					} else {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*The Butcher slams on the counter a hefty sack of deli ham.",
								"Here you are.",
								"*Acquired Deli Ham."
							},
							new Godot.Collections.Array{
								"nan",
								"butcher_3_1",
								"nan"
							},
							new Godot.Collections.Array{
							},
							new Godot.Collections.Array{
								10
							}
						);
					}
					
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
			
			case 10:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The Butcher studies you carelessly.",
						""
					},
					new Godot.Collections.Array{
						"butcher_1_1",
						"nan",
						"nan",
						"nan"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);
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
