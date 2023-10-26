using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WaveSliderBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    private Slider waveSlider;
    private void OnEnable()
    {
        //Debug.Log(MainManager.Instance.MaxWave);
        waveSlider=GetComponent<Slider>();
        waveSlider.maxValue = MainManager.Instance.MaxWave;
        waveSlider.value = waveSlider.maxValue;
        waveText.text = "" + (int)waveSlider.value;
    }
    public void SetWaveNumber()
    {
        waveText.text = "" + (int)waveSlider.value;
        MainManager.Instance.CurWave = (int)waveSlider.value;
        

    }
}
