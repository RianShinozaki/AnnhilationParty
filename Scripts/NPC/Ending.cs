using Godot;
using System;

public partial class Ending : Speaker
{

	[Export] AnimationPlayer animPlayer;
	public override void _Ready()
    {

        base._Ready();
		GameController.theSpeaker = this;
		
		textbox_system.Instance.Initialize(0);
		
    }
    public override void _Process(double delta)
    {
		if(!initialized) {
			initialized = true;
		}
    }

	 public override DialogueSet GetNextDialogue(int id) {
		DialogueSet dialogueSet;
		
		switch(id){
			case 0:
				
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*You head to the city's center that evening.",
						"*There, you find the party in full swing.",
						"*Thousands of people have gathered here to herald in the New Year...",
						"*...Holding their hopes for the future close to their hearts.",
						"*It's nearly midnight.",
						"*On a massive screen, a timer begins to count down.",
						"*11:57... 11:58... 11:59..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{

					},
					new Godot.Collections.Array{
						(GameController.goodEnding ? 7 : 1)
					}
				);
				break;
			case 1:
				EndingAssist.actionState = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"*Midnight never arrives.",
						"...",
						"*The seed of chaos in the aberrant's heart fully blooms, enveloping the entire city.",
						"...Game over.",
						"...",
						"You could try this game over again.",
						"Or you could just retry today, if you wish."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"From the beginning.",
						"From today."
					},
					new Godot.Collections.Array{
						2,
						3
					}
				);
				break;
			case 2:
				EndingAssist.actionState = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...Frankly, I'll need you to just close the game and reopen it.",
						"Sorry."
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
			case 3:
				EndingAssist.actionState = 3;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Sure! Let's run it back.",
						"Good luck..."
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
			case 7:
				EndingAssist.actionState = 1;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"HAPPY NEW YEARS!",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						8
					}
				);
				break;
			case 8:
				EndingAssist.actionState = 2;
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The city goes wild in celebration!"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						GameController.engineerMemory[3] == 1 && GameController.occultistMemory[2] == 1 ? 9 : 10
					}
				);
				break;
			case 9:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*A little ways away, you see the Engineer and the Occultist...",
						"*The two notice each other at the same time.",
						"*The Occultist laughs. The Engineer gives a shy grin in return.",
						"*You see them move toward one another through the crowd."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						9
					}
				);
				break;
			case 10:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*...",
						"...! You feel a tap on your shoulder...",
						"...",
						"Hey, kid."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						11
					}
				);
				break;
			case 11:
				animPlayer.Play("Appear1");
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"*The old detective appears before you. She stands a head above nearly everyone else in the crowd.",
						"*She studies you, as if sizing you up.",
						"You did a good job here. The city gets to enjoy another year."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"What're you doing here?",
						"Well, Happy New Years to you, too."
					},
					new Godot.Collections.Array{
						12,
						13
					}
				);
				break;
			case 12:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Had to see it in person, of course. It'd be a spectacular show either way."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						14
					}
				);
				break;
			case 13:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Hah..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						14
					}
				);
				break;
			case 14:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Now, tell me...",
						"What do you think allows someone to become an aberrant?",
						"After all... everyone changes. Nobody is the same day by day.",
						"To live is to change, really.",
						"Aberrations are change as well. A departure from the norm.",
						"So... what do you believe separates us from an aberrant?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"A genuine hope for the future and for ourselves.",
						"The connection between our actions and our desires.",
						"Dreams that exist in reality."
					},
					new Godot.Collections.Array{
						15,
						16,
						17
					}
				);
				break;
			case 15:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"A genuine hope?",
						"Counter, I suppose, to the hopes that live in the darkest parts of ourselves.",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						18
					}
				);
				break;
			case 16:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Yes.",
						"I believe that is the most important thing.",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						18
					}
				);
				break;
			case 17:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"A dream that exists in reality...",
						"Sounds like something too good to be true.",
						"Or perhaps the point is that it's not?"
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						18
					}
				);
				break;
			case 18:
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"Humans are motivated by want.",
						"Our suspects each had a powerful desire for change.",
						"Yet, while some move towards their desired future with courage and deliberation...",
						"Others spend their years waiting for change to come to them.",
						"Not even aware, perhaps, of what they wanted.",
						"Both the Butcher and the Teacher wished for annihilation in the depths of their hearts.",
						"The Butcher, so that the world could become pure and beautiful again...",
						"And the Teacher, so that the sins and horrors of the world could be erased.",
						"That is what made them perfect candidates to become aberrants.",
						"The Occultist and the Engineer could have become aberrants too, if they hadn't learned to pick themselves up and try again.",
						"...",
						"The Butcher and the Teacher will be rehabilitated.",
						"It won't be easy.",
						"Far easier to infect someone with an aberration than it is to remove it.",
						"But, I suppose I do owe you that much."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						19
					}
				);
				break;
			case 19:
				animPlayer.Play("Appear2");
				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"You completed my game, after all.",
						"Was it fun? Did you enjoy yourself?",
						"I hope you did. I created this game for you, after all.",
						"So, what will you do next?",
						"Will you continue the way you are now, stuck in the past and waiting for something to change?",
						"Waiting for annihilation, perhaps?",
						"Or will you take control of your present and your future?",
						"Well, that's up to you.",
						"Enjoy the party for now. It's a brand new year.",
						"Annihilation has been avoided today.",
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						21
					}
				);
				break;
			case 21:
				animPlayer.Play("Disappear");
				EndingAssist.actionState = 1;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"...",
						"The old detective vanishes.",
						"Slowly, the party dies down...",
						"And a new year begins.",
						"..."
					},
					new Godot.Collections.Array{
					},
					new Godot.Collections.Array{
						"Well, that was some bullshit."
					},
					new Godot.Collections.Array{
						22
					}
				);
				break;
			case 22:
				EndingAssist.actionState = 4;

				dialogueSet = new DialogueSet(
					new Godot.Collections.Array{
						"And truly, it was."
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
