using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.gameObject.name == "Player")
        {
            transform.position = new Vector3(target.position.x, target.transform.position.y, transform.position.z);
        }
        else if (target.gameObject.name == "Airplane")
        {
            transform.position = new Vector3(target.position.x, 0, transform.position.z);
        }
    }
}
