using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Drive : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotSpeed = 30f;

    void FixedUpdate()
    {
        float vertic = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float horizon = CrossPlatformInputManager.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.Translate(0, 0, vertic);
        if(vertic != 0)            transform.Rotate(0, horizon, 0);
    }
}
