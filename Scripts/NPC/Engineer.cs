using Godot;
using System;

public partial class Engineer : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;
	float trustAtStartOfMeeting;

	public Godot.Collections.Array questionOptions = new Godot.Collections.Array{
		"*Spend the night working silently.",
		"Besides 'Auta', what else are you into?",
		"You looking forward to anything?",
		"How'd you end up in militia work?",
		"So, you seeing anyone?",
		"Everything been alright with you?",
		"Listen, about that girl..."
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		-1,
		-1,
		-1,
		-1,
		-1
	};
	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.SOFTWARE];
        base._Ready();
		GameController.theSpeaker = this;

		GameController.engineerMemory[1] = 1;
		textbox_system.Instance.Initialize(-2);
		GameController.engineerQuestionFlags[0] = true;
		GameController.engineerQuestionFlags[1] = true;
		return;

		if(GameController.currentTime != 1 
			|| GameController.GetDay(GameController.currentDay) == "Friday" 
			|| GameController.GetDay(GameController.currentDay) == "Saturday"
			|| GameController.GetDay(GameController.currentDay) == "Wednesday") {
				textbox_system.Instance.Initialize(-100);
				NPCSprite.Visible = false;
				return;
			}
		
		textbox_system.Instance.Initialize(-2);
		
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	public void Init() {
		
		/*if(GameController.engineerMemory[1] == 0) {
			textbox_system.Instance.Initialize(-2);
			return;
		} else {
			//textbox_system.Instance.Initialize(-2);
		}*/
		
	}

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		switch(id){
			case -100:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*The Engineer isn't here today.",
						"*No point in buying coffee here. Better go back."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			case -2:
				if(GameController.engineerMemory[1] == 0 || GameController.money < 8) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*Coffee here costs $8... Can't get a seat without paying."
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							"*Get a coffee.",
							"*Go home."
						},
						new Godot.Collections.Array{
							-3,
							-1
						}
					);
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*Coffee here costs $8... Can't get a seat without paying."
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							"*Get a coffee.",
							"*Go home."
						},
						new Godot.Collections.Array{
							100,
							-1
						}
					);
				}
				break;
			case -3:
				if(GameController.money >= 8) {
					GameController.Instance.ChangeMoney(-8);
					if(GameController.engineerMemory[0] == 0) {
						dialogueSet = new DialogueSet(
							new Godot.Collections.Array{
								"*You get your coffee and take a seat beside the Engineer.",
								"*He's looks like he's forcing himself to stay focused... the bags around his eyes speak of many recent long nights.",
								"*His laptop is covered in stickers, but you don't recognize any of them.",
								"...",
								"*You can't think of a way to get his attention...",
								"*Maybe you should check the log book for ideas?"
							},
							new Godot.Collections.Array{

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
								"*You get your coffee and take a seat beside the Engineer.",
								"*He's looks like he's forcing himself to stay focused... the bags around his eyes speak of many recent long nights.",
								"*His laptop is covered in stickers...",
								"*That's right. You know about the Engineer's watch history... that character is from the anime 'Auta'",
								"*...That'd be one way to break the ice."
							},
							new Godot.Collections.Array{

							},
							new Godot.Collections.Array{
								"Hey... is that an 'Auta' sticker?"
							},
							new Godot.Collections.Array{
								0
							}
						);
					}
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"*Can't afford the coffee...",
							"Honestly. What is this world coming to. Better go home."
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							-1
						}
					);
				}
				break;
			case 0:
				animPlayer.Play("Intro");
				GameController.engineerMemory[1] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*Hearing the name of his favorite show, he snaps his head up. Fast.",
						"...",
						"Oh wow. You actually recognize this one?",
						"Not a lot of people get it. It's a pretty obscure show, after all..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Well, I just started watching it.",
						"Oh man. I watch it ALL the time."
						
					},
					new Godot.Collections.Array{
						1,
						2
					}
				);
				break;
			
			case 1:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Ah, wow, you've got a lot to look forward to.",
						"Around episode thirteen is when it gets absolutely crazy...",
						"The ending is kind of divisive, but I thought it was brilliant...",
						"--Shit, sorry. You just started watching it. I should be careful.",
						"*The Engineer clearly wants to talk more about this show. He's restraining himself that much."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			
			case 2:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh man, so you know how crazy it gets...",
						"Who's your favorite character?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"...I like all of them.",
						"...Bet you can guess."
					},
					new Godot.Collections.Array{
						3,
						4
					}
				);
				break;
			
			case 3:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"No, yeah, that's so fair.",
						"I mean, they're all such great characters. I get not being able to pick just one.",
						"...",
						"Mine's Auta herself, actually.",
						"I know it's kind of generic to pick the main character as your favorite, but...",
						"Well, I think she's really cool. For a lot of reasons.",
						"*The Engineer clearly wants to talk more about this show. He looks like he's restraining himself."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			
			case 4:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Probably Captain Mighty, huh? She is a fan favorite.",
						"I really admire how they make her super-strength into a device for her internal conflict...",
						"But my favorite's Auta herself, actually.",
						"I know it's kind of generic to pick the main character as your favorite, but...",
						"Well, I think she's really cool. For a lot of reasons.",
						"*The Engineer clearly wants to talk more about this show. He looks like he's restraining himself."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			
			case 5:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer smiles, awkwardly."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"You come here often?",
						"When's the last time you slept...?",
						"So, What late-night drudgery are you working on?",
					},
					new Godot.Collections.Array{
						6,
						14,
						7
					}
				);
				break;
			
			case 6:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Pretty much, yeah. More than I should. Just helps me get out of the house.",
						"Sort of makes me feel like a character in one of those old Neo-Noirs, too...",
						"Sitting with a cup of coffee by the window as rain splatters on the ground...",
						"Not that it ever rains anymore. But you know what I mean."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			
			case 7:
				if(GameController.engineerMemory[2] == 0) {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"...it's nothing too interesting. Drudgery is correct.",
							"I work for Koxhos Corp. The, uh... militia company.",
							"...",
							"I don't usually mean to tell people that.",
							"It's nothing special, really. I'm a systems engineer. I just install stuff, and... remind people to turn off their computers sometimes.",
							"What do you do?"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							"I'm a burgeoning writer.",
							"I'm a free man. (Unemployed.)",
							"I'm an engineer, too."
						},
						new Godot.Collections.Array{
							8,
							11,
							12
						}
					);
					
				} else {
					dialogueSet = new DialogueSet(
						new Godot.Collections.Array{
							"...it's nothing too interesting. Drudgery is correct.",
							"I work for Koxhos Corp. The, uh... militia company.",
							"...",
							"I don't usually mean to tell people that.",
							"It's nothing special, really. I'm a systems engineer. I just install stuff, and... remind people to turn off their computers sometimes.",
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
						},
						new Godot.Collections.Array{
							5
						}
					);
				}
				break;
			case 8:
				GameController.engineerMemory[2] = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"A writer, huh?",
						"Have you written anything I might know?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"That's a secret.",
						"Haven't released anything..."
					},
					new Godot.Collections.Array{
						9,
						10
					}
				);
				break;
			case 9:
			
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Don't want people knowing who you are in public?",
						"That's kind of cool, honestly.",
						"Nobody knows who created 'Auta', either. They use a pen-name for everything.",
						"...",
						"Blink once if it was you.",
						"...",
						"Man."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						13
					}
				);
				break;
			case 10:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Must be hard work.",
						"I used to want to be a writer too, honestly. I read so many comics when I was a kid...",
						"Then, I sat down in front of a piece of paper and realized the unfortunate truth.",
						"I have no idea what to write..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						13
					}
				);
				break;
			case 11:
				GameController.engineerMemory[2] = 2;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I can't really blame you. Way things are these days...",
						"Uh, honestly, I'd quit my job if I could. But I find the idea of trying to find something else right now... much scarier than the idea of doing this forever."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						13
					}
				);
				break;
			case 12:
				GameController.engineerMemory[2] = 3;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Did they tell you we were in high demand, too?",
						"That's how they get us.",
						"...I guess under Trade-Secret Clause we're actually not allowed to talk about this anymore.",
						"Never know who's a plant..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						13
					}
				);
				break;
			case 13:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer sighs as he looks back at his laptop.",
						"Honestly? These days I don't do much besides work.",
						"It's not the worst thing, in the world, but...",
						"Damn. I really used to have a life, you know?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Same.",
						"Very much not same."
					},
					new Godot.Collections.Array{
						20,
						21
					}
				);
				break;
			case 14:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer snorts.",
						"Could say the same to you. Your eye bags are practically dragging on the ground.",
						"Sleepless recognizes sleepless, huh?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"The horrors keep me awake all night.",
						"We could solve this issue together.",
						"I persist out of pure spite, thank you."
					},
					new Godot.Collections.Array{
						15,
						18,
						19
					}
				);
				break;
			case 15:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"The, uh, horrors, huh?",
						"Are you referring to the societal kind, or are you just disturbed?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Society",
						"I'm a sick man."
					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			case 16:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah. Well. Can't argue with you on that one."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			case 17:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer laughs.",
						"Yeah, what the hell. Why not. I don't think I'll be much help there, but we can still chat."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			case 18:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer looks at you incredulously, and then laughs.",
						"Woah. Is this a meet-cute? I didn't realize they happened in real life.",
						"...",
						"Well, I'm flattered. But I'm, uh, not really doing that kind of stuff right now."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			case 19:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer frowns and nods.",
						"I can respect that."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						5
					}
				);
				break;
			case 20:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Maybe it was just the way things were when we were kids.",
						"*The Engineer sighs.",
						"Hah, well... it's not often I meet someone else who knows my favorite anime, so...",
						"Maybe this is a brand new friendship meant to be."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						22
					}
				);
				break;
			case 21:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, we all had different circumstances growing up, I guess...",
						"*The Engineer sighs.",
						"I mean. I don't think I'd have the confidence to come up to someone and ask them about 'Auta', so...",
						"You can't be too bad."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						22
					}
				);
				break;
			case 22:
				GameController.engineerQuestionFlags[0] = true;
				GameController.engineerQuestionFlags[1] = true;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"But anyway...",
						"I need to finish this big project tonight. I should probably get back to this...",
						"It was... nice to talk to you, though.",
						"I'm here after work a lot. If you see me, come say hi again, alright?",
						"...",
						"*You've become acquainted with the Engineer. Time to head back."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			
			
			case 100:
				GameController.Instance.ChangeMoney(-8);

				Godot.Collections.Array theDialogue = new Godot.Collections.Array{
					"*You see the Engineer and say hello. Briefly, you chat about your respective days."
				};
				if(GameController.trustLevels[GameController.SOFTWARE] < 1) {
					theDialogue.Add("*A momentary awkward silence ensues.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 1 && GameController.trustLevels[GameController.SOFTWARE] < 2) {
					theDialogue.Add("*The Engineer seems comfortable around you.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 2 && GameController.trustLevels[GameController.SOFTWARE] < 3) {
					theDialogue.Add("*The Engineer looks happy to see you.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 3 && GameController.trustLevels[GameController.SOFTWARE] < 4) {
					theDialogue.Add("*The Engineer seems like they're opening up to you.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 4) {
					theDialogue.Add("*You sense a good deal of trust from the Engineer.");
				}

				theDialogue.Add("*Now's a good time to get to know him better.");

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.engineerQuestionFlags[i] == true) {
						theQuestions.Add(questionOptions[i]);
						theIndices.Add(questionIndices[i]);
					}
				}

				animPlayer.Play("Intro");
				dialogueSet = new DialogueSet(
					theDialogue,
					new Godot.Collections.Array{
						
					},
					theQuestions,
					theIndices
				);
				break;
			
			case 105:
				GameController.trustLevels[GameController.SOFTWARE] += 0.5f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The two of you work together in a companionable silence.",
						"*Some time passes. Time for you to head home."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						106
					}
				);
				break;
			case 106:
				Godot.Collections.Array byeDialogue = new Godot.Collections.Array{};
				if(trustAtStartOfMeeting == GameController.trustLevels[GameController.SOFTWARE] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.SOFTWARE]) ) {
					byeDialogue.Add("*The Engineer definitely trusts you a little more after today.");
				} else if (trustAtStartOfMeeting < GameController.trustLevels[GameController.SOFTWARE] ) {
					byeDialogue.Add("*You think you grew a little closer to the Engineer today.");
				} else {
					byeDialogue.Add("*Something is wrong here.");
				}
				dialogueSet = new DialogueSet(
					byeDialogue,
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						-1
					}
				);
				break;
			case 110:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"ANIMEEEEEE."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						106
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
