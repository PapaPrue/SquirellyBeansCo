using Godot;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public partial class sceneloader : Node2D
{
	StringBuilder SB= new StringBuilder("ABC",75);
	// Called when the node enters the scene tree for the first time.
	[Export]
	String[] pathNames;
	[Export]
	String[] shortNames;
	[Export]
	String[] hardModePaths;
	[Export]
	//String[] status;

	int index;
	int prevIndex;
	Node loaderInstance;
	[Export]
	public bool CozyMode;
	[Export]
	byte lives=3;
	[Export]
	public MinigameManager manager;
	float transitionTime=1.5f;
	int gamesPlayed;
	bool speedUp2x;
	bool speedUp3x;

	[Export]
	AudioStreamPlayer[] audios;

	bool hardMode;
	//bool hardMode2;
	[Export]
	RichTextLabel status;
	[Export]
	RichTextLabel lifeCounter;
	public override void _Ready()
	{
		gamesPlayed=1;
		speedUp2x=false;
		speedUp3x=false;
		shortNames=new string[pathNames.Length];
		for(int i=0;i<pathNames.Length;i++)
		{
			shortNames[i]=pathNames[i].Replace("res://Scenes/MiniGames/Level1/","");
			shortNames[i]=shortNames[i].Replace(".tscn","");
			//GD.Print(shortNames[i]);

		}
		LoadRandomScene();
	}

	async void LoadRandomScene()
	{	
		//int x=1;
		//if(hardMode)
		//	x=2;
		Random rand=new Random();
		index=rand.Next(0,pathNames.Length);
		//GD.Print(pathNames.Length);
		if(index==prevIndex)
		{
			LoadRandomScene();
			return;
		}
		GetNode<RichTextLabel>("../MiniGameHub/Canvas/MiniGameName").Text = shortNames[index];
		GetNode<RichTextLabel>("../MiniGameHub/Canvas/LevelNumber").Text = gamesPlayed.ToString();
		await ToSignal(GetTree().CreateTimer(1.5),SceneTreeTimer.SignalName.Timeout);
		GetNode<Control>("Canvas").Visible=false;

		prevIndex=index;
		PackedScene loader;
		if(!hardMode)
		{
		loader=GD.Load<PackedScene>(pathNames[index]);
		}
		else
		{
		loader=GD.Load<PackedScene>(hardModePaths[index]);
		}
		loaderInstance= loader.Instantiate();
		
		AddChild(loaderInstance);
		loaderInstance.GetNode<MinigameManager>("../" + shortNames[index]).MainGameNode=this;
		if(speedUp2x)
		{
			if(!hardMode)
			{
			loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).timer=4f;
			loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).leeway=.3f;
			//loaderInstance.GetNode<MinigameManager>(pathNames[index]).hardMode1=true;
			//GD.Print(loaderInstance.GetNode<MinigameManager>(pathNames[index]).timer);
			if(speedUp3x)
			{
				
				loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).timer*=3f;
				loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).leeway*=.2f;
				//loaderInstance.GetNode<MinigameManager>(pathNames[index]).hardMode2=true;
			}
			}
			else
			{
			loaderInstance.GetNode<MinigameManager>("../"+ shortNames[index]).timer=3.75f;
			if(shortNames[index].Equals("Maze"))
				loaderInstance.GetNode<MinigameManager>("../"+ shortNames[index]).timer=7;
			loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).leeway=2f;
			//loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).hardMode1=true;
			if(speedUp3x)
			{
				
				loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).timer=2.85f;
				if(shortNames[index].Equals("Maze"))
				loaderInstance.GetNode<MinigameManager>("../"+ shortNames[index]).timer=5.55f;
				loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).leeway=.7f;
				//loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).hardMode2=true;
			}
			}
			//manager=loaderInstance.GetNode<MinigameManager>(pathNames[index]);
		loaderInstance.GetNode<MinigameManager>("../"+shortNames[index]).MainGameNode=this;
		
			manager=GetNode<MinigameManager>(shortNames[index]);
		
		}
		
	}
   public void GameCheck()
   {
	
	//await ToSignal(GetTree().CreateTimer(loaderInstance.GetNode<MinigameManager>("/root/MiniGameHub").timer),SceneTreeTimer.SignalName.Timeout);
	if(manager.win)
	{

		//GD.Print("Good Job!");
		audios[1].Play();
		UnloadScene();
	}	
	else
	{
		//GD.Print("Oh no...");
		audios[2].Play();
		if(!CozyMode)
			lives--;
		UnloadScene();

	}
   }

	public async void UnloadScene()
	{
		loaderInstance.QueueFree();
		gamesPlayed++;
		GetNode<Control>("Canvas").Visible=true;
		GetNode<RichTextLabel>("../MiniGameHub/Canvas/MiniGameName").Text = "";
		Engine.TimeScale=1;
		if(lives<=0)
		{
			GameOver();
			return;
		}
		await ToSignal(GetTree().CreateTimer(transitionTime),SceneTreeTimer.SignalName.Timeout);
		if(!CozyMode)
		{
		switch(gamesPlayed){
		case 7:
		{
			speedUp2x=true;
			status.Text="SPEED UP!";
			break;
			//GD.Print("SPEED UP!");
		}
		case 13:
		{
			hardMode=true;
			status.Text="HARDMODE ACTIVATED";
			break;
			
			//GD.Print("SPEED UP!!!");
		}
		case 20:
		{
			speedUp3x=true;
			status.Text="SPEED UP!";
			break;

		}
		default:
	
		status.Text="";
		break;
		}
		lifeCounter.Text= "Lives: " + lives;
		}
		else
		{
			lifeCounter.Text= "Cozy Mode";
		}
		
		audios[0].Play();
		LoadRandomScene();

	}
	async void GameOver()
	{
		status.Text="GAME OVER! Score: " + gamesPlayed;
		await ToSignal(GetTree().CreateTimer(3f),SceneTreeTimer.SignalName.Timeout);
		GetTree().ReloadCurrentScene();

	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("pause"))
			GetTree().ReloadCurrentScene();
	}
}
