using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private new Rigidbody rigidbody;
    private int jumpCount = 0;
    private int maxJumpCount = 2;
    private bool isMove;
    public float useStamina;

    public bool IsMove { get { return isMove; } }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.Instance.Player.condition.StopMove() == true)
        {
            Move();
        }
    }


    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;
        rigidbody.velocity = dir;

    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // 문제점 발생. 키을 입력받고 계속 움직임.
        if (context.phase == InputActionPhase.Started)
        {
            curMovementInput = context.ReadValue<Vector2>();
            isMove = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            isMove = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJamp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && (IsGrounded() || jumpCount < maxJumpCount))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.5f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.5f, Color.red, 1f);
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                jumpCount = 0;
                return true;
            }
        }

        return false;
    }
}
