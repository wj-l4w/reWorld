using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float smoothing;
    public Vector3 offset;

    public void moveCamera()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
        transform.position = newPosition;
    }

}
