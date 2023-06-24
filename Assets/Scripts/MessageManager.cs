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

    // Auto�p
    public bool autoMode;
    private float autoTimer;
    public float autoTimeAfterFinish = 1;

    // ���Ԃɏo������e�L�X�g�̃A�j���[�V�����p
    private string fullMessage = "";
    private float textAnimTimer;
    public float textAnimSpeed = 10;
    private bool isEndTextAnim = false;

    //apikey
    string api_key;
    [SerializeField] private EventSystem eventSystem;
    private GameObject button_ob;
    SelectBotton sb;
    //loading��ʗp
    Loading loading;
    
    ChatGPTConnection chatGPTConnection;
    //�f�o�b�O�p
    public GameObject inputField;
    private InputField inputFieldc;

    // ���ȍ~�ɕ\�����郁�b�Z�[�W
    public List<MessageData> futureMessages = new List<MessageData>();

    // Start is called before the first frame update
    void Start()
    {
        loading = GameObject.Find("LoadingCanvas").GetComponent<Loading>();
        // �R���|�[�l���g�擾
        namec = nameTextObject.GetComponent<Text>();
        messagec = messageTextObject.GetComponent<Text>();
        //logManager = transform.GetComponent<LogManager>();

        inputFieldc = inputField.GetComponent<InputField>();

        // �f�o�b�O�p�̃��b�Z�[�W
        /*futureMessages.Add(new MessageData("A", "aaa"));
        futureMessages.Add(new MessageData("B", "bbbbbbb"));
        futureMessages.Add(new MessageData("C", "ccccccccccccccc"));
        futureMessages.Add(new MessageData("D", "dddddddddddddddddddddd"));*/
       
        //���ϐ��ǂݏo��
        api_key = Environment.GetEnvironmentVariable("API_key", EnvironmentVariableTarget.User);
        chatGPTConnection = new ChatGPTConnection(api_key);
        //��肾�����Ɍ�点��
        chatGPTConnection.RequestAsync("��肾�������A�I������񎦂��Ă�������");

        //load�J�n
        loading.Start_load();

    }


    // Update is called once per frame
    void Update()
    {
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
        // ���͂̌���{�^���������ꂽ��Ă΂��

        //SetMessage(new MessageData(author: "B", message: inputFieldc.text));
        
        // ������ inputField.text ��ChatGPT�ɑ���
       
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
