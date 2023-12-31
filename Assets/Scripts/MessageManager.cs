using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Microsoft.Win32;


public class MessageManager : MonoBehaviour
{

    public GameObject nameTextObject;
    public GameObject messageTextObject;
    public GameObject inputScreen;
    public GameObject inputButton;

    private Text namec;
    private Text messagec;
    private LogManager logManager;
    private Button inputButtonc;

    // Auto用
    public bool autoMode;
    private float autoTimer;
    public float autoTimeAfterFinish = 1;

    // 順番に出現するテキストのアニメーション用
    private string fullMessage = "この範囲をクリックすると表示が進みます。";
    private float textAnimTimer;
    public float textAnimSpeed = 10;
    private bool isEndTextAnim = false;

    //デバッグ用
    public bool PrintHistoryToLog;
    
    //apikey
    string api_key;
    //loading???p
    Loading loading;
    
    ChatGPTConnection chatGPTConnection;
    //?f?o?b?O?p
    
    public GameObject inputField;
    private InputField inputFieldc;

    // 次以降に表示するメッセージ
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    async void Start()
    {
        // コンポーネント取得
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        logManager = transform.GetComponent<LogManager>();

        inputFieldc = inputField.GetComponent<InputField>();
        inputButtonc = inputButton.GetComponent<Button>();


        futureMessages.Add(new MessageData("説明", "AUTOボタンをクリックするとAUTOモードになり、表示アニメーション終了後1秒で次に移ります。"));
        futureMessages.Add(new MessageData("説明", "LOGボタンをクリックするとこれまでの会話の履歴が表示され、右上のXで戻ります。"));

       
        //?????????o??
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        chatGPTConnection = new ChatGPTConnection(api_key);
        //load?J?n
        loading.Start_load();
        //??????????点??
        await chatGPTConnection.RequestAsync("語りだしを語ってください。");
    }


    // Update is called once per frame
    void Update()
    {
        // 次のメッセージがない場合は入力ボタンをインタラクティブにする
        if (futureMessages.Count == 0)
		{
            inputButtonc.interactable = true;
		}

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

        // 会話履歴のログ出力
        if (PrintHistoryToLog)
        {
            PrintHistoryToLog = false;
            History.PrintLog();
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
        logManager.Add(md);
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

    public void AddFullText(string author,string message)
	{
        // 区切り文字
        if (message.StartsWith("「") && message.EndsWith("」"))
            message = message.Remove(0, 1).Remove(message.Length - 1, 1);
        message = message.Replace("「", "|「");
        message = message.Replace("」", "」|");
        message = message.Replace("\n", "|");
        message = message.Replace("。", "。|");
        message = message.Replace("|」|", "」|");
        message = message.Replace("||", "|");

		foreach (string part in message.Split("|",StringSplitOptions.RemoveEmptyEntries))
		{
            SetMessage(new MessageData(author,part));
		}
	}

    public async void EnterInputScreen()
	{
        // 入力の決定ボタンが押されたら呼ばれる
        
        SetMessage(new MessageData(author: "You", message: inputFieldc.text));

        // ここで inputField.text をChatGPTに送る
       
        loading.Start_load();
        await chatGPTConnection.RequestAsync(inputFieldc.text);

        inputScreen.SetActive(false);
        inputButtonc.interactable = false;
        inputFieldc.text = "";
    }


    public void HideInputScreen()
	{
        inputScreen.SetActive(false);
	}
    public void ShowInputScreen()
	{
        inputScreen.SetActive(true);
	}
    public void ToggleAutoMode()
	{
        autoMode ^= true;
	}
}
