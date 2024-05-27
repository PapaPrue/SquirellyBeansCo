using Godot;
using System;

public partial class FourDirectionChar : Godot.CharacterBody2D
{
	[Export]
	public const float Speed = 700.0f;
	public const float JumpVelocity = -400.0f;
	Sprite2D sprite;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = 0;
	Vector2 screenClamp;
	AnimationPlayer anim;
	public override void _Ready()
	{
		anim=GetNode<AnimationPlayer>("AnimationPlayer");
		
	   screenClamp=GetViewportRect().Size;
	}

	public override void _Process(double delta)
	{
		Position = new Vector2(x: Mathf.Clamp(Position.X, 0, screenClamp.X+10), y: Mathf.Clamp(Position.Y, 0, screenClamp.Y));
		Vector2 velocity=Velocity;
	   

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			if(Input.IsActionJustPressed("move_left")||Input.IsActionJustPressed("move_right"))
			{
				

				if(anim.CurrentAnimation!="walk")
					anim.Play("walk");	
			}
			if(Input.IsActionJustPressed("move_up"))
			{
				if(anim.CurrentAnimation!="walkUp")
					anim.Play("walkUp");
			}
			if(Input.IsActionJustPressed("move_down"))
			{
				if(anim.CurrentAnimation!="walkDown")
					anim.Play("walkDown");
			}


			
			velocity.X = direction.X * Speed;
			velocity.Y=direction.Y*Speed;
			
		}
		else
		{
			anim.Stop();
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		

		Velocity = velocity;
		MoveAndSlide();
	}
}
