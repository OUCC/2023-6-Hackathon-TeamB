using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Microsoft.Win32;


public class MessageManager : MonoBehaviour
{

    public GameObject nameTextObject;
    public GameObject messageTextObject;
    public GameObject InputScreen;

    private Text namec;
    private Text messagec;
    private LogManager logManager;

    // Auto用
    public bool autoMode;
    private float autoTimer;
    public float autoTimeAfterFinish = 1;

    // 順番に出現するテキストのアニメーション用
    private string fullMessage = "";
    private float textAnimTimer;
    public float textAnimSpeed = 10;
    private bool isEndTextAnim = false;

    //apikey
    string api_key;
    [SerializeField] private EventSystem eventSystem;
    private GameObject button_ob;
    SelectBotton sb;
    //loading画面用
    Loading loading;
    
    ChatGPTConnection chatGPTConnection;
    //デバッグ用
    public GameObject inputField;
    private InputField inputFieldc;

    // 次以降に表示するメッセージ
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    void Start()
    {
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        // コンポーネント取得
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        //logManager = transform.GetComponent<LogManager>();

        inputFieldc = inputField.GetComponent<InputField>();

        // デバッグ用のメッセージ
        /*futureMessages.Add(new MessageData("A", "aaa"));
        futureMessages.Add(new MessageData("B", "bbbbbbb"));
        futureMessages.Add(new MessageData("C", "ccccccccccccccc"));
        futureMessages.Add(new MessageData("D", "dddddddddddddddddddddd"));*/
       
        //環境変数読み出し
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        chatGPTConnection = new ChatGPTConnection(api_key);
        //語りだしを先に語らせる
        chatGPTConnection.RequestAsync("語りだしを語り、選択肢を提示してください");

        //load開始
        loading.Start_load();

    }


    // Update is called once per frame
    void Update()
    {
        // テキストのアニメーション完了したらAutoのタイマー稼働
		if (isEndTextAnim & autoMode)
		{
            autoTimer += Time.deltaTime;
            if(autoTimer > autoTimeAfterFinish)
			{
                autoTimer = 0;
                DisplayNextMessage();
			}
        }

        // テキストを一文字ずつ順番に表示させる
        if(!isEndTextAnim)
		{
            textAnimTimer += Time.deltaTime;
            int dispTextNum = Mathf.FloorToInt(textAnimTimer * textAnimSpeed);
            messagec.text = fullMessage.Substring(0, Mathf.Min(fullMessage.Length, dispTextNum));
            isEndTextAnim = fullMessage.Length < dispTextNum;
        }
    }


    public void DisplayNextMessage()
	{
        //まだアニメーション中なら表示を完了する
        if(! isEndTextAnim)
		{
            textAnimTimer = fullMessage.Length * textAnimSpeed;
            return;
        }

        //次のメッセージが未決定の場合はなにもしない
        if (futureMessages.Count == 0)
            return;

        //新しいメッセージに変更

        textAnimTimer = 0;
        isEndTextAnim = false;
        //次のメッセージをリストから取り出して更新
        MessageData md = futureMessages[0];
        futureMessages.RemoveAt(0);
        namec.text = md.author;
        fullMessage = md.message;
        //履歴に追加
        History.list.Add(md);
        //logManager.Add(md);
	}

    public void SetMessage(MessageData md)
	{
        futureMessages.Add(md);
	}
    public void SetMessageList(List<MessageData> list)
	{
		foreach (var md in list)
		{
            futureMessages.Add(md);
		}
	}

    public void OnInputButtonClick()
	{
        // 入力の決定ボタンが押されたら呼ばれる

        //SetMessage(new MessageData(author: "B", message: inputFieldc.text));
        
        // ここで inputField.text をChatGPTに送る
       
        chatGPTConnection.RequestAsync(inputFieldc.text);

        loading.Start_load();

    }

    public void OnSwitch()
    {
        
        button_ob = eventSystem.currentSelectedGameObject;
        sb = button_ob.GetComponent<SelectBotton>();
       
        chatGPTConnection.RequestAsync(sb.number.ToString());

        loading.Start_load();
    }

    public void OnShowHistoryButton()
	{
        History.PrintLog();
	}

    public void OnHideInputFieldButtonClick()
	{
        InputScreen.SetActive(false);
	}
    public void OnShowInputFieldButtonClick()
	{
        InputScreen.SetActive(true);
	}
    public void ToggleAutoMode()
	{
        autoMode ^= true;
	}
}
