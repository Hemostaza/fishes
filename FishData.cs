using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class FishData : Resource
{
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
    float sizee;


    RandomNumberGenerator rng = new RandomNumberGenerator();

    public Vector2 spawnSize(){
        
        float size = rng.RandfRange(0.5f,1);
        if(sizee>0){
            size=sizee;
        }
        return new Vector2(size,size);
    }

}
