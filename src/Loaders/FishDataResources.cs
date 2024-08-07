using Godot;
using Godot.Collections;
using System;
using System.Collections.Immutable;
using System.Linq;

public partial class FishDataResources : Node
{
    FishData[] fishDatas    { get; set; }

    string fishResourcesPath = "res://Resources/Fish/";
    public static FishDataResources Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        
        LoadFishData(fishResourcesPath);
    }

    public FishData[] GetFishDatas(){
        return fishDatas;
    }

    public void LoadFishData(String path){
        String[] paths = DirAccess.GetFilesAt(path);
        //GD.Print(paths);
        fishDatas = new FishData[paths.Length];
        
        for(int i = 0; i<fishDatas.Length;i++){
            FishData res = (FishData) GD.Load(path+"/"+paths[i]);
            fishDatas[i] = res;
            //GD.Print(res);
        }
        fishDatas = fishDatas.OrderBy(item => item.value).ToArray();
    }

    public FishData GetFishDataByIndex(int index){
        return fishDatas[index];
    }

    public String GetFishNameByIndex(int index){
        return fishDatas[index].Name;
    }
}
