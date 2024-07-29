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
    Coin coin;
    [Export]
    public float speed;
    
    [Export]
    public PackedScene coinScene;
    [Export]
    public Array<CoinData> coinDatas;

    [Export]
    float minSize;

    [Export]
    public int value;
    [Export]
    public Texture2D icon;
    [Export]
    public int hungerMeter;


    RandomNumberGenerator rng = new RandomNumberGenerator();

    public Vector2 spawnSize(){
        
        float size = minSize + rng.RandfRange(-1,1);
        if(minSize==0){
            size=maxSize;
        }
        return new Vector2(size,size);
    }

    public override string ToString()
    {
        return Name+" "+value;
    }

}
