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

    // ���Ԃɏo������e�L�X�g�̃A�j���[�V�����p
    private string fullMessage = "";
    private float textAnimTimer;
    public float textAnimSpeed = 10;

    //�f�o�b�O�p
    public GameObject inputFieldB;
    private InputField inputFieldc;

    // ���ȍ~�ɕ\�����郁�b�Z�[�W
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�擾
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();

        inputFieldc = inputFieldB.GetComponent<InputField>();

        // �f�o�b�O�p�̃��b�Z�[�W
        futureMessages.Add(new MessageData("A", "aaa"));
        futureMessages.Add(new MessageData("B", "bbbbbbb"));
        futureMessages.Add(new MessageData("C", "ccccccccccccccc"));
        futureMessages.Add(new MessageData("D", "dddddddddddddddddddddd"));
        futureMessages.Add(new MessageData("E", "eeeeeeeeeeeeeeeeeeeeeeeeeeeee"));

    }


    // Update is called once per frame
    void Update()
    {
        // �e�L�X�g���ꕶ�������Ԃɕ\��������
        textAnimTimer += Time.deltaTime;
        messagec.text = fullMessage.Substring(0,Mathf.Min(fullMessage.Length, Mathf.FloorToInt(textAnimTimer * textAnimSpeed)));
    }


    public void DisplayNextMessage()
	{
        //�܂��A�j���[�V�������Ȃ�\������������
        if(fullMessage.Length > Mathf.FloorToInt(textAnimTimer * textAnimSpeed))
		{
            textAnimTimer = fullMessage.Length * textAnimSpeed;
            return;
        }

        //���̃��b�Z�[�W��������̏ꍇ�͂Ȃɂ����Ȃ�
        if (futureMessages.Count == 0)
            return;

        //�V�������b�Z�[�W�ɕύX

        textAnimTimer = 0;
        //���̃��b�Z�[�W�����X�g������o���čX�V
        MessageData md = futureMessages[0];
        futureMessages.RemoveAt(0);
        namec.text = md.author;
        fullMessage = md.message;
        //�����ɒǉ�
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
