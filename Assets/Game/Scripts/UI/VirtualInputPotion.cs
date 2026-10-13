using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualInputPotion : MonoBehaviour
{
   UIVirtualButton potionButton;
    private void Awake() {
        potionButton = GetComponent<UIVirtualButton>();
        potionButton.interactable = true;
        
    }
    public void EnableDisableButtonInteraction(bool on)
    {
        potionButton.interactable = on;
    }
}
