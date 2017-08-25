using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScreenPanel : MonoBehaviour {

    public string Name { get; private set; }

    public ScreenPanel(string name) : base() {
        Name = name;
    }

    public virtual void ShowScreen() {
        gameObject.SetActive(true);

        // float widthOffset = _rectTransform.rect.width;
        // Vector3 startPosition = new Vector3(widthOffset, 0, 0);
        // _rectTransform.localPosition = startPosition;

        // // tween the panel to the desired position
        // Sequence seq = DOTween.Sequence();
        // seq.AppendInterval(delay);
        // seq.Append(transform.DOLocalMoveX(0, duration));
        // seq.AppendCallback(ShowScreenDone);
    }

    public virtual void HideScreen() {
        gameObject.SetActive(false);
    }

    public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}
}