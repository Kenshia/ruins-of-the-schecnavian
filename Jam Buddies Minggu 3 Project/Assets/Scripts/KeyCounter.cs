using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    public static KeyCounter instance;
    public bool levelContainsKey;
    public int keyCount;
    public GameObject keyCountObject;
    private TextMeshProUGUI keyCountText;
    

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (levelContainsKey)
            keyCountObject.SetActive(true);
        else
            keyCountObject.SetActive(false);
        keyCountText = keyCountObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateKey(int key)
    {
        keyCount += key;
        keyCountText.text = keyCount.ToString();
    }
}
