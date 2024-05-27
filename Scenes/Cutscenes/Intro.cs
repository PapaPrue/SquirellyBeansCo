using Godot;
using System;

public partial class Intro : Node2D
{
	[Export]
	String sceneloaderPath;

	[Export]
	Camera2D cam;

	[Export]
	Node2D zoomInPoint;

	Node loaderInstance;
	PackedScene loader;
	[Export]
	Node[] NodesToBeInvisible;
	[Export]
	RichTextLabel[] textToBeVisible;
	[Export]
	public bool CozyMode;

	public string rootCutscene; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i=0;i<textToBeVisible.Length;i++)
			textToBeVisible[i].Visible=false;
		intro();
	}

	async void intro()
	{
		await ToSignal(GetTree().CreateTimer(1),SceneTreeTimer.SignalName.Timeout);
		for(int i=0;i<textToBeVisible.Length;i++)
			textToBeVisible[i].Visible=true;
		await ToSignal(GetTree().CreateTimer(2.5),SceneTreeTimer.SignalName.Timeout);
		cam.Position=zoomInPoint.Position;
		cam.Zoom=new Vector2(1,1);
		for(int i=0;i<7;i++)
		{
		cam.Zoom=new Vector2(cam.Zoom.X+1f,cam.Zoom.Y+1f);
		}

		await ToSignal(GetTree().CreateTimer(1.5),SceneTreeTimer.SignalName.Timeout);
		loader=GD.Load<PackedScene>(sceneloaderPath);
		loaderInstance=loader.Instantiate();
		for(int i=0;i<NodesToBeInvisible.Length;i++)
		{
			RemoveChild(NodesToBeInvisible[i]);
			
		}
		for(int i=0;i<textToBeVisible.Length;i++)
			RemoveChild(textToBeVisible[i]);
		//if(CozyMode)
		//{
			//loaderInstance.GetNode<sceneloader>("MiniGameHub").CozyMode=true;
		//}
		//else
			//loaderInstance.GetNode<sceneloader>("MiniGameHub").CozyMode=false;
		AddChild(loaderInstance);
	}
}
