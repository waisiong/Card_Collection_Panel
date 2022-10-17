using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image buttonImage;

    public void setButtonColor(Color value)
    {
        buttonImage.color = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            transform.DOPunchRotation(new Vector3(0, 0, 30), 0.2f, 4, 0.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 1.5f).setEasePunch();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            LeanTween.scale(gameObject,new Vector3(1f, 1f, 1f), 0.5f);
    }
}
