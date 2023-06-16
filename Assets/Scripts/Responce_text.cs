using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Responce_text : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI responceText;

    void Start()
    {
        responceText = GetComponent<TextMeshProUGUI>();
    }
    public void responces(string res)
    {
        Debug.Log(res);
        responceText.text = res;
        
    }
}