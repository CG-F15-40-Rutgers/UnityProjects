using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;
        int scrollDistance = 150;
        float cameraSpeed = 70;


        if (Input.GetKey(KeyCode.Space))
        {
            x = Input.GetAxis("Mouse X");
            
            transform.Rotate(Vector3.up, x * cameraSpeed * Time.deltaTime);
        }
        else
        {
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;
            if (x < scrollDistance)
            {
                transform.Translate(-1, 0, 1);
            }
            if (x >= Screen.width - scrollDistance)
            {
                transform.Translate(1, 0, -1);
            }

            if (y < scrollDistance)
            {
                transform.Translate(-1, 0, -1);
            }

            if (y >= Screen.height - scrollDistance)
            {
                transform.Translate(1, 0, 1);
            }
        }
        
     

        Camera Eye = GetComponentInChildren<Camera>();
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.orthographicSize > 4)
        {
            Eye.orthographicSize -= 4;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.orthographicSize > 4)
        {
            Eye.orthographicSize += 4;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Eye.orthographicSize = 50;
        }
    }
}
