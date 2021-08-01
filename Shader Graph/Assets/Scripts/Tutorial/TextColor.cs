using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();   
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = Color.white;
    }
    
}
