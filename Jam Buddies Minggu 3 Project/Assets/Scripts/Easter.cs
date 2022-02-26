using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easter : MonoBehaviour
{
    public int timesToPress;
    public GameObject objectToShow;

    private void Start()
    {
        objectToShow.SetActive(false);
    }

    public void Pressed()
    {
        timesToPress--;
        if (timesToPress <= 0)
        {
            objectToShow.SetActive(true);
        }
    }
}
