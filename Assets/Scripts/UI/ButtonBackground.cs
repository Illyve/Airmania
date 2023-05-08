using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBackground : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Color normalColor;
    public Color highlightedColor;
    public Color pressedColor;

    private Image image;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = normalColor;
    }
}
