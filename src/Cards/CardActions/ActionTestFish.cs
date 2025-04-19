using Godot;
using Godot.Collections;
using System;
using System.Reflection.Metadata.Ecma335;
[GlobalClass]
public partial class ActionTestFish : CardResource
{

    // CardController cardController;
    // Card parent;

    //Karta gupika:

    public override void OnPlayed(CardController cardController, Card parent)
    {
        base.OnPlayed(cardController, parent);
        // FishData fishData = FishDataResources.Instance.GetFishDataByName(GetCardName());
        // if (fishData != null)
        // {
        //     Fish instance = (Fish)cardController.fishScene.Instantiate();
        //     instance.Spawn(fishData);
        //     instance.TreeExited += () => cardController.OnEntityExit(instance);

        //     cardController.GetParent().AddChild(instance);

        //     instance.LinkCard(parent);
        //     cardController.GetLinkedCards().Add(parent,instance);
        // }
        parent.ChangeCardField(cardController.GetCardsInPlayField());
    }

    public override void Action(CardController old, Card old2)
    {
        Card jedzenie = cardController.FindInPlayedCards("Jedzenie");
        if (jedzenie == null)
        {
            GD.Print("Nie ma jedzenia");
        }
        else
        {
            GD.Print("Jest jedzenie");
            Fish ryba = (Fish)cardController.GetLinkedEntity(parent);
            //ryba.Sethungry;
        }
        Card innaryba = cardController.FindInPlayedCards("Ryba");
        if (innaryba != null)
        {
            Fish ryba = (Fish)cardController.GetLinkedEntity(innaryba);
        }
        // GD.Print("kurasa");
        // GD.Print(cardController.GetLinkedCards()[parent]);
        // GD.Print(cardController.GetCardsInPlayField().GetCards().Contains(parent));
        // GD.Print(cardController.FindInPlayedCards("Jedzenie"));
        // cardController.DiscardCard(parent);
        //if cardcontroller cardsInPlay.contains food
        //parent set hungry goes eating
        //else
        //parent set dieded
    }

    public override void EndTrun(CardController old, Card old2)
    {
        cardController.DiscardCard(parent);
    }
}
