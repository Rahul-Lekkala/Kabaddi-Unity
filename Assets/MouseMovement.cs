using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    bool invert;

    [SerializeField]
    bool can_unlock = true;

    [SerializeField]
    float sensitivity = 5f;

    [SerializeField]
    int smooth_steps = 10;

    [SerializeField]
    float smooth_weight = 0.4f;

    [SerializeField]
    float roll_angle = 10f;

    [SerializeField]
    float roll_speed = 0f;

    Vector2 default_look_limits = new Vector2(-70f, 80f);
    Vector2 look_angles;
    Vector2 current_mouse_look;
    Vector2 smooth_move;
    float current_roll_angle;
    int last_look_frame;

    [SerializeField]
    Transform lookroot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        //LookAround();
    }

    void LookAround()
    {
        current_mouse_look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        look_angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
        look_angles.y += current_mouse_look.y * sensitivity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_look_limits.x, default_look_limits.y);
        current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw("Mouse X") * roll_angle, Time.deltaTime * roll_speed);

        lookroot.localRotation = Quaternion.Euler(look_angles.x,look_angles.y,current_roll_angle);
        
    }
}
