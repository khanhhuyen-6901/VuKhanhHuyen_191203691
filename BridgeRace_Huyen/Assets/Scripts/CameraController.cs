using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 cameraPoint;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isFinish == false)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cameraPoint, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
