using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rigid_body;
    public Transform head;
    public Camera camera;


    [Header("configurations")]
    public float walk_speed;
    public float run_speed;

    [Header("Accessed Elsewhere")]
    public bool inRange;
    public bool vPressed = false;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
    }




    private void FixedUpdate()
    {
        Vector3 new_velocity = Vector3.up * rigid_body.linearVelocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? run_speed : walk_speed;
        new_velocity.x = Input.GetAxis("Horizontal") * speed;
        new_velocity.z = Input.GetAxis("Vertical") * speed;
        rigid_body.linearVelocity = transform.TransformDirection(new_velocity);
    }

    private void LateUpdate()
    {
        // vertical rotation
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;
    }

    public static float RestrictAngle(float angle, float angle_min, float angle_max)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if (angle > angle_max)
            angle = angle_max;
        if (angle < angle_min)
            angle = angle_min;

        return angle;
    }
}
