using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//load‰æ–Ê—p‚Ìƒpƒlƒ‹‚ğŠÇ—‚·‚é
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
