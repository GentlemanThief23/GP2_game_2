using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForce : MonoBehaviour
{
   [SerializeField] private LineRenderer lineRenderer;

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3 CastMouseClickRay()
    {
        Vector3 screenMouseposFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMouseposNear = new Vector3(
             Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToViewportPoint(screenMouseposFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToViewportPoint(screenMouseposNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositivityInfinty))
        {
            return hit.point;
        }
        else
        {
            return null;
        }

    }
}
