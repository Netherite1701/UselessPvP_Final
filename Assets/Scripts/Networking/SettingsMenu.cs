using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_InputField xinput;
    public TMP_InputField yinput;
    public Slider xSlider;
    public Slider ySlider;

    //public void Start()
    //{
    //    audioMixer.SetFloat("Volume", Mathf.Log(0.01f) * 20f);
    //}

    private void Start()
    {
        xSlider.minValue = 0;
        ySlider.minValue = 0;
        xSlider.maxValue = 400;
        ySlider.maxValue = 400;
        xSlider.wholeNumbers = true;
        ySlider.wholeNumbers = true;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log(volume)*20f);
    }

    public void Update()
    {
        xinput.text = RoomManager.instance.xSens.ToString();
        yinput.text = RoomManager.instance.ySens.ToString();
        xSlider.value = RoomManager.instance.xSens;
        ySlider.value = RoomManager.instance.ySens;
    }
}
