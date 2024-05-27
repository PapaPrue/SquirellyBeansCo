using Godot;
using System;
using System.Reflection.Metadata;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float Speed = 550.0f;
	[Export]
	public float JumpVelocity;
	//bool grounded;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	[Export]
	public float gravity =2000;
	[Export]
	bool playerControlX;
	[Export]
	bool canJump;
	
	[Export]
	public bool autoRun;
	
	[Export]
	bool autojump;
	
	[Export]
	bool floaty;
	float maxVelocityY;
	AnimationPlayer anim;
	Vector2 screenClamp;
	[Export]
	bool footballer;

	public override void _Ready()
	{
		anim=GetNode<AnimationPlayer>("AnimationPlayer");
		screenClamp=GetViewportRect().Size;
	}
	public override void _PhysicsProcess(double delta)
	{
		if(!footballer)
			Position = new Vector2(x: Mathf.Clamp(Position.X, 0, screenClamp.X+10), Position.Y);
		Vector2 velocity = Velocity;
		
		
		// Add the gravity.
		if (!IsOnFloor())
		{
			//GD.Print("Is on floor");
			velocity.Y += gravity * (float)delta;
		}
		if(IsOnFloor())
		{
			if(floaty)
				playerControlX=false;

		}

		// Handle Jump.
		if (Input.IsActionJustPressed("A_Button") && IsOnFloor()||IsOnFloor()&&autojump)
		{
			velocity.Y = JumpVelocity;
			anim.Play("Jump");
			//GD.Print("Jump");
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if(playerControlX)
		{
		if (direction != Vector2.Zero)
		{
			anim.Play("Run");
			velocity.X = direction.X * Speed;
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		
		//MoveAndSlide();
		}
		if(autoRun)
		{
			
			velocity.X=1*Speed;
			anim.Play("Run");
			if(velocity.Y!=0)
				anim.Play("Jump");

		if (Input.IsActionJustPressed("A_Button") && IsOnFloor()||IsOnFloor()&&autojump)
		{
			velocity.Y = JumpVelocity;
			//anim.Play("Jump");
			//GD.Print("Jump");
		}
			MoveAndSlide();
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	
}
