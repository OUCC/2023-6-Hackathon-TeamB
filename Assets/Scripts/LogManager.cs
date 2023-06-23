using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{

    public GameObject logScrollView;
    public GameObject parentContent;
    public GameObject LogUnitPrefab;
    private RectTransform parentT;
    private Transform top;

    private float logUnitHeight;
    public float yMergin = 20;
    public Vector2 instantiatePosition;

    // Start is called before the first frame update
    void Start()
    {
        parentT = parentContent.GetComponent<RectTransform>();
        logUnitHeight = LogUnitPrefab.GetComponent<RectTransform>().rect.height;
        top = parentContent.transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(MessageData md)
	{
        RectTransform logUnit = Instantiate(LogUnitPrefab,top).GetComponent<RectTransform>();
        logUnit.anchoredPosition = instantiatePosition;
        Text logName = logUnit.GetChild(0).GetComponent<Text>();
        Text logMessage = logUnit.GetChild(1).GetComponent<Text>();
        logName.text = md.author;
        logMessage.text = md.message;

        instantiatePosition.y -= logUnitHeight + yMergin;

        parentT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentT.rect.height + logUnitHeight + yMergin);

        top.SetLocalPositionAndRotation(new Vector3(parentT.rect.width/2, -logUnitHeight, 0),Quaternion.identity);
    }

    public void ShowLogView()
	{
        logScrollView.SetActive(true);
	}
    public void HideLogView()
	{
        logScrollView.SetActive(false);
	}
}
