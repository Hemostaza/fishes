using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class FishData : Resource
{
    [Export]
    public String Name;

    [Export]
    public SpriteFrames sprites;
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

    [Export]
    public int value;
    [Export]
    public Texture2D icon;

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

    public override string ToString()
    {
        return "\n Name: "+ Name +"\n Value: "+value;
    }

}
