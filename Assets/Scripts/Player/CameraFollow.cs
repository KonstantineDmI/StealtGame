using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private float _smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private float yDist = 8f; // y distantion from the player
    private float zDist = 6f; // z distantion from the player
    private void Update()
    {
        Vector3 cameraPos = new Vector3(_player.position.x, _player.position.y + yDist, _player.position.z - zDist);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, _smoothTime);
    }
}
