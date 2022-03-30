using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCtrl : MonoBehaviour
{

    public GameObject selectedObj;

    Plane xPlane;

    Vector3 lastPos;
    Vector3 Velocity;

    void Update()
    {
        Vector3 Far_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 Near_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 _Far = Camera.main.ScreenToWorldPoint(Far_mousePos);
        Vector3 _Near = Camera.main.ScreenToWorldPoint(Near_mousePos);

        Debug.DrawRay(_Near, _Far - _Near, Color.yellow);

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray rayOrigin = new Ray(_Near, _Far - _Near);

            if(Physics.Raycast(rayOrigin, out hitInfo) && hitInfo.transform.gameObject.tag.Equals("Dice"))
            {
                selectedObj = hitInfo.transform.gameObject;

                xPlane = new Plane(Vector3.up, selectedObj.transform.position);
            }
        }
        else if(Input.GetMouseButton(0) && selectedObj)
        {
            //Vector3 X = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //selectedObj.transform.position = new Vector3(X.x, selectedObj.transform.position.y, X.z);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter;
            if(xPlane.Raycast(ray, out enter))
            {
                selectedObj.transform.position = ray.GetPoint(enter);

                Velocity = selectedObj.transform.position - lastPos;
                lastPos = selectedObj.transform.position;
            }

        }
        else if(Input.GetMouseButtonUp(0))
        {
            selectedObj.GetComponent<Rigidbody>().AddForce(Velocity * 1000);
            selectedObj = null;
        }
    }
}
