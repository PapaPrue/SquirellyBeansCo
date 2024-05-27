using Godot;
using System;

public partial class DodgeGame : Node2D
{

	[Export]
	Hazard[] hazardHandler;

	[Export]
	Camera2D camera;
	[Export]
	CharacterBody2D player;

	[Export]
	bool footballGame;
	[Export]
	MinigameManager mgm;
Vector2 velocity;
	public bool gameOver;
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void HitEventHandler();
	public bool win;

	public override void _Ready()
	{
	  camera=GetNode<Camera2D>("Camera2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!gameOver)
		{
			camera.Position=player.Position;
			win=true;
		}
		else
		{
			player.autoRun=false;
			player.Velocity=velocity;
			if(footballGame)
			{
			velocity.Y-=((float)delta*50)+25;
			velocity.X-=((float)delta*100)+25f;
			player.Rotation-=(float)delta*50;
			
			player.gravity=0;
			win=false;
			}
			else
				player.Velocity=Vector2.Zero;
				win=false;
				
		}
	}
	public async void Death()
	{
		//await ToSignal(GetTree().CreateTimer(1),SceneTreeTimer.SignalName.Timeout);
		mgm.QuickReturn();
	}
}
