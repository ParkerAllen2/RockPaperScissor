using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Color faded;
    private int currentButton = -1;

    public void Highlight(int i)
    {
        foreach(Button b in buttons)
        {
            ColorBlock cb = b.colors;
            cb.normalColor = faded;
            b.colors = cb;
        }

        if (i != currentButton)
        {
            ColorBlock c = buttons[i].colors;
            c.normalColor = Color.white;
            buttons[i].colors = c;
            currentButton = i;
        }
        else
            currentButton = -1;
    }
}
