
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Meters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI meters;
    private float MeterCounter;
    private int meterCounterCasting;
    [SerializeField]private bool live;
    private void Update()
    {
        MeterSumatory();
    }

    private void MeterSumatory()
    {
        if (live)
        {
            meterCounterCasting = (int)MeterCounter;
            MeterCounter += 1 * Time.deltaTime;
            meters.text = "Distancia " + meterCounterCasting.ToString();
        }
    }
}
