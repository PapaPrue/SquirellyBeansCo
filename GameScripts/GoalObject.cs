using Godot;
using System;

public partial class GoalObject : Area2D
{
    [Export]
    MinigameManager mgm;
    public bool win=false;
    bool entered;
    [Export]
    bool pieGame;

    [Export]
    AudioStreamPlayer audio;

    public override void _Ready()
    {
        //GD.Print("Ready");
       // base._Ready();
    }
    public void _on_body_entered(Node2D body)
    {     
        if(body==mgm.objectOfInterest)
        {
            //GD.Print("Object has entered the body");
            if(Visible)
            {   
                audio.Play();
              //  GD.Print("Winner");
                win=true;
                mgm.QuickReturn();
            }
        }
        
    }
    /*public void _on_body_exited(Node2D node)
    {
        if(node==mgm.objectOfInterest)
            GD.Print("Object has exited the body");
            win=false;
    }*/

}
