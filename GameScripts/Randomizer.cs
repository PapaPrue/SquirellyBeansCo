using Godot;
using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;

public partial class Randomizer : Node2D
{
	[Export]
	Node2D goal;
	
	[Export]
	Node2D[] objects;

	[Export]
	bool canHaveCopies;
	Random rand=new Random();
	int x;
	int[] storedNumbers;
	int currentIndex=0;

	[Export]
	bool singlularGoal;
	[Export]
	MinigameManager manager;
	[Export]
	Sprite2D goalSprite;
	[Export]
	Sprite2D[] spriteHandler;

	[Export]
	Sprite2D[] spritePositions;
	[Export]
	Sprite2D pointer;
	[Export]
	Sprite2D[] sprites;

	[Export]
	AudioStreamPlayer[] audio;


	bool gameStillGoing;
		int index=0;
		int dirNum;
	public bool win;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(manager.CopyGame)
		{
			storedNumbers=new int[spritePositions.Length];
			GetNumber();
			//DanceGame();
			//danceGame=true;
			gameStillGoing=true;
			
		}
		
		if(manager.AreaGoalGame)
		{
			int y=rand.Next(0,objects.Length);
			goal=objects[y];
			for(int i=0;i<objects.Length;i++)
			{
				if(objects[i]!=goal)
					objects[i].Visible=false;
					
			}
			spriteHandler[y].Texture=goalSprite.Texture;
			manager.goalArea=goal.GetNode<GoalObject>(goal.GetPath());
		}
		
		

	}

	public override void _Process(double delta)
	{
	 if(gameStillGoing)
	 {
		if(Input.IsActionJustPressed("move_left"))
		{
		dirNum=0;
		if(dirNum==storedNumbers[index])
		{
			audio[1].Play();
			//GD.Print("Correct");
			if(index==spritePositions.Length-1)
			{
				sprites[index].Visible=true;
				win=true;
				//danceGame=false;
				gameStillGoing=false;
			}
			else
			{
				sprites[index].Visible=true;
				index++;
				
				pointer.Position=new Vector2(spritePositions[index].Position.X,spritePositions[index].Position.Y+100);
			}
		}
		else
		{
			//GD.Print("Incorrect");
			audio[0].Play();
			gameStillGoing=false;
			
		}
		}
	  
		if(Input.IsActionJustPressed("move_right"))
		{
			
			dirNum=1;
		if(dirNum==storedNumbers[index])
		{
			audio[1].Play();
			//GD.Print("Correct");
			if(index==spritePositions.Length-1)
			{
				sprites[index].Visible=true;
				win=true;
				//danceGame=false;
				gameStillGoing=false;
			}
			else
			{
				sprites[index].Visible=true;
				index++;
				pointer.Position=new Vector2(spritePositions[index].Position.X,spritePositions[index].Position.Y+100);
			}
		}
		else
		{
			//GD.Print("Incorrect");
			audio[0].Play();
			gameStillGoing=false;
		}
		}
		if(Input.IsActionJustPressed("move_up"))
		{
			dirNum=2;
			if(dirNum==storedNumbers[index])
			{
				audio[1].Play();
				//GD.Print("Correct");
			if(index==spritePositions.Length-1)
			{
				sprites[index].Visible=true;
				win=true;
				//danceGame=false;
				gameStillGoing=false;
			}
			else
			{
				sprites[index].Visible=true;
				index++;
				pointer.Position=new Vector2(spritePositions[index].Position.X,spritePositions[index].Position.Y+100);
			}
			
			}
			else
			{
				//GD.Print("Incorrect");
				audio[0].Play();
				gameStillGoing=false;
			}
		}
		if(Input.IsActionJustPressed("move_down"))
		{
			dirNum=3;
			if(dirNum==storedNumbers[index])
		{
			audio[1].Play();
			//GD.Print("Correct");
			if(index==spritePositions.Length-1)
			{
				sprites[index].Visible=true;
				win=true;
				//danceGame=false;
				gameStillGoing=false;
			}
			else
			{
				sprites[index].Visible=true;
				index++;
				pointer.Position=new Vector2(spritePositions[index].Position.X,spritePositions[index].Position.Y+100);
			}
			
		}
		else
		{
			//GD.Print("Incorrect");
			audio[0].Play();
			gameStillGoing=false;
		}
		}
		
		
	 	}
	  }
		


	void GetNumber()
	{
		//GD.Print(storedNumbers.Length);
		for(int i=0;i<storedNumbers.Length;i++)
		{
			x=rand.Next(0,4);
			storedNumbers[i]=x;
			spritePositions[i].Texture=spriteHandler[x].Texture;
			//GD.Print(storedNumbers[i]);
		
		}
		//GD.Print(storedNumbers)
		
		
		//return;
	}
	
	
}
