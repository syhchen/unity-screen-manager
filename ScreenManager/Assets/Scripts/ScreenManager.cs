using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : SingletonMonoBehaviour<ScreenManager> {

    public static GameObject RootCanvas;
    public List<ScreenPanel> Screens { get; private set; }
    public Dictionary<string, int> AvailableScreens { get; private set; }
    public ScreenPanel CurrentScreen { get; private set; }

    private Stack<ScreenPanel> _navigationStack;

    protected override void Awake() {
        base.Awake();

        Screens = new List<ScreenPanel>();
        AvailableScreens = new Dictionary<string, int>();
        _navigationStack = new Stack<ScreenPanel>();
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

    public bool NavigateTo(string nextScreenName, bool resetNavigationStack = false) {
        if (!AvailableScreens.ContainsKey(nextScreenName)) {

            return false;
        }

        return _navigateTo(AvailableScreens[nextScreenName], resetNavigationStack);
    }

    public bool NavigateBack() {

        return _navigateTo(-1, false);
    }

    public bool CanNavigateBack() {

        return (_navigationStack != null && _navigationStack.Count > 1);
    }

    private void _handleScreenTransition(ScreenPanel nextScreen) {
        if (CurrentScreen) CurrentScreen.HideScreen();
        
        nextScreen.ShowScreen();
        CurrentScreen = nextScreen;
    }

    private bool _navigateTo(int nextScreenIndex, bool resetNavigationStack) { // index of target screen, or if -1 go back to last screen in stack
        if (resetNavigationStack) _navigationStack.Clear();

        if (nextScreenIndex == -1 && CanNavigateBack()) {
            _navigationStack.Pop();
        } else if (nextScreenIndex >= 0 &&
            nextScreenIndex < Screens.Count &&
            Screens[nextScreenIndex] != CurrentScreen) {
            _navigationStack.Push(Screens[nextScreenIndex]);
        }
        else {
            return false;
        }

        _handleScreenTransition(_navigationStack.Peek());

        return true;
    }
}
