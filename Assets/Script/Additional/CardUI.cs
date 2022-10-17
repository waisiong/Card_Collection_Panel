using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public ScriptableCard card;

    public Text nameText;
    public Text desText;

    public Image cardImage;
    public Image cardBGImage;

    public Text classText;

    public Text manaText;
    public Text attackText;
    public Text healthText;

    //MARKER new Feature
    public Image lockImage;
    public Image NumberImage;
    public Text countText;

    private void Start()
    {
        DisplayCard();
    }

    private void DisplayCard()
    {
        nameText.text = card.cardName;
        desText.text = card.cardDes;

        cardImage.sprite = card.cardSprite;

        manaText.text = card.cardMana.ToString();
        attackText.text = card.cardAttack.ToString();
        healthText.text = card.cardHealth.ToString();

        switch (card.cardClass)
        {
            case CardClass.Critter:
                {
                    classText.text = "Critter";
                    break;
                }
            case CardClass.Spell:
                {
                    classText.text = "Spell";
                    break;
                }
            case CardClass.Weapon:
                {
                    classText.text = "Weapon";
                    break;
                }
            case CardClass.GemStone:
                {
                    classText.text = "GemStone";
                    break;
                }
        }
    }
}
