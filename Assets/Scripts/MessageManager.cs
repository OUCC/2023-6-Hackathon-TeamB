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

    // Auto�p
    public bool autoMode;
    private float autoTimer;
    public float autoTimeAfterFinish = 1;

    // ���Ԃɏo������e�L�X�g�̃A�j���[�V�����p
    private string fullMessage = "���͈̔͂��N���b�N����ƕ\�����i�݂܂��B";
    private float textAnimTimer;
    public float textAnimSpeed = 10;
    private bool isEndTextAnim = false;

    //�f�o�b�O�p
    
    //apikey
    string api_key;
    //loading???p
    Loading loading;
    
    ChatGPTConnection chatGPTConnection;
    //?f?o?b?O?p
    
    public GameObject inputField;
    private InputField inputFieldc;

    // ���ȍ~�ɕ\�����郁�b�Z�[�W
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    async void Start()
    {
        // �R���|�[�l���g�擾
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        logManager = transform.GetComponent<LogManager>();

        inputFieldc = inputField.GetComponent<InputField>();
        inputButtonc = inputButton.GetComponent<Button>();

        // �f�o�b�O�p�̃��b�Z�[�W
        futureMessages.Add(new MessageData("����", "�@�@�@�@�@�@�@�@�@�@�@�@�@�@��\nAUTO�{�^�����N���b�N�����AUTO���[�h�ɂȂ�A\n�\���A�j���[�V�����I����1�b�Ŏ��Ɉڂ�܂��B"));
        futureMessages.Add(new MessageData("����", "�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@��\nLOG�{�^�����N���b�N����Ƃ���܂ł̉�b�̗������\������A�E���?�Ŗ߂�܂��B"));

        // ?f?o?b?O?p????b?Z?[?W
        futureMessages.Add(new MessageData("A", "aaa"));
        futureMessages.Add(new MessageData("B", "bbbbbbb"));
        futureMessages.Add(new MessageData("C", "ccccccccccccccc"));
        futureMessages.Add(new MessageData("D", "dddddddddddddddddddddd"));
       
        //?????????o??
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        chatGPTConnection = new ChatGPTConnection(api_key);
        //load?J?n
        loading.Start_load();
        //??????????�_??
        await chatGPTConnection.RequestAsync("��肾��������Ă��������B");
    }


    // Update is called once per frame
    void Update()
    {
        // ���̃��b�Z�[�W���Ȃ��ꍇ�͓��̓{�^�����C���^���N�e�B�u�ɂ���
        if (futureMessages.Count == 0)
		{
            inputButtonc.interactable = true;
		}

        // �e�L�X�g�̃A�j���[�V��������������Auto�̃^�C�}�[�ғ�
		if (isEndTextAnim & autoMode)
		{
            autoTimer += Time.deltaTime;
            if(autoTimer > autoTimeAfterFinish)
			{
                autoTimer = 0;
                DisplayNextMessage();
			}
        }

        // �e�L�X�g���ꕶ�������Ԃɕ\��������
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
        //�܂��A�j���[�V�������Ȃ�\������������
        if(! isEndTextAnim)
		{
            textAnimTimer = fullMessage.Length * textAnimSpeed;
            return;
        }

        //���̃��b�Z�[�W��������̏ꍇ�͂Ȃɂ����Ȃ�
        if (futureMessages.Count == 0)
            return;

        //�V�������b�Z�[�W�ɕύX

        textAnimTimer = 0;
        isEndTextAnim = false;
        //���̃��b�Z�[�W�����X�g������o���čX�V
        MessageData md = futureMessages[0];
        futureMessages.RemoveAt(0);
        namec.text = md.author;
        fullMessage = md.message;
        //�����ɒǉ�
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

    public async void EnterInputScreen()
	{
        // ���͂̌���{�^���������ꂽ��Ă΂��
        
        SetMessage(new MessageData(author: "You", message: inputFieldc.text));
        inputFieldc.text = "";

        // ������ inputField.text ��ChatGPT�ɑ���
       
        loading.Start_load();
        await chatGPTConnection.RequestAsync(inputFieldc.text);

        inputScreen.SetActive(false);
        inputButtonc.interactable = false;
    }

    public void PrintHistoryToLog()
	{
        History.PrintLog();
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
