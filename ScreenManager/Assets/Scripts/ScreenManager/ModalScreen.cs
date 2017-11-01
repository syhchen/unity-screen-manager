using UnityEngine;
using System;
using System.Collections.Generic;

public class ModalScreen : MonoBehaviour
{
    public GameObject Title;
    public GameObject Description;
    public GameObject Button;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void ShowError(string titleText, string descText, string buttonText)
    {
        if (!Title || !Description || !Button)
        {
            throw new Exception("ModalScreen: ShowError(titleText, descText, buttonText) failed, Title, Description, and Button must be set in Inspector.");
        }

        // TODO: set value of GameObjects

        Show();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}