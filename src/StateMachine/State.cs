using Godot;
using System;

public partial class State : Node
{
    public Entity parent;

    public String side;

    public String animation;

    [Signal]
    public delegate void transitionedEventHandler(State state, String newStateName);

    //public AnimatedSprite2D animationPlayer;

    public AnimationPlayer animationPlayer;
    public String animationGroup;
    String fullAnimationName;

    virtual public void InitState(Entity parent){
        this.parent = parent;
        //animationPlayer = parent.GetAnimatedSprite2D();
        animationPlayer = parent.animationPlayer;
        InitState();
    }
    virtual public void InitState(){

    }

    virtual public void Enter(){
    }

    virtual public void Exit(){
    }

    virtual public void Update(double delta){
    }

    virtual public void PhysicsUpdate(double delta){

    }
    
    public void UpdateAnimation(String animation){

    }
}
