using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHoverBtn : MonoBehaviour
{
    
    private void OnMouseEnter() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        AudioManager.S_PlaySound2D(SFXID.UI_BUTTON);
    }

    private void OnMouseExit() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
