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
<<<<<<< Updated upstream
    string api_key;
    

    void Start()
    {
        //��b������ێ����郊�X�g
        /*var cc = new ChatGPTConnection();
        cc._messageList = new();*/
        legacySystemText.text = "にゅうりょくしていいよ";
        
        
=======
    //load用のクラス　他のに統合できそうな気もする
    Loading loading;

    void Start()
    {
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        legacySystemText.text = "にゅうりょくしていいよ";
               
>>>>>>> Stashed changes
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
    }
   
}
