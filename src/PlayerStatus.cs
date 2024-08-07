using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerStatus : Node
{
    public static PlayerStatus Instance { get; private set; }

    int clickPower;
    int maxFoodCount;

    int selectedFood;

    float fishHealthRegeneration;

    Dictionary<String, bool> lockedFish;

    [Signal]
    public delegate void onFishUnlockedEventHandler(int fishIndex);

    public void SetLockedFishForStart(){
        lockedFish = new Dictionary<String, bool>();
        foreach (FishData fish in FishDataResources.Instance.GetFishDatas()){
            lockedFish[fish.Name] = true;
        }
    }

    public bool IsFishLocked(int index){
        return IsFishLocked(lockedFish.ElementAt(index).Key);
    }
    public bool IsFishLocked(String name){
        return lockedFish[name];
    }

    public void UnlockFish(int index){

        UnlockFish(lockedFish.ElementAt(index).Key);
    }
    public void UnlockFish(String name){

        lockedFish[name] = false;
        EmitSignal(SignalName.onFishUnlocked,lockedFish.Keys.ToList().IndexOf(name));
    }

    public override void _Ready()
    {
        SetLockedFishForStart();
        Instance = this;
        maxFoodCount = 1;
        clickPower = 1;
        fishHealthRegeneration = 1;
        selectedFood = 0;

        UnlockFish(0);

        //GD.Print(IsFishUnlocked("Default"));
    }
    public int GetClickPower(){
        int power = clickPower; //Odjebanie matematyki na siłe z bonusów?
        return power;
    }

    public int GetMaxFoodCount(){
        int maxCount = maxFoodCount; //odebanie matematyki an sile;
        return maxCount;
    }

    public int GetSelectedFood(){
        return selectedFood;
    }

    public float GetFishHealthRegeneration(){
        return fishHealthRegeneration;
    }
}
