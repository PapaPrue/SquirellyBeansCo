using Godot;
using System;

public partial class Masher : Node2D
{
	[Export]
	public int minimumMash;
	public int buttonPressed;
	public bool win;

	public bool hard1;
	public bool hard2;
	[Export]
	Sprite2D sprite;
	public override void _Ready()
	{
		if(hard1)
		{
			minimumMash=(int)(minimumMash*1.3);
			hard2=false;
		}
		if(hard2)
		{
			hard1=false;
			minimumMash=(int)(minimumMash*1.6);
		}
		win=false;
		buttonPressed=0;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("A_Button")||Input.IsActionJustPressed("B_Button"))
		{
			buttonPressed++;
			sprite.Position=new Vector2(sprite.Position.X-5,sprite.Position.Y);
		}
		if(buttonPressed>=minimumMash)
		{
			sprite.Position=new Vector2(sprite.Position.X-20,sprite.Position.Y-10);
			sprite.Rotation-=(float)delta*3;
			win=true;
		}
	}
}
