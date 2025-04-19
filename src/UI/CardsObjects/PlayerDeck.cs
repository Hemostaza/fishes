using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerDeck : CardDeck
{
    [Export]
    CardResource[] testDeck;

    public override void _Ready()
    {
        base._Ready();
    }

}
