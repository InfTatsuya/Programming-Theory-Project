using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
       Camera.main.transform.position = player.transform.position + offset;
    }
}
