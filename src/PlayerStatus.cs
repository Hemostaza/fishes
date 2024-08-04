using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStatus : Node
{
    public static PlayerStatus Instance { get; private set; }

    int clickPower;
    int maxFoodCount;

    int selectedFood;

    float fishHealthRegeneration;

    Dictionary<String, bool> lockedFish;

    [Signal]
    public delegate void onFishUnlockedEventHandler(String fishName);

    public void SetUnlockedFishForStart(){
        lockedFish = new Dictionary<String, bool>();
        foreach (FishData fish in FishDataResources.Instance.GetFishDatas()){
            lockedFish[fish.Name] = true;
        }
    }

    public Dictionary<String, bool> GetUnlockedFish(){
        return lockedFish;
    }

    public bool IsFishLocked(FishData fish){
        return IsFishLocked(fish.Name);
    }
    public bool IsFishLocked(String fishName){
        return lockedFish[fishName];
    }

    public void UnlockFish(FishData fish){
        UnlockFish(fish.Name);
    }
    public void UnlockFish(String fishName){
        lockedFish[fishName] = false;
    }

    public override void _Ready()
    {
        SetUnlockedFishForStart();
        Instance = this;
        maxFoodCount = 1;
        clickPower = 1;
        fishHealthRegeneration = 1;
        selectedFood = 0;

        UnlockFish("Default");
        UnlockFish("Default1");

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
