using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private GameObject cam;

    private void Start()
    {
        cam = this.gameObject;
    }

    private void Update()
    {
        cam.transform.position = Vector3.Lerp(new Vector3(0, cam.transform.position.y, cam.transform.position.z), new Vector3(0, player.transform.position.y, cam.transform.position.z), speed);
    }
}
