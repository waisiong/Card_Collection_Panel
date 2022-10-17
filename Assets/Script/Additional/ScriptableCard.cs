using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class ScriptableCard : ScriptableObject
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
