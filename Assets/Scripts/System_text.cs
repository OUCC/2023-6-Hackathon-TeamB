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
        //��b������ێ����郊�X�g
        /*var cc = new ChatGPTConnection();
        cc._messageList = new();*/
        legacySystemText.text = "にゅうりょくしていいよ";
        
        
    }
    
    public void OnEndEdit()
    {
        
        legacySystemText.text = "ちょっとまってね";
        
    }
    public void idol()
    {
        //systemText.text = "�ɂイ��傭���Ă�����";
        legacySystemText.text = "にゅうりょくしていいよ";
    }
   
}
