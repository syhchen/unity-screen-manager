using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ErrorScreen : ModalScreen
{
    public Text TitleText;
    public Text DescText;
    public Button CloseButton;

    public void ShowError(string titleText, string descText, string buttonText)
    {
        if (!TitleText || !DescText || !CloseButton)
        {
            throw new Exception("ModalScreen: ShowError(titleText, descText, buttonText) failed, Title, Description, and Button must be set in Inspector.");
        }

        TitleText.text = titleText;

        DescText.text = descText;
        
        CloseButton.GetComponentInChildren<Text>().text = buttonText;

        Show();
    }
}