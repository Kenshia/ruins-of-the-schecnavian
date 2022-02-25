using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyUI : MonoBehaviour
{
    public Vector3 endingPos;
    private void OnEnable()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 400f, transform.localPosition.z);
        StartCoroutine(Fancy());
    }
    private IEnumerator Fancy()
    {
        while(transform.position != endingPos)
        {
            transform.localPosition += (endingPos - transform.localPosition) / 5f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
