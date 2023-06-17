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
    public InputField legacyField;
    [SerializeField]
    private TextMeshProUGUI systemText;
    public Text legacySystemText;
    string api_key;

    void Start()
    {
        //var envs = Environment.GetEnvironmentVariables();
        //systemText = GetComponent<TextMeshProUGUI>();
        //systemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
        legacySystemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        
    }
    
    public void OnEndEdit()
    {
        //systemText.text = "‚¿‚å‚Á‚Æ‚Ü‚Á‚Ä‚Ë";
        //string input = Field.GetComponent<TMP_InputField>().text;
        legacySystemText.text = "‚¿‚å‚Á‚Æ‚Ü‚Á‚Ä‚Ë";
        string input = legacyField.GetComponent<InputField>().text;

        var chatGPTConnection = new ChatGPTConnection(api_key);
        chatGPTConnection.RequestAsync(input);
    }
    public void idol()
    {
        //systemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
        legacySystemText.text = "‚É‚ã‚¤‚è‚å‚­‚µ‚Ä‚¢‚¢‚æ";
    }
   
}
