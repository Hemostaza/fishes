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

    int money;

    float fishHealthRegeneration;

    Dictionary<String, Variant> playerStats;
    Dictionary<String, Variant> buffs;

    Dictionary<String, bool> lockedFish;

    [Signal]
    public delegate void onFishUnlockedEventHandler(int fishIndex);
    [Signal]
    public delegate void onMoneyChangeEventHandler(int changeValue);

    public void SetLockedFishForStart(){
        lockedFish = new Dictionary<String, bool>();
        foreach (FishData fish in FishDataResources.Instance.GetFishDatas()){
            lockedFish[fish.Name] = true;
        }
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        playerStats = new Dictionary<string, Variant>();
        buffs = new Dictionary<string, Variant>();
    }

    public override void _Ready()
    {
        //SetLockedFishForStart();

        //new game
        NewGame();
        //new game
        Instance = this;
        maxFoodCount = 1;
        clickPower = 1;
        fishHealthRegeneration = 1;
        selectedFood = 0;
        ProcessMode = Node.ProcessModeEnum.Always;

        //GD.Print(IsFishUnlocked("Default"));
    }

    bool paused = false;
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsKeyPressed(Key.Escape))
        {
            paused = !paused;
            GD.Print("pauza");
		    GetTree().Paused = paused;
        }
    }

    public void NewGame(){
        SetLockedFishForStart();
        UnlockFish(0);
        UnlockFish("CarnivoreTest");

        playerStats.Add("money",0);
        playerStats.Add("foodSelected",0);
        ChangeStats("money",100);

        buffs.Add("clickPower",1);
        buffs.Add("fishHelthRegeneration",1);
        buffs.Add("maxFoodCount",1);
    }

    public void SetBuffValue(String key, Variant value){
        buffs[key] = value;
    }
    public Variant GetBuffValue(String key){
        return buffs[key];
    }

    public void ChangeStats(String key, Variant value){
        playerStats[key] = value;
        if(key.Equals("money")){
            EmitSignal(SignalName.onMoneyChange,value);
        }
    }
    public Variant GetStats(String key){
        return playerStats[key];
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

    public float GetFishHealthRegeneration(){
        return fishHealthRegeneration;
    }
}
