using Godot;
using System;

public partial class State : Node
{
    public Entity parent;

    public String side;

    public String animation;

    [Signal]
    public delegate void transitionedEventHandler(State state, String newStateName);

    public AnimatedSprite2D animatedSprite2D;
    public String animationGroup;
    String fullAnimationName;
    //[Export]
    //public MoveComponent moveCompontent;
    //public InventoryComponent inventoryComponent;

    virtual public void InitState(Entity parent){
                this.parent = parent;
                animatedSprite2D = parent.GetAnimatedSprite2D();
                //moveCompontent = parent.GetMoveComponent();
                InitState();
    }
    virtual public void InitState(){

    }

    virtual public void Enter(){
        GD.Print(Name);
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
