using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour 
{
    public Button button;
    public Text phonePowerText;
    public Text wifiText;

    private string batteryData;
    private string wifiData;

	void Awake()
	{
        button.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        batteryData = jo.Call<string>("MonitorBatteryState");
        wifiData = jo.Call<string>("ObtainWifiInfo");
        OnBatteryDataBack(batteryData);
        OnWifiDataBack(wifiData);
    }

    private void OnBatteryDataBack(string data)
    {
        phonePowerText.text = "";
        string[] args = data.Split('|');
        if (args[2] == "2")
        {
            phonePowerText.text += "电池充电中\n";
        }
        else
        {
            phonePowerText.text += "电池放电中\n";
        }
        int curPower = int.Parse(args[0]);
        float power = float.Parse(args[1]);
        float percent = curPower / power;
        phonePowerText.text += " cur power:" + curPower;
        phonePowerText.text += "  all power:" + power;
        phonePowerText.text += " 电量比例：" + (Mathf.CeilToInt(percent * 100) + "%").ToString();
    }

    private void OnWifiDataBack(string data)
    {
        wifiText.text = "";
        wifiText.text += wifiData;
        string[] args = wifiData.Split('|');
        int wifiLevel = int.Parse(args[0]);
        wifiText.text += "Wifi信号格数：" + wifiLevel.ToString() + "\n";
        string ip = "IP：" + args[1] + "\n";
        string mac = "MAC:" + args[2] + "\n";
        string ssid = "Wifi Name:" + args[3] + "\n";
        wifiText.text += ip;
        wifiText.text += mac;
        wifiText.text += ssid;
    }
}
