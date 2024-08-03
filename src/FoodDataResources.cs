using Godot;
using System;
using System.Collections.Generic;

public partial class FoodDataResources : Node
{
    public Dictionary<String, FoodData> FoodResources  { get; private set; }
    string foodResourcesPath = "res://Resources/Food/";

    public static FoodDataResources Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        FoodResources = new Dictionary<string, FoodData>();
        //fishDatas = new Array<FishData>();
        LoadFoodData(foodResourcesPath);
        //fishDatasByValue = fishDatas.OrderBy(item => item.value).ToArray();
        
    }

    public void LoadFoodData(String path){
        foreach (var filePath in DirAccess.GetFilesAt(path)){
            FoodData res = (FoodData) GD.Load(path+"/"+filePath);
            FoodResources[res.Name] = res;
        }
    }

    public FoodData GetFoodDataByName(String name){
        return FoodResources[name];
    }

}
