using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTouch : MonoBehaviour
{
    public Text textObj;

    GameObject selected_obj;
    Plane xPlane;

    void Update()
    {
        textObj.text = Input.touchCount.ToString();

        Vector3 touchPos = Input.GetTouch(1).position;

        Vector3 _Far = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane));
        Vector3 _Near = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));


        if(Input.GetTouch(1).phase == TouchPhase.Began)
        {
            Ray ray = new Ray(_Near, _Far - _Near);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo))
            {
                selected_obj = hitInfo.transform.gameObject;

                xPlane = new Plane(Camera.main.transform.forward, selected_obj.transform.position);
            }
        }
        else if(Input.GetTouch(1).phase == TouchPhase.Moved && selected_obj)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float enter;

            if(xPlane.Raycast(ray, out enter))
            {
                selected_obj.transform.position = ray.GetPoint(enter);
            }
        }
        else if(Input.GetTouch(1).phase == TouchPhase.Ended && selected_obj)
        {
            selected_obj = null;
        }
    }
}
