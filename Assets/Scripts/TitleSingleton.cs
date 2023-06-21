using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSingleton : MonoBehaviour
{
    static TitleSingleton instance;
    public static TitleSingleton Instance => instance;
    //タイトル保持用の変数
    public string title;
    public GameObject inputField;
    InputField InputField_title;

    void Awake()
    {
        InputField_title = inputField.GetComponent<InputField>();
        //すでにインスタンスがあれば破壊　要らん気がする
        if (instance && this != instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void OnStartButtonClick()
    {
        //入力ナシならもう一回入力させたかった　上手くいってない
        if (InputField_title.text == null)
        {
            Debug.Log("もう一回入力しろ");
        }
        else
        {
            title = InputField_title.text;
            SceneManager.LoadScene("MainScene");
        }
    }
<<<<<<< Updated upstream
=======
    public void SceneLoader()
    {
        title = InputField_title.text;
        SceneManager.LoadScene("MainScene");
    }
>>>>>>> Stashed changes
}
