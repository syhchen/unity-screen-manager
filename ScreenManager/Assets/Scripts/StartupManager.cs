using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : SingletonMonoBehaviour<StartupManager> {

	public ScreenPanel[] Screens;
	
	void Start () {
		ScreenManager.Instance.Initialize(Screens);
	}
}
