using UnityEngine;
using System;
using System.Collections.Generic;

public class ThirdScreen : BaseScreen
{
    public static readonly string NAME = "Third Screen";
    public static readonly bool NAVIGATOR = false;

    public ThirdScreen() : base(NAME, NAVIGATOR) {}

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
