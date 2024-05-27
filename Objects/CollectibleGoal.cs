using Godot;
using System;

public partial class CollectibleGoal : Area2D
{
	public bool collected;
	[Signal]
	public delegate void CollectEventHandler();
	[Export]
	bool harvestables;

	[Export]
	bool football;
	[Export]
	bool laser;
	[Export]
	bool floater;
	[Export]
	bool dud;
	[Export]
	bool traveling;
	[Export]
	int travelSpeed;
	Vector2 velocity;
	bool entered;
	bool incompletePass;
	[Export]
	bool randomThrow;
	[Export]
	Sprite2D alertSprite;

	[Export]
	AudioStreamPlayer audio;


    public override void _Ready()
    {
		reset();
        //base._Ready();
    }
    public void reset()
	{
		Show();
		collected=false;
		if(football)
		{
			timeToThrow();
			

		
		}
	}
	public async void timeToThrow()
	{
		alertSprite.Visible=true;
		await ToSignal(GetTree().CreateTimer(1.2f),SceneTreeTimer.SignalName.Timeout);
		alertSprite.Visible=false;
		if(randomThrow)
			{
				Random rand=new Random();
				int x= rand.Next(0,3);
				if(x==0)
					floater=true;
				else if(x==1)
					laser=true;
				else if(x==2)
					dud=true;
			}
			else
			floater=true;
			traveling=true;
	}
	public void _on_body_entered(Node2D node)
	{
		if(node.GetType()!=typeof(CharacterBody2D)&&node.GetType()!=typeof(FourDirectionChar))
		{
			//GD.Print("Incorrect Type" + node.GetType());
			if(football)
			{
				if(node.GetType()==typeof(StaticBody2D))
				{
					traveling=false;
					incompletePass=true;
				}
				else
					return;
				
			}
			return;
			
		}
		else
		{
		if(!harvestables)
		{
			if(!football)
			{
			//GD.Print("Collected");
			collected=true;
			audio.Play();
			Hide();
			}
			else
			{
				if(!incompletePass)
				{
				traveling=false;
				collected=true;
				audio.Play();
				Hide();
				//node.Position=this.Position;
				//Engine.TimeScale=0;
				}
			//Hide();	
			}
		}
		else
		{
			entered=true;
			GD.Print("Harvest");
			
		}
		}
		
		
	}
	public void _on_body_exited(Node2D node)
	{
		if(node.GetType()==typeof(CharacterBody2D))
		{
			entered=false;
		}
	}

    public override void _Process(double delta)
    {
		
		if(entered)
		{
       if(Input.IsActionJustPressed("B_Button"))
			{
				GD.Print("Collected");
				collected=true;
				Hide();
				//PlatformerCharacter p;
				
			}
			
		}
		else if(football&&traveling)
		{
			if(laser)
			{
				Position=new Vector2(Position.X-(float)delta*travelSpeed*1.7f,Position.Y);
			}
			else if (floater)
			{
				Position=new Vector2(Position.X-(float)delta*travelSpeed,Position.Y);
			}
			else if(dud)
			{
				Position=new Vector2(Position.X-(float)delta*travelSpeed*1.2f,Position.Y+(float)delta*travelSpeed/3);
			}
		}
    }
}
