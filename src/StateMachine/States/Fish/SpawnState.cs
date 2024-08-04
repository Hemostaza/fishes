using Godot;
using System;

public partial class SpawnState : State
{
    float force;

    public override void Enter()
    {
        base.Enter();
        force = TankController.Instance.GetRandomNumberGenerator().RandfRange(15,25);
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if(force>0){
            Vector2 vector = new Vector2(0,force);
            parent.Position += vector;
            force/=1.2f;
        }
        if(force<0.2f){
            EmitSignal(SignalName.transitioned,this,"Idle");
        }
    }
}
