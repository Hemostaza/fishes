using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class FoodDataResources : Node
{
    //public Dictionary<String, FoodData> FoodResources  { get; private set; }

    FoodData[] foodDatas;
    string foodResourcesPath = "res://Resources/Food/";

    public static FoodDataResources Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        //FoodResources = new Dictionary<string, FoodData>();
        //fishDatas = new Array<FishData>();
        LoadFoodData(foodResourcesPath);
        //fishDatasByValue = fishDatas.OrderBy(item => item.value).ToArray();
        
    }

    void LoadFoodData(String path){
        String[] paths = DirAccess.GetFilesAt(path);
        //GD.Print(paths);
        foodDatas = new FoodData[paths.Length];
        
        for(int i = 0; i<foodDatas.Length;i++){
            FoodData res = (FoodData) GD.Load(path+"/"+paths[i]);
            foodDatas[i] = res;
            //GD.Print(res);
        }
        foodDatas = foodDatas.OrderBy(item => item.value).ToArray();
        // foreach (var filePath in DirAccess.GetFilesAt(path)){
        //     FoodData res = (FoodData) GD.Load(path+"/"+filePath);
        //     FoodResources[res.Name] = res;
        // }
    }

    public FoodData GetFoodDataByIndex(int index){
        return foodDatas[index];
    }

    // public FoodData GetFoodDataByName(String name){
    //     return FoodResources[name];
    // }

}
