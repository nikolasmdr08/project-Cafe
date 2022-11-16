using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TableScore : MonoBehaviour
{
    public TMP_Text textbox;
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.obtenerScores(textbox);
    }
}
