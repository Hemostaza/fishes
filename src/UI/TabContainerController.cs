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
        //Tymczasowo domyślnie aktywna karta to 0
        activeTab = 0;
        tabs[0].Visible = true;
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
    }

    public void ToggleShow(){
        if(isShowed){
            ClosePanel();
        }else ShowPanel();
    }

    public void ShowPanel(){
        Position -= new Vector2(170,0);
        isShowed = true;
        togglePanelButton.Icon = closeIco;
    }

    public void ClosePanel(){
        Position += new Vector2(170,0);
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
