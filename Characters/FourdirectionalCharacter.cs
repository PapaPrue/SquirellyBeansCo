using Godot;
using System;
using System.Threading;


public partial class FourdirectionalCharacter : Area2D
{
	[Export]
	public float speed {get;set;}=600;

	[Export]
	public bool killable;
	[Export]
	public bool isDead;

	[Signal]
	public delegate void CollisionEventHandler();

	public Vector2 ScreenSize;
	Vector2 velocity=Vector2.Zero;
    byte lastActionPressed;
	
	public override void _Ready()
	{
		ScreenSize=GetViewportRect().Size;
        
	}

	public override void _Process(double delta)
	{
	   
	   if(Input.IsActionPressed("move_left"))
		{
			
			velocity=Vector2.Left*speed;;
            lastActionPressed=1;
            
		}
		if(Input.IsActionPressed("move_right"))
		{
			
			velocity=Vector2.Right*speed;
            lastActionPressed=2;
		}
		
		if(Input.IsActionPressed("move_up"))
		{
			velocity=Vector2.Up*speed;
            lastActionPressed=3;
		}
		if(Input.IsActionPressed("move_down"))
		{
			velocity=Vector2.Down*speed;
            lastActionPressed=4;
		}
		Position += velocity * (float)delta;
	}
	private void _on_body_entered(Node2D body)
	{
		//if(body.GetNode)

	}
   
	


}






