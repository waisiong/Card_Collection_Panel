using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardClass
{
    Critter,
    Spell,
    Weapon,
    GemStone
}

[System.Serializable]
public class Card
{
    public string cardName;
    [TextArea(1,5)]
    public string cardDes;

    public Sprite cardSprite;

    public CardClass cardClass;

    public int cardMana;
    public int cardAttack;
    public int cardHealth;
}
