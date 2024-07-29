using Godot;
using System;

public partial class PlayerStatus : Node
{
    public static PlayerStatus Instance { get; private set; }

    int clickPower;
    int maxFoodCount;

    int selectedFood;

    public override void _Ready()
    {
        Instance = this;
        maxFoodCount = 1;
        clickPower = 1;
        selectedFood = 0;
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
}
