using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform playerTra;
    [SerializeField] Vector3 offset;

    [SerializeField] bool lockX;
    [SerializeField] bool lockY;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.zero;

        if (!lockX) newPos.x = playerTra.position.x;
        if (!lockY) newPos.y = playerTra.position.y;
        newPos.z = playerTra.position.z;
        transform.position = newPos + offset;

    }
}
