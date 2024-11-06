using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private float stopVelocity = .05f; //The velocity below which the rigidbody will be considered as stopped

    [SerializeField] private LineRenderer lineRenderer;

    //private bool isIdle;
    private bool isAiming;

    private Rigidbody rb;

    public bool letGo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;
        letGo = false;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0)) { letGo = true; }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        ProcessAim();
    }

    private void OnMouseDown()
    {
            isAiming = true;
    }

    private void ProcessAim()
    {
        if (!isAiming)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);

        if (letGo)
        {
            Shoot(worldPoint.Value);
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        letGo = false;
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rb.AddForce(direction * strength * shotPower);
        //isIdle = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = {
            transform.position,
            worldPoint};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //isIdle = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}
