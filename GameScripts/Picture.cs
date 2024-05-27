using Godot;
using System;

public partial class Picture : RigidBody2D
{
	 float speed=400f;
    float angularSpeed=Mathf.Pi;
	[Export]
    bool rotating;
    //bool win;
	[Export]
	bool sliding;
    bool win;

    public override void _Ready()
    {
        win=false;
    }
    public override void _Process(double delta)
    {
        if(rotating)
            Rotation+=(float)delta*angularSpeed*2;
        if(Input.IsActionJustPressed("A_Button")||Input.IsActionJustPressed("B_Button"))
        {
            rotating=false;
            GD.Print(Rotation);
			if(Rotation<=15&&Rotation>=-15)
			{
				win=true;
			}
			else
			{
				GravityScale=3;
                win=false;
			}
        }

    }
}