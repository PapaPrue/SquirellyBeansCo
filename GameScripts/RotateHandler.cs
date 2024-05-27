using Godot;
using System;

public partial class RotateHandler : Node2D
{

	public bool win;
	[Export]
	Rotator rotator;

    public override void _Process(double delta)
    {
       win=rotator.win;
    }
}
