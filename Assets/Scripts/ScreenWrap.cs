using UnityEngine;
using System.Collections;

public class ScreenWrap : MonoBehaviour
{

    float leftConstraint = 0.0f;
    float rightConstraint = 960.0f;
    float buffer = 1.0f;
    public Camera cam;
    float distanceZ;


    void Start()
    {
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
		cam = FindObjectOfType<Camera> ();
    }

    void FixedUpdate()
    {
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        }
    }
}