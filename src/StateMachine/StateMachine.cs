using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export]
    State initialState;
    [Export]
    Entity parent;
    State currentState;
    State previousState;
    Dictionary<String, State> states = new Dictionary<string, State>();
    public override void _Ready()
    {
        foreach (State child in GetChildren()){

            if(child.GetType().IsSubclassOf(typeof(State))){
                states[child.Name] = child;
                child.transitioned += OnChildTransition;
                child.InitState(parent);
            }
        }
        if(initialState!=null){
            initialState.Enter();
            currentState = initialState;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if(currentState!=null){
            currentState.PhysicsUpdate(delta);
        }
    }

    public override void _Process(double delta)
    {
        if(currentState!=null){
            currentState.Update(delta);
        }
    }

    public void OnChildTransition(State state, String newStateName)
    {
        if(state!=currentState){
            return;
        }
        previousState = state;
        ChangeState(newStateName);
    }

    public void ChangeState(String newStateName){
        State newState = states[newStateName];
        
        if(newState==null){
            return;
        }
        if(currentState!=null){
            currentState.Exit();
        }
        
        newState.Enter();
        currentState = newState;
    }

    public Dictionary<String, State> GetStates(){
        return states;
    }

    public State GetCurrentState(){
        return currentState;
    }
    
    public State GetPreviousState(){
        return previousState;
    }
    public String GetPreviousStateName(){
        if(previousState!=null){
            return previousState.Name;
        }
        return "O kurwa";
    }
}
