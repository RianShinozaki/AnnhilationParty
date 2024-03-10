using Godot;
using System;

public partial class Engineer : Speaker
{
	[Export] public Sprite2D NPCSprite;
	[Export] public AnimationPlayer animPlayer;
	[Export] public Item steak;
	public override void _Ready()
    {

        base._Ready();
		GameController.theSpeaker = this;
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
				if(GameController.engineerMemory[1] == 0) {
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
								"*He's staring at his work intently... the whole world might as well not exist.",
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
								"*He's staring at his work intently... the whole world might as well not exist.",
								"*His laptop is covered in stickers...",
								"*That's right. You know about the Engineer's watch history... that character is from the anime 'Auta'"
							},
							new Godot.Collections.Array{

							},
							new Godot.Collections.Array{
								"Hey, is that an 'Auta' sticker?"
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
							"Better go home."
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
						"*The name of his favorite show grabs his attention. He looks around, and catches your gaze.",
						"...",
						"Oh -- you're talking to me?",
						"Thanks. I love this show. Not a lot of people would recognize it, though..."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"I just started watching it.",
						"I watch it all the time."
						
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
						"*The Engineer looks a little helpless, trying to think of what to say."
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
						"Well, I think she's really cool. For a lot of reasons."
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
						"Well, I think she's really cool. For a lot of reasons."
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
						"*The Engineer laughs, awkwardly."
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						"You come here often?",
						"Is that work?",
						"It's been nice to meet you."
					},
					new Godot.Collections.Array{
						6,
						7,
						13
					}
				);
				break;
			
			case 6:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"A lot, yeah. More than I should. Just helps me get out of the house.",
						"Sort of makes me feel like a character in one of those old neo-noirs, too...",
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
							"Yep. I'm just a software engineer... it's nothing too interesting.",
							"I work for Koxhos Corp. The, uh... militia company.",
							"...",
							"I don't usually mean to tell people that.",
							"It's nothing special, really. I just help install tools and maintain systems.",
							"What do you do?"
						},
						new Godot.Collections.Array{

						},
						new Godot.Collections.Array{
							"I'm a writer.",
							"I'm unemployed.",
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
							"Yep. I'm just a software engineer... it's nothing too interesting.",
							"I work for Koxhos Corp. The, uh... militia company.",
							"...",
							"I don't usually mean to tell people that.",
							"It's nothing special, really. I just help install tools and maintain systems.",
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
						"If you made it, let me know, though."
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
			case 10:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Must be really hard work.",
						"I used to want to be a writer too. I read so many comics when I was a kid...",
						"Then, I sat down in front of a piece of paper and realized the unfortunate truth.",
						"I have no idea what to write..."
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
						5
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
						"I still can't believe they passed that..."
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
			case 13:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yeah, it's totally been a pleasure.",
						"Hope I see you around here again.",
						"*The Engineer returns to his work.",
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
				animPlayer.Play("Intro");
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You say hello to the Engineer. Unfortunately, no more dialogue exists. Go home!"
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
