using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject[] cardSlots;

    #region Color
    [Header("Card Background Color")]
    public Color critterColor;
    public Color spellColor;
    public Color weaponColor;
    public Color gemStoneColor;
    #endregion

    #region Page
    public int page = 0;
    public Text pageText;
    #endregion

    #region Search
    private bool isSearch;//MARKER 保证Toggle Group Allow Switch Off是☑️的
    [SerializeField] private int totalNumbers;//用于显示Page
    #endregion

    #region search Class
    [SerializeField] private bool isSearchByClass;
    private string currentSearchClass;
    #endregion

    #region card board  
    public Image cardBoardImage;
    [Header("Card Board Color")]
    public Color normalBgColor;
    public Color critterBgColor;
    public Color spellBgColor;
    public Color weaponBgColor;
    public Color gemStoneBgColor;
    #endregion

    private void Start()
    {
        cardBoardImage.GetComponent<Image>().color = normalBgColor;
        DisplayCards(page);
        UpdatePageUI();
    }

    private void Update()
    {
        TurnPage();

        if (!isSearch)
        {
            totalNumbers = 0;
        }
    }

    private void UpdatePageUI()
    {
        //Normal Search Mode
        if(!isSearch)
        {
            pageText.text = ((page + 1) + "/" + (Mathf.Ceil(cardSlots.Length / 10) + 1));
        }
        else// CLASS Mode
        {
            pageText.text = (page + 1) + "/" + (Mathf.Ceil(totalNumbers / 10) + 1).ToString();
        }
    }

    // Normal Mode display Cards
    private void DisplayCards(int _page)
    {
        for(int i = 0; i < cardManager.cards.Count; i++)
        {
            if(i >= _page * 10 && i < (_page + 1) * 10)
            {
                DisplaySingleCard(i);

                cardSlots[i].transform.DOPunchRotation(new Vector3(0, 0, 4), 0.2f, 4, 0.5f);
                cardSlots[i].transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.2f, 4, 0.5f);
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void TurnPage()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
         }
    }

    public void moveRight()
    {
        if(!isSearch){
          if (page >= Mathf.Floor((cardManager.cards.Count - 1) / 10))
                {
                    page = 0;
                }
                else
                {
                    page++;
                }

            DisplayCards(page);
            UpdatePageUI();
        } else {
            if(isSearchByClass) {
                if (page >= (Mathf.FloorToInt(totalNumbers / 10)))
                {
                     page = 0;
                }
                else
                {
                    page++;
                }
                    
                DisplayBySearchClass();
                UpdatePageUI();
            }
        }
    }

    public void moveLeft(){
        if(!isSearch){
            if (page <= 0)
            {
                page = Mathf.FloorToInt((cardManager.cards.Count - 1) / 10);
            }
            else
            {
                page--;
            }

            DisplayCards(page);
            UpdatePageUI();
        } else {
            if(isSearchByClass) {
                if (page <= 0){
                    page = (Mathf.FloorToInt(totalNumbers / 10));
                }
                else
                {
                    page--;
                }

                DisplayBySearchClass();
                UpdatePageUI();
            }
        }
    }

    public void SearchByClass(string _cardClass)
    {
        isSearchByClass = true;

        isSearch = true;
        totalNumbers = 0;
        page = 0;

        currentSearchClass = _cardClass;

        switch (currentSearchClass)
        {
            case "Critter":
                {
                    cardBoardImage.GetComponent<Image>().color = critterBgColor;
                    break;
                }
            case "Spell":
                {
                    cardBoardImage.GetComponent<Image>().color = spellBgColor;
                    break;
                }
            case "Weapon":
                {
                    cardBoardImage.GetComponent<Image>().color = weaponBgColor;
                    break;
                }
            case "GemStone":
                {
                    cardBoardImage.GetComponent<Image>().color = gemStoneBgColor;
                    break;
                }
        }

        List<Card> cards = new List<Card>();
        cards = ReturnCard(_cardClass);

        DisplayCardWhenTab(cards);
        UpdatePageUI();
    }

    #region
    //服务于上面2个函数
    private void DisplayCardWhenTab(List<Card> cards)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (i >= page * 10 && i < (page + 1) * 10)
            {
                totalNumbers++;
                cardSlots[i].gameObject.SetActive(true);

                CardItemController controller = cardSlots[i].GetComponent<CardItemController>();
                
                controller.setName(cards[i].cardName);
                controller.setDescription(cards[i].cardDes);

                controller.setCardImage(cards[i].cardSprite);

                controller.setManaNum(cards[i].cardMana.ToString());
                controller.setAttackNum(cards[i].cardAttack.ToString());
                controller.setHealthNum(cards[i].cardHealth.ToString());

                switch (cards[i].cardClass)
                {
                    case CardClass.Critter:
                        {
                            controller.setClassName("Critter");
                            controller.setCardBackgroundColor(critterColor);
                            break;
                        }
                    case CardClass.Spell:
                        {
                            controller.setClassName("Spell");
                            controller.setCardBackgroundColor(spellColor);
                            break;
                        }
                    case CardClass.Weapon:
                        {
                            controller.setClassName("Weapon");
                            controller.setCardBackgroundColor(weaponColor);
                            break;
                        }
                    case CardClass.GemStone:
                        {
                            controller.setClassName("GemStone");
                            controller.setCardBackgroundColor(gemStoneColor);
                            break;
                        }

                }
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    private void DisplaySingleCard(int i)
    {
        totalNumbers++;
        cardSlots[i].gameObject.SetActive(true);

        CardItemController controller = cardSlots[i].GetComponent<CardItemController>();
                
        controller.setName(cardManager.cards[i].cardName);
        controller.setDescription(cardManager.cards[i].cardDes);

        controller.setCardImage(cardManager.cards[i].cardSprite);

        controller.setManaNum(cardManager.cards[i].cardMana.ToString());
        controller.setAttackNum(cardManager.cards[i].cardAttack.ToString());
        controller.setHealthNum(cardManager.cards[i].cardHealth.ToString());

        switch (cardManager.cards[i].cardClass)
        {
            case CardClass.Critter:
                {
                    controller.setClassName("Critter");
                    controller.setCardBackgroundColor(critterColor);
                    break;
                }
            case CardClass.Spell:
                {
                    controller.setClassName("Spell");
                     controller.setCardBackgroundColor(spellColor);
                    break;
                }
            case CardClass.Weapon:
                {
                    controller.setClassName("Weapon");
                    controller.setCardBackgroundColor(weaponColor);
                    break;
                }
            case CardClass.GemStone:
                {
                    controller.setClassName("GemStone");
                    controller.setCardBackgroundColor(gemStoneColor);
                    break;
                }
        }
    }

    public void InitialCards()
    {
        page = 0;
        isSearch = false;
        DisplayCards(page);

        isSearchByClass = false;

        cardBoardImage.GetComponent<Image>().color = normalBgColor;
        UpdatePageUI();
    }

    #region Method Overloading
    private List<Card> ReturnCard(int _mana)
    {
        List<Card> cards = new List<Card>();
        
        for(int i = 0; i < cardManager.cards.Count; i++)
        {
            Card card;

            if(_mana < 10)
            {
                if (cardManager.cards[i].cardMana == _mana)
                {
                    card = cardManager.cards[i];
                    cards.Add(card);
                }
            }
            else
            {
                if(cardManager.cards[i].cardMana >= _mana)
                {
                    card = cardManager.cards[i];
                    cards.Add(card);
                }
            }
        }

        return cards;
    }

    private List<Card> ReturnCard(string _cardClass)
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < cardManager.cards.Count; i++)
        {
            Card card;

            if (cardManager.cards[i].cardClass.ToString() == _cardClass)
            {
                card = cardManager.cards[i];
                cards.Add(card);
            }
        }

        return cards;
    }
    #endregion

    #region TurnPage
    private void DisplayBySearchClass()
    {
        List<Card> cards = new List<Card>();
        cards = ReturnCard(currentSearchClass);

        DisplayCardsBySearch(cards);
    }
    #endregion

    //MARKER 用于DisplayBySearchClass函数
    private void DisplayCardsBySearch(List<Card> cards)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (i >= page * 10 && i < (page + 1) * 10)
            {
                // MARKER
                cardSlots[i].gameObject.SetActive(true);

                CardItemController controller = cardSlots[i].GetComponent<CardItemController>();

                controller.setName(cards[i].cardName);
                controller.setDescription(cards[i].cardDes);

                controller.setCardImage(cards[i].cardSprite);

                controller.setManaNum(cards[i].cardMana.ToString());
                controller.setAttackNum(cards[i].cardAttack.ToString());
                controller.setHealthNum(cards[i].cardHealth.ToString());

                switch (cards[i].cardClass)
                {
                    case CardClass.Critter:
                        {
                            controller.setClassName("Critter");
                            controller.setCardBackgroundColor(critterColor);
                            break;
                        }
                    case CardClass.Spell:
                        {
                            controller.setClassName("Spell");
                            controller.setCardBackgroundColor(spellColor);
                            break;
                        }
                    case CardClass.Weapon:
                        {
                            controller.setClassName("Weapon");
                            controller.setCardBackgroundColor(weaponColor);
                            break;
                        }
                    case CardClass.GemStone:
                        {
                            controller.setClassName("GemStone");
                            controller.setCardBackgroundColor(gemStoneColor);
                            break;
                        }
                }

                cardSlots[i].transform.DOPunchRotation(new Vector3(0, 0, 15), 0.2f, 4, 0.5f);
                cardSlots[i].transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.2f, 4, 0.5f);
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
