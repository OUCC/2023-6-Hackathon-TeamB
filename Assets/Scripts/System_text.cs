using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Microsoft.Win32;

public class System_text : MonoBehaviour
{
    public InputField legacyField;  
    public Text legacySystemText;
    MessageManager messageManager;
    

    

    //load用のクラス　他のに統合できそうな気もする
    Loading loading;

    void Start()
    {
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        messageManager = GameObject.Find("TextManager").GetComponent<MessageManager>();
        legacySystemText.text = "にゅうりょくしていいよ";
              
    }
    
    public void OnEndEdit()
    {
        
        legacySystemText.text = "ちょっとまってね";
        
    }
    public void idol()
    {
        //load終了
        loading.Finish_load();
        legacySystemText.text = "にゅうりょくしていいよ";
        //GameObject.Find("Input_screen").SetActive(false);
        messageManager.DisplayNextMessage();
    }
   
}
