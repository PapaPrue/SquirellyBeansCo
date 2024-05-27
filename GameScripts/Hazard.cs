using Godot;
using System;

public partial class Hazard : Node
{
	// Called when the node enters the scene tree for the first time.
	public bool isHit;
	//[Export]
	CharacterBody2D player;

	[Export]
	bool canJump;
	[Export]
	bool animates;
	AnimationPlayer anim;
	[Export]
	DodgeGame dodgeGame;
	
	public override void _Ready()
	{
		if(animates)
		{
			anim=GetNode<AnimationPlayer>("AnimationPlayer");
			anim.Play("default");
		}
	}

	void _on_body_entered (CharacterBody2D body)
	{
		if(body.GetType()==typeof(CharacterBody2D))
		{
			isHit=true;
			player=body;
			dodgeGame.gameOver=true;
			dodgeGame.Death();
			//EmitSignal("Hit");
			//player.GetNode<Camera2D>("Camera2D").Position=player.Position;
			//player.GetNode<Camera2D>("Camera2D").Enabled=false;
		}
	}
	
	Vector2 velocity;
    public override void _Process(double delta)
    {
      
    }
}
