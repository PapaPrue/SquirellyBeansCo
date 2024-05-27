using Godot;
using System;
using System.Collections;

public partial class mini_game_hub : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	Node2D[] miniNodes;
	[Export]
	bool CozyMode;
	[Export]
	byte lives=3;
	
	[Export]
	MinigameManager[] minigames;
	Random rand= new Random();
	Queue miniQueue;
	int number;
	int prevNumber;
	
	public override void _Ready()
	{
		for(int i=0;i<miniNodes.Length;i++)
		{
			miniNodes[i].SetProcess(false);
			miniNodes[i].SetPhysicsProcess(false);
			miniNodes[i].SetProcessInput(false);
		}
		number = rand.Next(0,minigames.Length);
		LoadMinigame(number);
		
			
	}
	public async void returnFromMinigame()
	{
		Engine.TimeScale=1;
		
		miniNodes[number].Visible=false;
		GetNode<Sprite2D>("Pizza").Visible=true;
		if(minigames[number].win==true)
		{
			GD.Print("You Win!");
			await ToSignal(GetTree().CreateTimer(2),SceneTreeTimer.SignalName.Timeout);
			getNumber();
		}
		else
		{
			GD.Print("You Lose");
			if(CozyMode)
			{
				await ToSignal(GetTree().CreateTimer(2),SceneTreeTimer.SignalName.Timeout);
				getNumber();
				
			}
			else
			{
				lives--;
				if(lives==0)
				{
					Engine.TimeScale=0;
					GD.Print("Game Over!");
				}
				else
				{
					await ToSignal(GetTree().CreateTimer(2),SceneTreeTimer.SignalName.Timeout);
					getNumber();
				}

			}



		}

	}
	void getNumber()
	{
		for(int i=0;i<miniNodes.Length;i++)
		{
			miniNodes[i].SetProcess(false);
			miniNodes[i].SetPhysicsProcess(false);
			miniNodes[i].SetProcessInput(false);
			//miniNodes[i].GetNode(CollisionShape2D)
		}
		number=rand.Next(0,minigames.Length);
		//if(number==prevNumber)
		//	getNumber();
		LoadMinigame(number);
	}

	public void LoadMinigame(int number)
	{
		//prevNumber = number;
		GetNode<Sprite2D>("Pizza").Visible=false;
		miniNodes[number].SetProcess(true);
		miniNodes[number].SetPhysicsProcess(true);
		miniNodes[number].SetProcessInput(true);
		miniNodes[number].Visible=true;
		//minigames[number];
		minigames[number].StartGame();

	}

	
}
