using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ErrorScreen : ModalScreen
{
    public Text Title;
    public Text Description;
    public Button Button;

    public void ShowError(string titleText, string descText, string buttonText)
    {
        if (!Title || !Description || !Button)
        {
            throw new Exception("ModalScreen: ShowError(titleText, descText, buttonText) failed, Title, Description, and Button must be set in Inspector.");
        }

        Title.text = titleText;
        Description.text = descText;
        Button.GetComponentInChildren<Text>().text = buttonText;

        Show();
    }
}