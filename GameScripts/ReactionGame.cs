using Godot;
using Godot.NativeInterop;
using System;

public partial class ReactionGame : Node2D
{
	double AlertInterval;
	[Export]
	float timeToReact;
	public bool win;
	double elapsedTime;
	bool gameStillGoing;
	[Export]
	Sprite2D alertSprite;

	[Export]
	Sprite2D startingSprite;
	[Export]
	Sprite2D reactSprite;
	[Export]
	Sprite2D[] spriteHandler;
	[Export]
	AudioStreamPlayer reactionSound;
	[Export] 
	AudioStreamPlayer anticipationSound;
	public override void _Ready()
	{
		elapsedTime=0;
		win=false;
		//timeToReact=.6f;
		Random rand=new Random();
		AlertInterval = rand.NextDouble() * (2.2-0.85)+.85;
		//GD.Print(AlertInterval);
		alertSprite.Visible=false;
		gameStillGoing=true;
		//Suspense();
	}
bool sound=false;
    public override void _Process(double delta)
    {
	 if(gameStillGoing)
	 {  
		elapsedTime+=delta;
		//GD.Print(elapsedTime);
	if(Input.IsActionPressed("A_Button")||Input.IsActionPressed("B_Button"))
	{
		startingSprite.Texture=reactSprite.Texture;
	}

	 if(elapsedTime<AlertInterval)
	 {
		
		
			if(Input.IsActionPressed("A_Button")||Input.IsActionPressed("B_Button"))
			{
				//GD.Print("too early");
				spriteHandler[0].Visible=true;
				win=false;
				gameStillGoing=false;
			}
	 }
	 if(elapsedTime>=AlertInterval&&elapsedTime<=AlertInterval+timeToReact)
	 {
		anticipationSound.Stop();
		alertSprite.Visible=true;
		if(!sound)
		reactionSound.Play();
		sound=true;
		//reactionSound.pla
		if(Input.IsActionPressed("A_Button")||Input.IsActionPressed("B_Button"))
		{
			reactionSound.Stop();
			//GD.Print("On Time");
			spriteHandler[1].Visible=true;
			win=true;
			gameStillGoing=false;
		}
	 }
	 if(elapsedTime>AlertInterval+timeToReact)
	 {
		reactionSound.Stop();
		alertSprite.Visible=false;
		spriteHandler[2].Visible=true;
		win=false;
		gameStillGoing=false;
		if(Input.IsActionPressed("A_Button")||Input.IsActionPressed("B_Button"))
		{

			//GD.Print("Too Late");
			
		}
	 }

	 }
    }
	
	
}
