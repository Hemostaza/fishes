using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class TabContainerController : Panel
{
    int activeTab = 0;
    int previousTab = -1;
    Array<Node> children;

    [Export]
    Array<Control> tabs;
    [Export]
    Array<Button> buttons;

    Array<Control> c = new Array<Control>();


    public override void _Ready()
    {
        foreach(Control tab in tabs){
            tab.Visible = false;
        }
        tabs[0].Visible = true;
        buttons[activeTab].Disabled = true;
        ChangeActiveTab(0);
    }

    public int ChangeActiveTab(int tab){
        
        if(tab>=tabs.Count){
            return -1;
        }
        if(tab==activeTab){
            return -1;
        }
        previousTab = activeTab;
        buttons[previousTab].Disabled = false;
        buttons[tab].Disabled = true;
        tabs[previousTab].Visible = false;
        buttons[previousTab].ButtonPressed = false;
        activeTab = tab;
        tabs[activeTab].Visible = true;
        return activeTab;
    }
}
