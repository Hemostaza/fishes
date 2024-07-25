using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class ResourcesLoader : Node
{
    public Dictionary<String, FishData> FishResources  { get; private set; }
    public Array<FishData> fishDatas    { get; private set; }

    public static FishData[] fishDatasByValue {get; set;}

    String fishResourcesPath = "res://Resources/Fish/";
    public static ResourcesLoader Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        FishResources = new Dictionary<string, FishData>();
        fishDatas = new Array<FishData>();
        LoadFishData(fishResourcesPath);
        fishDatasByValue = fishDatas.OrderBy(item => item.value).ToArray();
        
    }

    public void LoadFishData(String path){
        foreach (var filePath in DirAccess.GetFilesAt(path)){
            FishData res = (FishData) GD.Load(path+"/"+filePath);
            FishResources[res.Name] = res;
            fishDatas.Add(res);
        }
    }
}
