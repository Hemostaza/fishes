using Godot;
using Godot.Collections;
using System;

public partial class Particle : AnimatedSprite2D
{
    Timer particleDurationTimer;

    string particleSpritePath = "res://Resources/Particles/";

    public bool instant;

    public float particleDuration
    {
        get
        {
            return GetParticleDuration();
        }
        set
        {
            SetParticleDuration(value);
        }
    }

    float GetParticleDuration(){
        return particleDuration;
    }
    void SetParticleDuration(float value){
        particleDuration = value;
    }

    public override void _Ready()
    {
        if(particleDuration>0){
            SetTimer();
            StartParticle();
        }
        if(instant){
            Play();
        }

    }

    public void StartParticle(){
        particleDurationTimer.Start();
        Play();
    }
    void SetTimer(){
        particleDurationTimer.OneShot = true;
        particleDurationTimer.WaitTime = particleDuration;
        particleDurationTimer.Timeout += StopParticle;
        AddChild(particleDurationTimer);
    }

    void StopParticle(){
        Stop();
        QueueFree();
    }
}
