using Godot;
using Godot.Collections;
using System;

public partial class ShopButton : Control
{
    [Export]
    Button button;
    [Export]
    Label label;

    int value;

    int index;

    bool fishUnlocked;
    public override void _Ready()
    {
        button.ButtonDown += OnClick;
        base._Ready();
    }

    public virtual void SetButtonData(int index,int value, Texture2D icon){
        button.Icon = icon;
        label.Text = value.ToString();
        this.value = value;
        this.index = index;
        //button.Disabled = PlayerStatus.Instance.IsFishLocked(index);
    }

    void OnClick(){
        //tank.SpawnFish(fishName);
        int money = PlayerStatus.Instance.GetStats("money").As<int>();
        if(money>=value){
            money -= value;
            PlayerStatus.Instance.ChangeStats("money",money);
            OnClickEffect(index);
        }else{
            GD.Print("ni ma pijondza: "+PlayerStatus.Instance.GetStats("money").As<int>());
        }
        //GD.Print("Klikniete"); spawn fishdata xD spawn fish by name: spawnFish.
    }
    public void UnlockButton(){
        button.Disabled = false;
    }

    public virtual void OnClickEffect(int index){
        return;
    }

}
