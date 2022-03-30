using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_with_Swipe : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButton(0))
        {
            Plane xPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float enter;

            if(xPlane.Raycast(ray, out enter))
            {
                transform.position = ray.GetPoint(enter);
            }
        }
    }
}
