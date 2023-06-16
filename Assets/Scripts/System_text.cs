using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Microsoft.Win32;

public class System_text : MonoBehaviour
{
    public TMP_InputField Field;
    [SerializeField]
    private TextMeshProUGUI systemText;
    string api_key;

    void Start()
    {
        //var envs = Environment.GetEnvironmentVariables();
        systemText.text = "�ɂイ��傭���Ă�����";
        systemText = GetComponent<TextMeshProUGUI>();
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        
    }
    
    public void OnEndEdit()
    {
        systemText.text = "������Ƃ܂��Ă�";
        string input = Field.GetComponent<TMP_InputField>().text;
        var chatGPTConnection = new ChatGPTConnection(api_key);
        chatGPTConnection.RequestAsync(input);
    }
    public void idol()
    {
        systemText.text = "�ɂイ��傭���Ă�����";
    }
   
}
