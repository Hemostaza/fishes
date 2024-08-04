using Godot;
using Godot.Collections;
using System;
using System.Collections.Immutable;
using System.Linq;

public partial class FishDataResources : Node
{
    public Dictionary<String, FishData> FishResources  { get; private set; }
    FishData[] fishDatas    { get; set; }

    Array<FishData> fishDatasByValue { get; set;}

    string fishResourcesPath = "res://Resources/Fish/";
    public static FishDataResources Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        FishResources = new Dictionary<string, FishData>();
        //fishDatas = new Array<FishData>();
        LoadFishData(fishResourcesPath);
        //fishDatas.OrderBy(item => item.value).ToArray();
        fishDatas = FishResources.Values.OrderBy(item => item.value).ToArray();
        
    }

    public FishData[] GetFishDatas(){
        return fishDatas;
    }

    public void LoadFishData(String path){
        foreach (var filePath in DirAccess.GetFilesAt(path)){
            FishData res = (FishData) GD.Load(path+"/"+filePath);
            FishResources[res.Name] = res;
            //fishDatas.Add(res);
        }
    }

    public FishData GetFishDataByName(String name){
        return FishResources[name];
    }

    public FishData GetFishDataByIndex(int index){
        return fishDatas[index];
    }
}
