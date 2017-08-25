using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPanel : MonoBehaviour {

    public string Name { get; private set; }

    public ScreenPanel(string name) : base() {
        Name = name;
    }

    public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}
}