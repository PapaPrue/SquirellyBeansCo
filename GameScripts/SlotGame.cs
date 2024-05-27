using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

public partial class SlotGame : Node2D
{
	[Export]
	Node2D[] objects;
	
	bool set;
	[Export]
	double interval;
	double elapsedTime;
	int startingPoint;
	public bool win;
	[Export]
	Node2D hardObject;
	[Export]
	Node2D correctObject;
	[Export]
	MinigameManager mgm;
	
	bool hard1;
	bool hard2;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		set=false;
		Random rand=new Random();
		startingPoint=rand.Next(0,objects.Length);
		objects[startingPoint].Visible=true;
		
		interval=.175;
		//if(hard1)
		
			//objects[objects.Length-1]=hardObject;

		
		
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!set)
		{
		elapsedTime+=delta;
		if(elapsedTime>=interval)
		{
			if(startingPoint==objects.Length-1)
			{
				objects[startingPoint].Visible=false;
				startingPoint=0;
				objects[startingPoint].Visible=true;
			}
			else
			{
				objects[startingPoint].Visible=false;
				startingPoint+=1;
				objects[startingPoint].Visible=true;

			}
			elapsedTime=0;
		}
		if(Input.IsActionJustPressed("A_Button")||Input.IsActionJustPressed("B_Button"))
		{
			set=true;
		}
		}
		if(set)
		{
		if(objects[startingPoint]==correctObject)
		{
			win=true;
			mgm.QuickReturn();
			//GD.Print("You Win");
		}
		else
		{
			win=false;
			mgm.QuickReturn();
			//GD.Print("You lose");
		}
		}

	}
}
