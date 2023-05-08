using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class EventProp : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    private PointerEventData pointerEventData;

    private EventSystem eventSystem;


    void Start()
    {

        eventSystem = EventSystem.current;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (Button child in GetComponentsInChildren<Button>())
        {
            child.onClick.Invoke();

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (Button child in GetComponentsInChildren<Button>())
        {
            child.OnPointerEnter(eventData);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Button child in GetComponentsInChildren<Button>())
        {
            child.OnPointerExit(eventData);
        }
    }
}