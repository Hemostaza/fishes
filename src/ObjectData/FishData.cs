using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class FishData : ObjectData
{


    [Export]
    public int maxSize;
    [Export]
    public float maxSpeed;
    
    [Export]
    public bool carnivore;

    [Export]
    public String favroiteFoodName;
    [Export]
    public int favroiteFoodBonus;
    

    [Export]
    public Array<CoinData> coinDatas;

    [Export]
    float minSize;


    //Ile głodu wytrzyma
    [Export]
    public int hungerMeter;
    //Jak szybko głodnieje
    [Export]
    public float hungerSpeed;
    [Export]
    public float maxHealth;
    [Export]
    public float healthRegen;


    RandomNumberGenerator rng = new RandomNumberGenerator();

    public Vector2 spawnSize(){
        
        float size = minSize + rng.RandfRange(-0.5f,0.5f);
        if(minSize==0){
            size=maxSize;
        }
        return new Vector2(size,size);
    }

    

}
