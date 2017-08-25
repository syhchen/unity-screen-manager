using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScreenA : ScreenPanel {

	public static readonly string NAME = "DemoScreenA";

	public DemoScreenA() : base(NAME) {}

	public void NavigateToDemoScreenB() {
		ScreenManager.Instance.NavigateTo("DemoScreenB");
	}

	public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}
}
