using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;

public partial class TabContainerController : Panel
{
    int activeTab = -1;
    int previousTab = -1;
    Array<Node> children;

    [Export]
    Array<Control> tabs;
    [Export]
    Array<Button> buttons;
    [Export]
    Button togglePanelButton;

    Array<Control> c = new Array<Control>();

    bool isShowed;
    
    [Export]
    Texture2D openIco;
    [Export]
    Texture2D closeIco;

    Vector2 panelSize;


    public override void _Ready()
    {
        ClosePanel();
        togglePanelButton.Pressed += ToggleShow;
        panelSize = Size;
        foreach(Control tab in tabs){
            tab.Visible = false;
        }
        //tabs[0].Visible = true;
        //buttons[activeTab].Disabled = true;
        //ChangeActiveTab(false,0);
    }

    public void ChangeActiveTab(bool pressed, int tab){

        if(pressed){
            if(!isShowed){
                ShowPanel();
            }
            if(activeTab!=-1){
                previousTab = activeTab;
                tabs[previousTab].Visible = false;
                buttons[previousTab].SetPressedNoSignal(false);
            }
            activeTab = tab;
            tabs[activeTab].Visible = true;
        }
        if(!pressed){
            ClosePanel();
        }
        // if(!isShowed){
        //     ShowPanel();
        // }
        // if(tab<0){
        //     ClosePanel();
        // }
        // if(tab>=tabs.Count){
        //     return -1;
        // }
        // if(tab==activeTab){
        //     ClosePanel();
        // }
        // previousTab = activeTab;
        // //buttons[previousTab].Disabled = false;
        // //buttons[tab].Disabled = true;
        // tabs[previousTab].Visible = false;
        // buttons[previousTab].ButtonPressed = false;
        // activeTab = tab;
        // tabs[activeTab].Visible = true;
        // return activeTab;
    }

    public void ToggleShow(){
        if(isShowed){
            ClosePanel();
        }else ShowPanel();
    }

    public void ShowPanel(){
        GD.Print(Position+" "+panelSize.X);
        Position -= new Vector2(170,0);
        GD.Print("Powinno sie pojawic "+Position);
        isShowed = true;
        togglePanelButton.Icon = closeIco;
    }

    public void ClosePanel(){
        Position += new Vector2(170,0);
        GD.Print("Powinno schowac "+Position);
        isShowed = false;
        if(activeTab!=-1){
            tabs[activeTab].Visible = false;
            if(buttons[activeTab].ButtonPressed)
                buttons[activeTab].SetPressedNoSignal(false);
            activeTab=-1;
        }
        togglePanelButton.Icon = openIco;
    }
}
