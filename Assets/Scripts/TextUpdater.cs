using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextUpdater : MonoBehaviour
{

    public GameObject nameTextObject;
    public GameObject messageTextObject;

    private Text namec;
    private Text messagec;

    public GameObject inputFieldB;
    private InputField inputFieldc;

    // Start is called before the first frame update
    void Start()
    {
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        inputFieldc = inputFieldB.GetComponent<InputField>();

        messagec.text = "Started";
    }

    public List<MessageData> futureMessages = new List<MessageData>();

    // Update is called once per frame
    void Update()
    {

    }


    public void DisplayNextMessage()
	{
        if (futureMessages.Count == 0)
            return;

        MessageData md = futureMessages[0];
        futureMessages.RemoveAt(0);

        namec.text = md.author;
        messagec.text = md.message;

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

    public void OnInputTextSubmit(string text)
	{
        SetMessage(new MessageData(author: "A", message: text));
	}
    public void OnInputButtonClick()
	{

        SetMessage(new MessageData(author: "B", message: inputFieldc.text));
	}

    public void OnShowHistoryButton()
	{
        History.PrintLog();
	}

}
