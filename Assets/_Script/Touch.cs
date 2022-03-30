using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public GameObject Explosion;
    public LayerMask Mask;

    public GameObject selectedOBJ = null;
    Vector3 offset;

    Plane planeOBj;

    void Update()
    {
        Vector3 Far_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 Near_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 _Far = Camera.main.ScreenToWorldPoint(Far_mousePos);    //needed to do it in 2 steps
        Vector3 _Near = Camera.main.ScreenToWorldPoint(Near_mousePos);    //cz ScreenToWorldPoint() doesn't take three parameter

        Debug.DrawRay(_Near, _Far - _Near, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;

//================================================== EXPLOSION =====================================================
            if(Physics.Raycast(_Near, _Far - _Near, out hitInfo, Mask))//___Explodes on Click                    //=
            {                                                                                                    //=
                Instantiate(Explosion, hitInfo.transform.position, Quaternion.identity);                         //=
                Destroy(hitInfo.transform.gameObject);                                                           //=
            }                                                                                                    //=
//================================================== EXPLOSION =====================================================

            else if (Physics.Raycast(_Near, _Far - _Near, out hitInfo))//____Moves on Click
            {
                selectedOBJ = hitInfo.transform.gameObject;


                planeOBj = new Plane(Camera.main.transform.forward * -1, selectedOBJ.transform.position);//___A Vertual Plane..
                        //_____ Camera.main.transform.forward * -1 means plane is facing towards the camera........and is in front of the camera


                //__Lines below are for calculating offset
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float enter;
                planeOBj.Raycast(ray, out enter);
                offset = selectedOBJ.transform.position - ray.GetPoint(enter);
                //________________________________Bcoz of offset obj wont move when mouse is clicked;
            }
        }
        else if(Input.GetMouseButton(0) && selectedOBJ)
        {
            /***            THIS IS FOR ORTHOGRAPHIC VIEW
             * Vector3 K = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             * selectedOBJ.transform.position = new Vector3(K.x, K.y, selectedOBJ.transform.position.z);
            ***/

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.5f;
            if(planeOBj.Raycast(ray, out enter))
            {
                selectedOBJ.transform.position = ray.GetPoint(enter) + offset;
            }
        }
        else if(Input.GetMouseButtonUp(0) && selectedOBJ)
        {
            selectedOBJ = null;
        }
    }
}
