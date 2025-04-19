using Godot;
using System;
using System.Collections.Generic;

public partial class GameStateMachine : Node
{
    [Export]
    GameState initialState;
    
    GameState currentState;
    GameState previousState;

    Dictionary<String, GameState> gameStates = new Dictionary<string, GameState>();

    public override void _Ready()
    {
        foreach (GameState child in GetChildren()){

            if(child.GetType().IsSubclassOf(typeof(GameState))){
                gameStates[child.Name] = child;
                child.transitionedGameState += OnChildTransition;
                child.InitState();
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

    public void OnChildTransition(GameState state, String newStateName)
    {
        if(state!=currentState){
            return;
        }
        previousState = state;
        ChangeState(newStateName);
    }

    public void ChangeState(String newStateName){
        GameState newState = gameStates[newStateName];
        
        if(newState==null){
            return;
        }
        if(currentState!=null){
            currentState.Exit();
        }
        
        newState.Enter();
        currentState = newState;
    }

    public Dictionary<String, GameState> GetStates(){
        return gameStates;
    }

    public GameState GetCurrentState(){
        return currentState;
    }
    
    public GameState GetPreviousState(){
        return previousState;
    }
    public String GetPreviousStateName(){
        if(previousState!=null){
            return previousState.Name;
        }
        return "O kurwa";
    }

}
