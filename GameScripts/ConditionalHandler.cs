using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

public partial class ConditionalHandler : Node2D
{
		[Export]
		MultipleObjects[] conditionals;
		public bool win;

	public override void _Ready()
	{
	   OneByOne();
	}
	public void OneByOne()
	{
		for(int i=0;i<conditionals.Length;i++)
		{
			if(!conditionals[i].set)
			{
				//conditionals[i].canMove=true;
				conditionals[i].Reset();
				GD.Print(i);
				return;
			}
		}
		//CheckWin();
	}
	

	void CheckWin()
	{
		for(int i=0;i<conditionals.Length;i++)
		{
			if(!conditionals[i].win)
			{
				win=false;
				GD.Print("No Good");
				return;
			}

		}
		win=true;
	}


}
