using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScreenManager : SingletonMonoBehaviour<ScreenManager> {

    public static readonly UITransition.AnimateMode MODE = UITransition.AnimateMode.PAGE;
    
    public List<ScreenPanel> Screens { get; private set; }
    public Dictionary<string, int> AvailableScreens { get; private set; }
    public ScreenPanel CurrentScreen { get; private set; }

    private Stack<ScreenPanel> _navigationStack;
    private bool _isShowing;
    private bool _isHiding;

    protected override void Awake() {
        base.Awake();

        Screens = new List<ScreenPanel>();
        AvailableScreens = new Dictionary<string, int>();
        _navigationStack = new Stack<ScreenPanel>();
        _isShowing = false;
        _isHiding = false;
    }

    public void Initialize(ScreenPanel[] screens) {
        if (Screens.Count == 0 && AvailableScreens.Count == 0) {
            for (int i = 0; i < screens.Length; i++) {
                Screens.Add(screens[i]);
                AvailableScreens.Add(screens[i].Name, i);
            }
            _navigateTo(0, false);
        }
    }

    public void NavigateTo(string nextScreenName, bool resetNavigationStack = false) {
        if (!AvailableScreens.ContainsKey(nextScreenName)) {

            return;
        }

        _navigateTo(AvailableScreens[nextScreenName], resetNavigationStack);
    }

    public void NavigateBack() {

        _navigateTo(-1, false);
    }

    public bool CanNavigateBack() {

        return (_navigationStack != null && _navigationStack.Count > 1);
    }


    private void _handleScreenTransition(ScreenPanel nextScreen, bool isAnimateForward) {
        // only hide the current screen if there is one
        if (CurrentScreen) {
            CurrentScreen.HideScreen(UITransition.DELAY, UITransition.DURATION, MODE, isAnimateForward, () => {
                _isHiding = false;
            });
        }
        else {
            _isHiding = false;
        }
        nextScreen.ShowScreen(UITransition.DELAY, UITransition.DURATION, MODE, isAnimateForward, () => {
            _isShowing = false;
        });

        CurrentScreen = nextScreen;
    }

    private void _navigateTo(int nextScreenIndex, bool resetNavigationStack) { // index of target screen, or if -1 go back to last screen in stack
        if(!_isShowing && !_isHiding) {
            _isShowing = true;
            _isHiding = true;

            if (resetNavigationStack) _navigationStack.Clear();

            bool isAnimateForward = true;
            if (nextScreenIndex == -1 && CanNavigateBack()) {
                _navigationStack.Pop();
                isAnimateForward = !isAnimateForward;
            } else if (nextScreenIndex >= 0 &&
                nextScreenIndex < Screens.Count &&
                Screens[nextScreenIndex] != CurrentScreen) {
                _navigationStack.Push(Screens[nextScreenIndex]);
            }
            else {
                return;
            }

            _handleScreenTransition(_navigationStack.Peek(), isAnimateForward);
        }
    }
}
