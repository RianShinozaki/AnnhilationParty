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
		"So, you seeing anyone?",
		"*Something odd you saw in the logs...",
		"So, what are you gonna do about your work?",
		"Hey… would you be able to fix this phone?",
		"Any luck with that phone?"
	};
	public Godot.Collections.Array questionIndices = new Godot.Collections.Array{
		105,
		110,
		200,
		300,
		400,
		500,
		600,
		601
	};
	public Godot.Collections.Array relationshipGates = new Godot.Collections.Array{
		0,
		0,
		1,
		2,
		3,
		4,
		4,
		0
	};
	public override void _Ready()
    {
		trustAtStartOfMeeting = GameController.trustLevels[GameController.SOFTWARE];
        base._Ready();
		GameController.theSpeaker = this;
		if(GameController.brokenPhones > 0) GameController.engineerQuestionFlags[6] = true;

		if(GameController.currentTime != 1 
			|| GameController.GetDay(GameController.currentDay) == "Saturday" 
			|| GameController.GetDay(GameController.currentDay) == "Thursday"
			|| GameController.currentDay == 15 || GameController.currentDay == 16 || GameController.currentDay == 17) {
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
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh man, so you know how crazy it gets...",
						"Who's your favorite character?",
						"*The Engineer is delighted to talk about this, clearly..."
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
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer snorts.",
						"Could say the same to you. Your eye bags are practically dragging on the ground.",
						"Sleepless recognizes sleepless, huh?",
						"*The Engineer actually enjoys this solidarity."
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
						16,
						17
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
				GameController.trustLevels[GameController.SOFTWARE] += 0.25f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer looks at you incredulously, and then laughs. That totally threw him.",
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
				GameController.trustLevels[GameController.SOFTWARE] += 0.25f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Maybe it was just the way things were when we were kids.",
						"*The Engineer sighs, lost in the nostalgia.",
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
				GameController.engineerQuestionFlags[2] = true;
				GameController.engineerQuestionFlags[3] = true;

				

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
					theDialogue.Add("*The Engineer settles comfortably back into his work.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 2 && GameController.trustLevels[GameController.SOFTWARE] < 4) {
					theDialogue.Add("*The Engineer looks happy to see you.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 4 && GameController.trustLevels[GameController.SOFTWARE] < 5) {
					theDialogue.Add("*The Engineer seems like they're opening up to you.");
				}
				if(GameController.trustLevels[GameController.SOFTWARE] >= 5) {
					theDialogue.Add("*You sense a good deal of trust from the Engineer.");
				}

				Godot.Collections.Array theQuestions = new Godot.Collections.Array{};
				Godot.Collections.Array theIndices = new Godot.Collections.Array{};

				bool foundAGate = false;

				for(int i = 0; i < questionOptions.Count; i++) {
					if(GameController.engineerQuestionFlags[i] == true) {
						if (GameController.trustLevels[GameController.SOFTWARE] >= (float)relationshipGates[i]) {
							theQuestions.Add(questionOptions[i]);
							theIndices.Add(questionIndices[i]);
						} else {
							foundAGate = true;
						}
					}
				}
				if(foundAGate) {
					theDialogue.Add("*You sense that if you were closer, you would have more to talk about...");
				}

				theDialogue.Add("*Now's a good time to get to know him better.");

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
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
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
				if(GameController.trustLevels[GameController.SOFTWARE] >= 5) {
					byeDialogue.Add("*Your trust with the Engineer has maxxed out...");
				} else if(trustAtStartOfMeeting == GameController.trustLevels[GameController.SOFTWARE] ) {
					byeDialogue.Add("*You didn't grow much closer today...");
				} else if (Mathf.FloorToInt(trustAtStartOfMeeting) < Mathf.FloorToInt(GameController.trustLevels[GameController.SOFTWARE]) ) {
					byeDialogue.Add("*The Engineer definitely trusts you more after today.");
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
			case 107:
				GameController.trustLevels[GameController.SOFTWARE] += 0.25f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"That's a good one for sure.",
						"I've never really been into multiplayer games, but I used to love that one.",
						"Yeah... I dunno, hahah. I'm really just into whatever's nerdy, I guess."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Nerdy is cool."
					},
					new Godot.Collections.Array{
						111
					}
				);
				break;
			case 108:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh man, where've you been all my life?",
						"That's my absolute favorite game, too.",
						"Man. I'm such a nerd for this kind of thing."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Nerdy is cool."
					},
					new Godot.Collections.Array{
						111
					}
				);
				break;
			case 109:
				GameController.trustLevels[GameController.SOFTWARE] += 0.25f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, yeah. I guess games aren't for everyone still.",
						"I must seem like a real nerd to you, huh?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Nerdy is cool."
					},
					new Godot.Collections.Array{
						111
					}
				);
				break;
			case 110:
				GameController.engineerQuestionFlags[1] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, man. All kinds of stuff. All sorts of anime, movies, games...",
						"I like the older shows especially... Poly Zero, TechnoHearts, Death Book...",
						"Music-wise, I'm really into EDM. Helps me focus on work.",
						"And video-games, I mean... there probably isn't a retro game I haven't played.",
						"Do you play any games? What's your favorite?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Tough Coded.",
						"Captain Betz.",
						"I don't really game."
					},
					new Godot.Collections.Array{
						107,
						108,
						109
					}
				);
				break;
			case 111:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah, totally it is.",
						"I'm an adult, so I can watch whatever I like.",
						"Yeah... a software engineer, with games and anime instead of a social life...",
						"Oh no... Am I a walking stereotype?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"I wasn't gonna say it.",
						"Okay, so what's wrong with that?"
					},
					new Godot.Collections.Array{
						112,
						113
					}
				);
				break;
			case 112:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer laughs, but it's a little empty.",
						"*...Seems like they were a little bit serious about this."
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
			
			case 113:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Well, yeah, you're right.",
						"You're totally right.",
						"It's not really about how it makes me look. It's just what I like.",
						"*Despite the obvious conclusion, the Engineer is still happy to have heard this from another person.",
						"And anyway..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						115
					}
				);
				break;
			

			case 115:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Sure beats the real world, doesn't it?",
						"*...A slightly sardonic look has crossed the Engineer's face.",
						"What's wrong with pretending things can be a little easier?",
						"I can't leave this city. I'll never be able to. It's not my fault I'd rather believe in a fantasy world than my own future."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Fair enough. I too have given myself up to abject nihilism.",
						"The world's that bad, huh?"
					},
					new Godot.Collections.Array{
						116,
						117
					}
				);
				break;
			case 116:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"What? Hahah... No, man. It's not abject nihilism. It's...",
						"I mean, it's optimistic. Those shows are all about how things could become better, and...",
						"*The Engineer pauses. You've really made him think.",
						"Man."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						118
					}
				);
				break;
			case 117:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Of course it is. Who doesn't hate this stupid planet?",
						"I'd leave if I could. If it was remotely possible.",
						"*The Engineer sighs."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						118
					}
				);
				break;
			case 118:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Me and reality have a bit of a strained relationship, that's all.",
						"But you just gotta get through it all one way or another, right?",
						"*...The Engineer doesn't seem to have more to say on that topic."
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
			
			case 200:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				GameController.engineerQuestionFlags[2] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Right now? Looking forward to going home and getting some sleep. ",
						"I've, uh, been up for about 48 hours straight."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Because of work?",
						"That's wild."
					},
					new Godot.Collections.Array{
						201,
						202
					}
				);
				break;
			case 201:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Sort of. It's not exactly because of work."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						203
					}
				);
				break;
			case 202:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The Engineer laughs.",
						"Yeah. Pretty crazy." 

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						203
					}
				);
				break;
			case 203:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I don’t know. Been having a really hard time getting to sleep lately. So I just keep working instead. ",
						"So, I guess, beyond sleeping tonight? I just have more work to look forward to. ",
						"…Yeah. I mentioned I used to have a life. ",
						"I’ve been really wondering where that went, lately. ",
						"When I first got to this city, I had so much energy… I was just excited for a new start, I guess. I went out every night and met other people in the same boat as me. ",
						"We’d be out on the streets all night back in those days. We felt unstoppable. ",
						"Then, I achieved what I’d always really wanted – a stable, well-paying corporate job. ",
						"I got so busy after that, though...",
						"I really just sank into my job.",
						"But, yeah, I don’t see those guys anymore. It’s probably been too long for me to reach out, by now. ",
						"…"

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
			
			case 300:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				GameController.engineerQuestionFlags[3] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Ah... nope.",
						"Haven't had a lot of luck lately. Or... time, really.",
						"Bet you're pretty popular, though."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Yeah, I have to fight suitors off with a stick.",
						"Nope. My demented dialogue options are sometimes a turnoff."
					},
					new Godot.Collections.Array{
						301,
						302
					}
				);
				break;
			
			case 301:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Hah. Sounds tough."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						303
					}
				);
				break;
			
			case 302:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Your what nows...?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						303
					}
				);
				break;
			case 303:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"But anyway, I haven't put a lot of effort into meeting new people, so it serves me right.",
						"And I kind of screwed up my last relationship. So... I'm giving myself some time.",
						"Which is a little tough, since she's always at the park in the evening...",
						"...Right outside my work."
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
			
			case 400:

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*…That’s right. Something seemed off about the Engineer. What did you notice?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"He spent an entire day lying on the floor.",
						"He went out on a several-night bender.",
						"He quit his job.",
					},
					new Godot.Collections.Array{
						401,
						402,
						401
					}
				);
				break;
			case 401:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"That doesn't seem right...",
						"You'd better check the logbook again..."
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
			case 402:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*...Yeah. How should you approach that with him?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"I was thinking of going on a bender.",
						"Why do you smell of alcohol and regret?",
						"You okay? You look like you have a headache.",

					},
					new Godot.Collections.Array{
						403,
						404,
						405
					}
				);
				break;
			case 403:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You're... huh?",
						"Um... don't? Sorry, I don't know what to tell you there.",
						"*...The Engineer doesn't want to discuss that with you.",
						"*Maybe you need a different approach? You should try again later..."

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
			case 404:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"I... what?",
						"Um. No. I don't think I do.",
						"*...The Engineer doesn't want to discuss that with you.",
						"*Maybe you need a different approach? You should try again later..."
						
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
			case 405:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				GameController.engineerQuestionFlags[4] = false;
				GameController.engineerQuestionFlags[5] = true;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh… yeah, I do. Man, is it that obvious? ",
						"…No, I, uh… did something kind of stupid.",
						"*The Engineer closes his eyes and chuckles, regretfully. ",
						"I don't know. You ever just feel cooped up and you feel like you just have to… get out? ",
						"Like if you stay still any longer you'll just explode? ",
						"I was like that all the time as a kid, too. I thought adulthood was about getting used to keeping it all in … but…",
						"I, um… Got assigned to a new position at my job. Drone programming. ",
						"Directly for use in the war against the Colony. ",
						"I know I'm not sending those drones or controlling. I'm just following orders. But…",
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Your work is at odds with how you want to see yourself.",
						"You didn't get to be in control."
					},
					new Godot.Collections.Array{
						406,
						407
					}
				);
				break;
			
			case 406:
				GameController.trustLevels[GameController.SOFTWARE] += 0.75f;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah.",
						"You're right.",
						"It was like... cognitive dissonance?"
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						408
					}
				);
				break;
			case 407:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...No, it's not really about control.",
						"I don't really care if I have control over my life or not.",
						"I just want to live peacefully and put things behind me."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						408
					}
				);
				break;
			case 408:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Did you know I’m a colony kid, too? Not from Colony T-68, but… it was hard where I grew up, too. ",
						"I wanted to escape from all that. ",
						"So, I got a proper job. I moved to a planet city. It was horrible, but it wasn’t a colony. ",
						"And now here I am, creating abominations to attack people that could’ve been me. ",
						"I don’t know what to do with myself. ",
						"I haven’t lost control like that in years. ",
						"…Sorry. That was a ton to drop on you. I just had to tell someone. ",

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
			
			case 500:
				GameController.trustLevels[GameController.SOFTWARE] += 1;
				GameController.engineerQuestionFlags[5] = false;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh, yeah. That's the big question, huh? ",
						"I get why you're still thinking about that. I really dropped a lot on you last time. Sorry about that. ",
						"…After that night, I took a really long look at myself. ",
						"The kid from the colonies… the kid trying to avoid living in the real world now…",
						"I've grown up with so much shame, and I've just tried to ignore it. ",
						"Truth is, all along I think I'd accepted there was just something bad inside of me. ",
						"So I didn't care what I did with myself. ",
						"But there's nothing bad inside of me. I choose my own actions. I can choose not to be a part of this. ",
						"…By New Years, I'll no longer be working for Koxhos. I already turned in my resignation. I'm not sure what I'll do with myself after that. ",
						"Maybe I'll try to reconnect with old friends. I think I'll go to the city New Year's party, too. ",

					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"Ever thought of being a volunteer?"
					},
					new Godot.Collections.Array{
						501
					}
				);
				break;
			case 501:
				GameController.trustLevels[GameController.SOFTWARE] += 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Huh? There's a shelter for colony kids out there? ",
						"…",
						"Yeah. I think I'll go there. I know what it's like, after all. ",
						"*The Engineer laughs, in spite of himself. ",
						"Wow. I forgot how it felt to look forward to something. ",
						"Screw it. I'm going home early tonight. ",
						"I'll see you at the New Years' party, yeah? "


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
			
			case 600:
				GameController.engineerQuestionFlags[6] = false;
				GameController.engineerQuestionFlags[7] = true;
				GameController.brokenPhones--;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You broke your phone? ...Sorry, man, I can't exactly just fix that.",
						"It's looking pretty trashed...",
						"Tell you what, though... I might be able to extract its memory chip.",
						"*The Engineer took the broken phone from you.",
						"No promises, but check with me again later, okay?"
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
			case 601:
				GameController.engineerQuestionFlags[7] = false;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Oh yeah -- I was able to get the memory chip out. I put your data on this USB.",
						"The Engineer hands you a USB.",
						"By the way... I didn't look at your data or anything. But I saw some of the file names, and -- I've never seen encryption like that before.",
						"You take your privacy seriously, huh? You'll have to tell me what the encryption is called someday.",
						"*...?",
						"*You plug the USB into your laptop.",
						"*...The files look encrypted. Their names are all scrambled.",
						"*But they're not actually encrypted at all...",
						"*You open a text file.",
						"*Before you pops up a manifesto about humankinds' destruction of nature.",
						"*'Humanity is a race of spoiled meat'...",
						"*In addition, there's hundreds of photos... pictures of people with animal heads pasted on top of them.",
						"..."
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

			default:
				dialogueSet = null;
				break;
		}
		return dialogueSet;
	}
}
