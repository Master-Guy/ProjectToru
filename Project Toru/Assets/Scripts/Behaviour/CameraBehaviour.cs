using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target = null;

    public float smoothing;

    public float zoomDistance;
    public float minZoomDistance = 6;
    public float maxZoomDistance = 10;

    public Vector2 topLeft;
    public Vector2 bottomRight;

    public static bool freeLook;

    private Vector3 change;

    void Update()
    {
        Move();
        Zoom();

        if (!freeLook)
        {
            if (Character.selectedCharacter != null)
            {
                target = Character.selectedCharacter.transform;
            }
        }

        Vector3 borderCheck = CheckBorders();
        if (borderCheck != transform.position)
        {
            transform.position = borderCheck;
        }

    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetVector = new Vector3(target.position.x, target.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetVector, smoothing);
            }
        }
    }

    void Move()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero)
        {
            if (!freeLook)
            {
                freeLook = true;
            }
            if (target != null)
            {
                target = null;
            }
            transform.position += change * Time.deltaTime * 15;
        }
    }

    void Zoom()
    {
        zoomDistance -= Input.mouseScrollDelta.y * Time.deltaTime * 30;
        zoomDistance = Mathf.Clamp(zoomDistance, minZoomDistance, maxZoomDistance);
        GetComponent<Camera>().orthographicSize = zoomDistance;
    }

    Vector3 CheckBorders()
    {
        Vector3 border = Vector3.zero;
        border.x = Mathf.Clamp(transform.position.x, topLeft.x, bottomRight.x);
        border.y = Mathf.Clamp(transform.position.y, bottomRight.y, topLeft.y);
        border.z = transform.position.z;

        return border;
    }
}
