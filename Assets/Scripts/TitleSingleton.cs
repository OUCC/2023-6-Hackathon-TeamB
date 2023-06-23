using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSingleton : MonoBehaviour
{
    public static TitleSingleton instance;
    public static TitleSingleton Instance => instance;
    //�^�C�g���ێ��p�̕ϐ�
    public string title;
    public GameObject inputField;
    InputField InputField_title;

    void Awake()
    {
        InputField_title = inputField.GetComponent<InputField>();
        //���łɃC���X�^���X������Δj��@�v���C������
        if (instance && this != instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void OnStartButtonClick()
    {
        //���̓i�V�Ȃ���������͂������������@��肭�����ĂȂ�
        if (InputField_title.text == null)
        {
            Debug.Log("���������͂���");
        }
        else
        {
            title = InputField_title.text;
            SceneManager.LoadScene("MainScene");
        }
    }
    public void SceneLoader()
    {
        title = InputField_title.text;
        SceneManager.LoadScene("MainScene");
    }
}
