using Godot;
using System;

public partial class MultipleObjects : RigidBody2D
{
	 float speed=400f;
    float angularSpeed=Mathf.Pi;
	[Export]
    bool rotating;
    //bool win;
	[Export]
	bool sliding;
    public bool win;
	[Export]
	bool PictureRotateGame;
	

    [Export]
    MinigameManager mgm;
    public bool set=false;
    public bool canMove=false;



    public override void _Ready()
    {
        
    }
    public void Reset()
    {
        canMove=true;
        set=false;
        //win=false;
        
    }
    double x;
    public override void _Process(double delta)
    {
        x+=delta;
        if(canMove)
        {
        //set=false;
        if(rotating)
            Rotation+=(float)delta*angularSpeed;
            //set=false;
          GD.Print(Rotation);  
        if(Input.IsActionJustPressed("A_Button")||Input.IsActionJustPressed("B_Button"))
        {
            
            Set();
            return;
        }
    }
    }
    void Set()
    {
        if(canMove)
        {
         rotating=false;
         set=true;
         GD.Print("Set");
		 if(PictureRotateGame)
		    {
		        if(GlobalRotation<=15&&Rotation>=-15)
		        {
                //GD.Print(Rotation);
			     win=true;
		        }
			    else
			    {
                   // GD.Print(Rotation);
				    GravityScale=3;
                    win=false;
			    }
		    }
        canMove=false;
        }
       //mgm.
    }
         
 }

