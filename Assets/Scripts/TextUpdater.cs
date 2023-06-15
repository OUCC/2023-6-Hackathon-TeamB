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
    private InputField InputFieldc;

    // Start is called before the first frame update
    void Start()
    {
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        InputFieldc = inputFieldB.GetComponent<InputField>();

        messagec.text = "Started";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string author, string message)
	{
        namec.text = author;
        messagec.text = message;

        History.list.Add(new History.Data(author:author, message:message));
	}

    public void OnInputTextSubmit(string text)
	{
        SetMessage(author: "A", message: text);
	}
    public void OnInputButtonClick()
	{

        SetMessage(author: "B", message: InputFieldc.text);
	}

    public void OnShowHistoryButton()
	{
        History.PrintLog();
	}

}
