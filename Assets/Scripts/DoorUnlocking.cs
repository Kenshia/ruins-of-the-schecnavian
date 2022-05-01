using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorUnlocking : MonoBehaviour
{
    public TextMeshPro text;
    public int keyNeeded;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    public GameObject keyNeededObject;

    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private void Start()
    {
        text.text = keyNeeded.ToString();
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
        StartCoroutine(CheckPlayer());
    }
    private IEnumerator CheckPlayer()
    {
        while (true)
        {
            bool nearPlayer = false;
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            foreach (Collider2D collider in collisions)
            {
                if (collider.CompareTag("Player")){
                    nearPlayer = true;
                    break;
                }
            }
            if (nearPlayer)
                keyNeededObject.SetActive(true);
            else
                keyNeededObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public bool CheckKey()
    {
        if (KeyCounter.instance.keyCount >= keyNeeded)
        {
            KeyCounter.instance.UpdateKey(-keyNeeded);
            AudioManager.instance.PlayS("door");
            //ganti ke change sprite / whatever it is
            Destroy(gameObject);
            return true;
        }
        else
        {
            AudioManager.instance.PlayS("lockedDoor");
            return false;
        }
    }
    private void OnReal()
    {
        sr.sprite = real;
    }
    private void OnUnreal()
    {
        sr.sprite = unreal;
    }
}
