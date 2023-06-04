using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnhancedUI.EnhancedScroller;

public class LobbyEnhancedScollerCellView : EnhancedScrollerCellView
{
    public TextMeshProUGUI animalNameText;
    public void SetData(LobbyInfoEnhancedScrollerData data)
    {
       animalNameText.text = data.animalName;
    }

}

