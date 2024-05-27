using Godot;
using System;

public partial class PlatformerCharacter : AnimatableBody2D
{
	
	float speed=600;
	Vector2 velocity=Vector2.Zero;
	float direction=0;
	public override void _Ready()
	{
		
	}
	public override void _Process(double delta)
	{
	   
		if(Input.IsActionPressed("ui_left"))
		{
			direction=-1;
			velocity=Vector2.Left*speed;
		}
		if(Input.IsActionPressed("ui_right"))
		{
			direction=1;
			velocity=Vector2.Right*speed;
		}
		
		if(Input.IsActionPressed("ui_up"))
		{
			direction=1;
			velocity=Vector2.Up*speed;
		}
		if(Input.IsActionJustPressed("ui_down"))
		{
			direction=-1;
			velocity=Vector2.Down*speed;
		}
		
		
	}
	
}
