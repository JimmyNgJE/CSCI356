using UnityEngine;

public class GunnerMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float jumpSpeed = 5f;

    [SerializeField]
    private float jumpButtonGracePeriod = 0.2f;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;

    public GameObject settingsPopup; // Reference to the settings popup GameObject

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        characterController.stepOffset = 0.3f; // Adjust as needed
    }

    void Update()
    {
        // Check if settings menu is active
        if (settingsPopup.activeSelf)
        {
            return; // Skip processing if settings menu is open
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction and magnitude
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Update animator parameter
        animator.SetFloat("InputMagnitude", inputMagnitude, 0.01f, Time.deltaTime);

        // Convert movement direction to world space relative to camera
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // Apply gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Jump logic
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
            }

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }
        }

        if (isJumping && characterController.isGrounded)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        // Apply movement
        Vector3 velocity = movementDirection * inputMagnitude * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        // Rotate character towards movement direction
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true); // Update moving state
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false); // Stop moving animation when there's no input
        }
    }

    private void OnAnimatorMove()
    {
        if (characterController.isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;
            characterController.Move(velocity);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
