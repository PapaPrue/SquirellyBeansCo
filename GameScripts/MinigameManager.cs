using Godot;
using System;
using System.Linq;

public partial class MinigameManager : Node2D
{
	
	[Export]
	CollectibleGoal[] collectibles;
	[Export]
	CharacterBody2D[] characters;
	[Export]
	public GoalObject goalArea;
	[Export]
	public Node2D objectOfInterest;
	[Export]
	public RigidBody2D[] goalObjects;
	[Export]
	DodgeGame dodgeHandler;
	[Export]
	ReactionGame reactionHandler;

	[Export]
	Randomizer copyHandler;

	[Export]
	SlotGame slotHandler;
	[Export]
	RotateHandler rotateHandler;



	[Export]
	bool collectingMinigame;
	[Export]
	public bool AreaGoalGame;
	[Export]
	bool RoationGame;
	[Export]
	bool ReactionBasedGame;

	[Export]
	public bool CopyGame;
	[Export]
	public bool DodgeGame;
	[Export]
	public bool SlotGame;

	[Export]
	public bool rotateGame;
	[Export]
	public Masher mashHandler;
	[Export]
	public bool MashGame;


	[Export]
	public float timer;
	public float leeway=.5f; 
	[Export]
	public sceneloader MainGameNode;

	public bool hardMode1;
	public bool hardMode2;

	[Export]
	RichTextLabel timerText;

	public override void _Ready()
	{
		timerText=GetNode<RichTextLabel>("RichTextLabel");
		StartGame();
	}


double elapsedTime;
	public async void StartGame()
	{
		elapsedTime=0;

		//GD.Print(timer);
		for(int i=0;i<collectibles.Length;i++)
		{
			collectibles[i].reset();
		}
	   
			await ToSignal(GetTree().CreateTimer(leeway),SceneTreeTimer.SignalName.Timeout);
			//Timer(timer);
			timerStart=true;

				
		


	}
	bool timerStart=false;
	bool conditionCheck=false;
	public async void Timer(float duration)
	{
		//duration=timer;
		await ToSignal(GetTree().CreateTimer(timer),SceneTreeTimer.SignalName.Timeout);
		Engine.TimeScale=0;
		Condition();



	}
	
	public override void _Process(double delta)
	{
		if(timerStart)
		{
			elapsedTime+=delta*1.3;
			timerText.Text=((int)(timer-elapsedTime)).ToString();
			if(elapsedTime>timer)
			{
				if(conditionCheck==false)
				{
					Condition();
				}
			}
		}
	   
	}

	[Export]
	public bool win;
	public void Condition()
	{
		conditionCheck=true;
		if(collectingMinigame)
		{
			for(int i=0;i<collectibles.Length;i++)
			{
				if(collectibles[i].collected==false)
				{
					
					win= false;
					ReturnToHub();
					return;

				}
			}
			
			win= true;
			ReturnToHub();
			return;
		}
		if(AreaGoalGame)
		{
			
			if(goalArea.win)
			{
				win=true;
				
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				
				ReturnToHub();
				return;
			}

			//if(objectOfInterest.IsInsideTree())
		}
		if(DodgeGame)
		{
			
			if(dodgeHandler.win)
			{
				win=true;
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				
				ReturnToHub();
				return;
			}
		}
		if(ReactionBasedGame)
		{
			if(reactionHandler.win)
			{
				win=true;
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				ReturnToHub();
				return;
			}
		}
		if(CopyGame)
		{
			if(copyHandler.win)
			
			{
				win=true;
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				ReturnToHub();
				return;
			}
			
		}
		if(SlotGame)
		{
			if(slotHandler.win)
			
			{
				win=true;
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				ReturnToHub();
				return;
			}
				
		}
		if(rotateGame)
		{
			if(rotateHandler.win)
			
			{
				win=true;
				ReturnToHub();
				return;
			}
			else
			{
				win=false;
				ReturnToHub();
				return;
			}	
		}
	if(MashGame)
	{
		if(mashHandler.win)
		{
			win=true;
			ReturnToHub();
			return;
		}
		else
		{
			win=false;
			ReturnToHub();
			return;
		}
	}
	}

	public async void QuickReturn()
	{
		await ToSignal(GetTree().CreateTimer(1),SceneTreeTimer.SignalName.Timeout);
		Condition();
		//ReturnToHub();

	}
	
	void ReturnToHub()
	{
	//	hub.unloadScene();
		if(MainGameNode!=null)
		{
			MainGameNode.manager=this;
			MainGameNode.GameCheck();
		}
		else
			GD.Print("It's Null!");
			
		
	}
	


}
