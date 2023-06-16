using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class System_text : MonoBehaviour
{
    public TMP_InputField Field;
    [SerializeField]
    private TextMeshProUGUI systemText;
    

    void Start()
    {
        systemText.text = "にゅうりょくしていいよ";
        systemText = GetComponent<TextMeshProUGUI>();
        
    }
    
    public void OnEndEdit()
    {
        systemText.text = "ちょっとまってね";
        string input = Field.GetComponent<TMP_InputField>().text;
        var chatGPTConnection = new ChatGPTConnection("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");
        chatGPTConnection.RequestAsync(input);
    }
    public void idol()
    {
        systemText.text = "にゅうりょくしていいよ";
    }
   
}
