using Godot;
using System;
using System.Runtime.Serialization;
using System.Transactions;

public partial class Rotator : Node2D
{
    float speed=400f;
    float angularSpeed=Mathf.Pi;
    bool rotating;

    [Export]
    bool areaBased;
    [Export]
    Area2D WinArea;

    [Export]
    bool PressToRotate;
    
    public bool win;
    [Export]
    CollisionPolygon2D collisionPolygon2D;

    Random rand=new Random();
    public override void _Ready()
    {
        
        if(!PressToRotate)
            rotating=true;
     //   if(WinArea!=null)
       //     WinArea.Visible=false;
        if(PressToRotate)
        {
            RotationDegrees=rand.Next(75,270);
        }
    }
    public override void _Process(double delta)
    {
        if(rotating)
        {
            Rotation+=(float)delta*angularSpeed*2;
            
            //collisionPolygon2D.Rotation+=(float)delta*angularSpeed*2;
           // GD.Print(collisionPolygon2D.Rotation);
        }
        if(PressToRotate)
        {
            if(Input.IsActionPressed("A_Button"))
            {
                Rotation+=(float)delta*angularSpeed*1.2f;
                //GD.Print(RotationDegrees);
            }
            else if(Input.IsActionPressed("B_Button"))
            {
                Rotation-=(float)delta*angularSpeed*2*1.2f;
                //GD.Print(RotationDegrees);
            }

        }
        if(Input.IsActionJustPressed("A_Button")||Input.IsActionJustPressed("B_Button"))
        {
           if(!PressToRotate)
           {
            rotating=false;
            //if(WinArea!=null)
            //WinArea.Visible=true;
            //GD.Print(Rotation);
           }
        }
        if(RotationDegrees<5&&RotationDegrees>-5||RotationDegrees<365&&RotationDegrees>355)
            {
                win=true;
            }
            else
            {
                win=false;
            }

    }

}
