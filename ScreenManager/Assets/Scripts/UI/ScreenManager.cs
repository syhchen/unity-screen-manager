using UnityEngine;
using System;
using System.Collections.Generic;

// TODO: transitions for avatar screen (drag to fade?)
// TODO: titlebar UI w/ breadcrumbs, how to get titlebar and underlay to animate with screen?

public class ScreenManager: SingletonMonoBehaviour<ScreenManager>
{
    public BaseScreen DefaultScreen;
    public ErrorScreen ErrorModal;
    public TitleBar Navigation;

    public BaseScreen CurrentScreen { get; private set; }

    public Dictionary<string, BaseScreen> Screens { get; private set; }

    private Stack<BaseScreen> _navStack;

    protected override void Awake()
    {
        base.Awake();
        
        if (!DefaultScreen || !ErrorModal)
        {
            throw new ArgumentException("ScreenManager: Initialize() failed, DefaultScreen and ErrorModal must be set in Inspector.");
        }

        Screens = new Dictionary<string, BaseScreen>();
        _navStack = new Stack<BaseScreen>();

        foreach (BaseScreen screen in GetComponentsInChildren<BaseScreen>(true))
        {
            screen.Initialize(this);
            Screens[screen.Name] = screen;
        }

        if (!Screens.ContainsKey(DefaultScreen.Name))
        {
            throw new KeyNotFoundException("ScreenManager: Initialize() failed, cannot find screen named '" + DefaultScreen.Name +  "'.");
        }

        NavigateTo(DefaultScreen.Name);
    }

    public bool ResetNavStack()
    {
        if (!CurrentScreen.IsTransitioning)
        {
            _navStack.Clear();

            return true;
        }

        return false;
    }

    public void ShowErrorModal(string titleText, string descText, string buttonText = "Close")
    {
        ErrorModal.ShowError(titleText, descText, buttonText);
    }
    
    public void HideErrorModal()
    {
        ErrorModal.Hide();
    }

    public void NavigateTo(string screenName)
    {
        if (!Screens.ContainsKey(screenName))
        {
            throw new KeyNotFoundException("ScreenManager: NavigateTo(screenName) failed, no screen named '" + screenName + "'.");
        }

        BaseScreen targetScreen = Screens[screenName];

        if (CanTransition(targetScreen, CurrentScreen))
        {
            _navStack.Push(CurrentScreen);

            Transition(targetScreen, CurrentScreen, false);
        }
    }

    public void NavigateBack()
    {
        if (_navStack.Count > 1)
        {
            BaseScreen targetScreen = _navStack.Peek();

            if (CanTransition(targetScreen, CurrentScreen))
            {
                _navStack.Pop();

                Transition(targetScreen, CurrentScreen, true);
            }
        }
    }

    public bool CanTransition(BaseScreen targetScreen, BaseScreen originScreen)
    {
        if (targetScreen != null && originScreen != null)
        {
            return !originScreen.IsTransitioning && !targetScreen.IsTransitioning;
        }

        if (targetScreen != null && originScreen == null)
        {
            return !targetScreen.IsTransitioning;
        }
        
        return false;
    }

    private void Transition(BaseScreen targetScreen, BaseScreen originScreen, bool isReverse)
    {
        CurrentScreen = targetScreen;

        if (originScreen)
        {
            originScreen.Hide(isReverse);
        }
        
        targetScreen.Show(isReverse);        
    }
}
