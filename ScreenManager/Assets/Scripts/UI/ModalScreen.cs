using UnityEngine;
using System;
using System.Collections.Generic;

public class ModalScreen : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}