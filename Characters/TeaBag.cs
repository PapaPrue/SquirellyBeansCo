using Godot;
using System;


public partial class TeaBag : RigidBody2D
{
	public Vector2 ScreenSize;
	Vector2 droppedPosition;
	Vector2 velocity=Vector2.Zero;
	[Export]
	public float speed;
	bool dropped=false;
	bool dropping;
    public override void _Ready()
    {
       ScreenSize=GetViewportRect().Size;
	   
	   
	   
    }
    public override void _Process(double delta)
    {
       if(!dropped)
		{
		Position = new Vector2(x: Mathf.Clamp(Position.X, 0, ScreenSize.X+10), y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
		//velocity.X+=1;
		GlobalPosition += velocity * (float)delta;
		velocity.X=speed;
		if(Position.X>ScreenSize.X+6)
		{
				Position=new Vector2(-10,Position.Y);

		}
		droppedPosition=Position;
		}

		if(Input.IsActionPressed("A_Button")||Input.IsActionPressed("B_Button"))
		{
			Position=droppedPosition;
			
			dropped=true;
			
			
		}
		
		if(dropped)
		{
			if(!dropping)
			{
			//GD.Print(droppedPosition);
			GlobalPosition=droppedPosition;
			dropping=true;
			}
			else
				GravityScale=2;
		
		
		}
		
		
		
		
		
		


    }
}
