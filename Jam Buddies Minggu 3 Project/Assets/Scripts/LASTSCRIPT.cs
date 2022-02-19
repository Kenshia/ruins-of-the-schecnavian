using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASTSCRIPT : MonoBehaviour
{
    public GameObject textToShow;
    private bool a;

    private void Start()
    {
        a = false;
    }
    public void ShowText()
    {
        a = true;
        textToShow.SetActive(true);
    }

    private void Update()
    {
        if (a)
        {
            if (Input.anyKey)
            {
                a = false;
                textToShow.SetActive(false);
            }
        }
    }
}
