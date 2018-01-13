using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
  // The position the camera jumps to upon reset
  private Vector3 resetPosition = new Vector3(-43, 40, 11);

  // The orientation the camera points to upon reset
  private Vector3 resetOrientation = new Vector3(60, 0, 0);

  public float minFlyHeight = 5f;

  public float initialSpeed = 10f;
  public float increaseSpeed = 1.25f;

  public bool allowMovement = true;
  public bool allowRotation = true;

  public KeyCode forwardButton = KeyCode.W;
  public KeyCode backwardButton = KeyCode.S;
  public KeyCode rightButton = KeyCode.D;
  public KeyCode leftButton = KeyCode.A;
  public KeyCode resetButton = KeyCode.R;

  public float cursorSensitivity = 0.025f;
  public KeyCode freeLookButton = KeyCode.LeftShift;

  private float currentSpeed = 0f;
  private bool moving = false;

  private void OnEnable()
  {
    allowRotation = false;
    allowMovement = false;
    Cursor.visible = true;
  }

  private void Update()
  {
    if (allowMovement)
    {
      bool lastMoving = moving;
      Vector3 deltaPosition = Vector3.zero;

      if (moving)
      {
        currentSpeed += increaseSpeed * Time.deltaTime;
      }

      moving = false;

      CheckMove(forwardButton, ref deltaPosition, transform.forward);
      CheckMove(backwardButton, ref deltaPosition, -transform.forward);
      CheckMove(rightButton, ref deltaPosition, transform.right);
      CheckMove(leftButton, ref deltaPosition, -transform.right);

      if (moving)
      {
        if (moving != lastMoving)
        {
          currentSpeed = initialSpeed;
        }

        transform.position += deltaPosition * currentSpeed * Time.deltaTime;
      }
      else currentSpeed = 0f;            
    }

    if (allowRotation)
    {
      Vector3 eulerAngles = transform.eulerAngles;
      eulerAngles.x += -Input.GetAxis("Mouse Y") * 359f * cursorSensitivity;
      eulerAngles.y += Input.GetAxis("Mouse X") * 359f * cursorSensitivity;
      transform.eulerAngles = eulerAngles;
    }

    if (Input.GetKey(freeLookButton))
    {
      allowRotation = true;
      allowMovement = true;
      Cursor.visible = false;
    }
    else
    {
      allowRotation = false;
      allowMovement = false;
      Cursor.visible = true;
    }

    if (Input.GetKeyDown(resetButton))
    {
      transform.position = resetPosition;
      transform.rotation = Quaternion.Euler(resetOrientation);
    }

    if (transform.position.y <= minFlyHeight)
    {
      transform.position = new Vector3(transform.position.x, minFlyHeight, transform.position.z);
    }
  }

  private void CheckMove(KeyCode keyCode, ref Vector3 deltaPosition, Vector3 directionVector)
  {
    if (Input.GetKey(keyCode))
    {
      moving = true;
      deltaPosition += directionVector;
    }
  }
}
