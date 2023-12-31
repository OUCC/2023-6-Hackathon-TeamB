using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//load画面用のパネルを管理する
public class Loading : MonoBehaviour
{
    [SerializeField]
    GameObject loadingPanel;

    public void Start_load()
    {
        loadingPanel.SetActive(true);
    }

    public void Finish_load()
    {
        loadingPanel?.SetActive(false);
    }
}
