using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 10f;
    public float jumpPower = 10f;
    public Camera mainCamera;

    private CharacterController moveController;
    private float yForce = 0;

    void Start()
    {
        moveController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameController.Instance.GameState != GameStates.Game) return;

        // Movement
        transform.Rotate(0, InputController.RotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 moveDirection = new Vector3(InputController.Horizontal, 0, InputController.Vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveController.Move(moveDirection * speed * Time.deltaTime);

        // Camera rotation
        Quaternion targetCameraRotation = mainCamera.transform.localRotation;
        targetCameraRotation *= Quaternion.Euler(InputController.RotationVertical * rotationSpeed * Time.deltaTime, 0f, 0f);
        targetCameraRotation = ClampRotationAroundXAxis(targetCameraRotation);
        mainCamera.transform.localRotation = targetCameraRotation;

        // Jump
        RaycastHit hitInfo;
        bool isGrounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hitInfo, moveController.height / 2);
        bool isCeiling = Physics.SphereCast(transform.position, 0.5f, Vector3.up, out hitInfo, moveController.height / 2);
        if (yForce > -10f) yForce -= 30f * Time.deltaTime;
        if (isGrounded && InputController.Jump)
        {
            yForce = jumpPower;
        }
        if (isCeiling && yForce > 0) yForce = 0;
        moveController.Move(transform.up * yForce * Time.deltaTime);
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -75, 75);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
