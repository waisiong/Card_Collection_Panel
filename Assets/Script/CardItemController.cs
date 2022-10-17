using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardItemController : MonoBehaviour
{
    public Image cardBackgroundImage;
    public Image cardImage;
    public Text cardName;
    public Text description;
    public Text attackNum;
    public Text healthNum;
    public Text manaNum;
    public Text className;

    public void setCardBackgroundColor(Color value){
        cardBackgroundImage.color = value;
    }

    public void setCardImage(Sprite value){
        cardImage.sprite = value;
    }

    public void setClassName(string value){
        className.text = value;
    }

    public void setName(string value){
        cardName.text = value;
    }

    public void setDescription(string value){
        description.text = value;
    }

    public void setAttackNum(string value){
        attackNum.text = value;
    }

    public void setHealthNum(string value){
        healthNum.text = value;
    }

    public void setManaNum(string value){
        manaNum.text = value;
    }
}
