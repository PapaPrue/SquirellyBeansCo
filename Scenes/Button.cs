using Godot;
using System;

public partial class Button : Godot.Button
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	String sceneToLoad;
	PackedScene loader;
	[Export]
	bool CozyMode;
	[Export]
	Node2D[] nodesToDelete;
	[Export]
	CanvasItem[] canvastodelete;

	Node loaderInstance;

	[Export]
	Title title;
	[Export]
	bool quit;

	public void _on_pressed()
	{
		
		loader=GD.Load<PackedScene>(sceneToLoad);
		loaderInstance=loader.Instantiate();
		if(quit)
			GetTree().Quit();
		else if(CozyMode)
			title.LoadMode(0,loaderInstance,loader);
		else
		{
			title.LoadMode(1,loaderInstance,loader);
		}

			//loaderInstance.GetNode<Intro>("../CozyOpener").CozyMode=true;
			//GetParent().AddChild(loaderInstance);
		//GetParent().QueueFree();
		
	}
}
