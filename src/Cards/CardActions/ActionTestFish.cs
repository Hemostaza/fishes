using Godot;
using Godot.Collections;
using System;
using System.Reflection.Metadata.Ecma335;
[GlobalClass]
public partial class ActionTestFish : CardResource
{

    //Karta gupika:
    [Export]
    PackedScene fishScene;

    Entity linkedEntity;

    public override void OnPlayed(CardController cardController, Card parent)
    {
        base.OnPlayed(cardController, parent);
        FishData fishData = FishDataResources.Instance.GetFishDataByName(GetCardName());
        if (fishData != null)
        {
            Fish spawnedEntity = (Fish) fishScene.Instantiate();
            spawnedEntity.Spawn(fishData);
            

            cardController.GetParent().AddChild(spawnedEntity);
            //Polowicznie zbędne ale chuj
            spawnedEntity.TreeExited += OnEntityDestroyed;

            parent.TreeExiting += spawnedEntity.OnCardDestory;

            linkedEntity = spawnedEntity;
        }
    }

    public void OnEntityDestroyed(){
        linkedEntity.TreeExited -= linkedEntity.OnCardDestory;
        cardController.DiscardCard(parent);
    }

    public override void Action()
    {
        Card jedzenie = cardController.FindInPlayedCards("Jedzenie");
        if (jedzenie == null)
        {
            GD.Print("Nie ma jedzenia");
        }
        else
        {
            GD.Print("Jest jedzenie");
        }
        Card innaryba = cardController.FindInPlayedCards("Test 2");
        if (innaryba != null)
        {
            GD.Print(innaryba);
           // Fish ryba = (Fish)cardController.GetLinkedEntity(innaryba);
        }
    }

    public override void EndTrun()
    {
        linkedEntity.TreeExited -= OnEntityDestroyed;
        cardController.DiscardCard(parent);
    }
}
