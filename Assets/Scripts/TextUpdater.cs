using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextUpdater : MonoBehaviour
{

    public GameObject nameTextObject;
    public GameObject messageTextObject;
    public GameObject InputScreen;

    private Text namec;
    private Text messagec;

    // Auto用
    public bool autoMode;
    private float autoTimer;
    public float autoTimeAfterFinish = 1;

    // 順番に出現するテキストのアニメーション用
    private string fullMessage = "";
    private float textAnimTimer;
    public float textAnimSpeed = 10;
    private bool isEndTextAnim = false;

    //デバッグ用
    public GameObject inputField;
    private InputField inputFieldc;

    // 次以降に表示するメッセージ
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();

        inputFieldc = inputField.GetComponent<InputField>();

        // デバッグ用のメッセージ
        futureMessages.Add(new MessageData("A", "aaa"));
        futureMessages.Add(new MessageData("B", "bbbbbbb"));
        futureMessages.Add(new MessageData("C", "ccccccccccccccc"));
        futureMessages.Add(new MessageData("D", "dddddddddddddddddddddd"));
        futureMessages.Add(new MessageData("E", "eeeeeeeeeeeeeeeeeeeeeeeeeeeee"));

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

        SetMessage(new MessageData(author: "B", message: inputFieldc.text));
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
