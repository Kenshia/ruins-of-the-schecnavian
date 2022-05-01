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
        if(timesToPress > 0)
        {
            timesToPress--;
            if(timesToPress != 0) AudioManager.instance.PlayS("miniPop");
        }
        if (timesToPress == 0)
        {
            timesToPress = -1;
            objectToShow.SetActive(true);
            AudioManager.instance.PlayS("pop");
        }
    }
}
