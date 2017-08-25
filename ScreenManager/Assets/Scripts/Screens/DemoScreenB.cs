using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScreenB : ScreenPanel {

	public static readonly string NAME = "DemoScreenB";

	public DemoScreenB() : base(NAME) {}

		public void NavigateToDemoScreenA() {
		ScreenManager.Instance.NavigateTo("DemoScreenA");
	}

	// TODO: movo to ScreenPanel
	public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}
}
