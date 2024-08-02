using Godot;
using System;

public partial class DeadState : State
{
    Timer lingeringTimer = new Timer();
    float lingering = 1.0f;


    public override void InitState()
    {
        base.InitState();
        SetTimer();
    }

    public override void Enter()
    {
        base.Enter();
        lingering=2;
        animatedSprite2D.Play("dieded");
        lingeringTimer.Start();
    }
    public override void Update(double delta)
    {
        base.Update(delta);
        parent.Position += Vector2.Down;
    }
    
    void SetTimer(){
        lingeringTimer.OneShot = true;
        lingeringTimer.WaitTime = lingering;
        lingeringTimer.Timeout += StopThat;
        AddChild(lingeringTimer);
    }

    void StopThat(){
        parent.QueueFree();
    }

}
