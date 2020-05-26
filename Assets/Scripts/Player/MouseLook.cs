using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private float roll_Angle = 10f;

    [SerializeField]
    private float roll_Speed = 3f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70f, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    private float current_Roll_Angle;
    private float upRecoil;
    private float sideRecoil;

    // Use this for initialization
    void Start()
    {
        //GameManager.Instance.setMenu(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked && !player.isDead && !player.isOutOfFocus)
        {
            LookAround();
        }

    }

    public void ApplyRecoil(float up, float side)
    {
        upRecoil += up;
        sideRecoil += side;
    }

    void LockAndUnlockCursor()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.L))
        {
           
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                //GameManager.Instance.setMenu(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                //GameManager.Instance.setMenu(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
            }

        }

    }

    void LookAround()
    {
        current_Mouse_Look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        look_Angles.x += upRecoil + current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += sideRecoil + current_Mouse_Look.y * sensivity;
        upRecoil = 0;
        sideRecoil = 0;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, 
            Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_Angle, 
            Time.deltaTime * roll_Speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime, Transform transform)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

}