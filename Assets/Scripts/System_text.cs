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
        systemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
        systemText = GetComponent<TextMeshProUGUI>();
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        
    }
    
    public void OnEndEdit()
    {
        systemText.text = "‚¿‚å‚Á‚Æ‚Ü‚Á‚Ä‚Ë";
        string input = Field.GetComponent<TMP_InputField>().text;
        var chatGPTConnection = new ChatGPTConnection(api_key);
        chatGPTConnection.RequestAsync(input);
    }
    public void idol()
    {
        systemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
    }
   
}
