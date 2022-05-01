using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform player;
    private Player playerScript;
    private Transform cam;
    private Vector2 desiredPosition, smoothedPosition;

    private void Start()
    {
        playerScript = player.gameObject.GetComponent<Player>();
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        //cam.position = new Vector3(player.position.x, player.position.y, cam.position.z);

    }

    private void FixedUpdate()
    {
        desiredPosition = playerScript.nextPos;
        smoothedPosition = Vector2.Lerp(player.position, desiredPosition, 0.02f);
        cam.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cam.transform.position.z);
    }
}
