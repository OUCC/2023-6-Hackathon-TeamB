using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//load��ʗp�̃p�l�����Ǘ�����
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
