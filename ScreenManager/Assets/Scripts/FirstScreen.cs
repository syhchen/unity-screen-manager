using UnityEngine;
using System;
using System.Collections.Generic;

public class FirstScreen : BaseScreen
{
    public static readonly string NAME = "First Screen";
    public static readonly bool NAVIGATOR = true;

    public FirstScreen() : base(NAME, NAVIGATOR) {}

    public void OnPressErrorButton()
    {
        _screenManager.ShowErrorModal("First Screen Error", "This is a description.");
    }

    protected override void WillShow()
    {
        Debug.Log(NAME + ": WillShow()");
    }

    protected override void OnShow()
    {
        Debug.Log(NAME + ": OnShow()");
    }

    protected override void WillHide()
    {
        Debug.Log(NAME + ": WillHide()");
    }
    protected override void OnHide()
    {
        Debug.Log(NAME + ": OnHide()");
    }
}
