using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverColorText : MonoBehaviour
{
    [Header("Text fields")]
    [SerializeField] private Color hoverTextColor;
    [SerializeField] private Color defaultTextColor;
    [SerializeField] private Color pressedTextColor;

    [Header("Button fields")]
    [SerializeField] private Sprite hoverButton;
    [SerializeField] private Sprite defaultButton;

    [SerializeField] private bool textElement;
    public void ChangeColorText()
    {
        if (textElement)
        {
            this.GetComponent<TextMeshProUGUI>().color = hoverTextColor;

        }
    }

    public void RevertColorText()
    {
        if (textElement)
        {
            this.GetComponent<TextMeshProUGUI>().color = defaultTextColor;
        }

    }
    public void PressedColorText()
    {
        if (textElement)
        {
            this.GetComponent<TextMeshProUGUI>().color = pressedTextColor;
        }
    }

    public void HoverButton()
    {
        if (!textElement)
        {
            this.GetComponent<Image>().sprite = hoverButton;
        }
    }
    public void RevertColorButton()
    {
        if (!textElement)
        {
            this.GetComponent<Image>().sprite = defaultButton;
        }
    }

}
