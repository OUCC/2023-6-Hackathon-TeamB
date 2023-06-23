using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


[Serializable]
public class ChatGPTMessageModel
{
    public string role;
    public string content;

}

//ChatGPT API��Request�𑗂邽�߂�JSON�p�N���X
[Serializable]
public class ChatGPTCompletionRequestModel
{
    public string model;
    public List<ChatGPTMessageModel> messages;
}

//ChatGPT API�����Response���󂯎�邽�߂̃N���X
[System.Serializable]
public class ChatGPTResponseModel
{
    public string id;
    public string @object;
    public int created;
    public Choice[] choices;
    public Usage usage;


    [ System.Serializable]
    public class Choice
    {
        public int index;
        public ChatGPTMessageModel message;
        public string finish_reason;
    }

    [System.Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}


public class ChatGPTConnection
{
	//Text text;
	private readonly string _apiKey;
	public List<ChatGPTMessageModel> _messageList = new();

	MessageManager _mmanager;


	public ChatGPTConnection(string apiKey)
	{
		_mmanager = GameObject.Find("Manager").GetComponent<MessageManager>();
		_apiKey = apiKey;
		_messageList.Add(
			new ChatGPTMessageModel()
			{
				role = "system",
				content = "あなたは物語の語り手です。「" + TitleSingleton.instance.title + "」という作品を創作し、語ってください。また、次の条件を守ってください。" +
			"条件１：会話ごとに相手に選択をゆだねてください。" +
			"条件２：相手の返答によって物語に変化を加えてください。"
			});

	}

	public async UniTask<ChatGPTResponseModel> RequestAsync(string userMessage)
	{
		//���͐���AI��API�̃G���h�|�C���g��ݒ�
		var apiUrl = "https://api.openai.com/v1/chat/completions";

		_messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

		//OpenAI��API���N�G�X�g�ɕK�v�ȃw�b�_�[����ݒ�
		var headers = new Dictionary<string, string>
			{
				{"Authorization", "Bearer " + _apiKey},
				{"Content-type", "application/json"},
				{"X-Slack-No-Retry", "1"}
			};

		//���͐����ŗ��p���郂�f����g�[�N������A�v�����v�g���I�v�V�����ɐݒ�
		var options = new ChatGPTCompletionRequestModel()
		{
			model = "gpt-3.5-turbo",
			messages = _messageList
		};
		var jsonOptions = JsonUtility.ToJson(options);

		Debug.Log("自分:" + userMessage);

		//OpenAI�̕��͐���(Completion)��API���N�G�X�g�𑗂�A���ʂ�ϐ��Ɋi�[
		using var request = new UnityWebRequest(apiUrl, "POST")
		{
			uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
			downloadHandler = new DownloadHandlerBuffer()
		};

		foreach (var header in headers)
		{
			request.SetRequestHeader(header.Key, header.Value);
		}

		await request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError ||
			request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
			throw new Exception();
		}
		else
		{
			var responseString = request.downloadHandler.text;
			var responseObject = JsonUtility.FromJson<ChatGPTResponseModel>(responseString);

			Debug.Log("ChatGPT:" + responseObject.choices[0].message.content);

			// 返答を これから表示するリスト に追加する
			_mmanager.AddFullText(author: "ChatGPT", message: responseObject.choices[0].message.content);


			GameObject.Find("systemText").GetComponent<System_text>().idol();
			_messageList.Add(responseObject.choices[0].message);

			return responseObject;
		}
	}
}
