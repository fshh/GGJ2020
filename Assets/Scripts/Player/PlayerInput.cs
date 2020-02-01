using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public enum PlayerNumber { ONE = 1, TWO = 2, THREE = 3, FOUR = 4 }
    public PlayerNumber playerNumber;
    public TextMeshProUGUI playerNumberText;

    private PlayerMovement movement;
    private bool canJump = true;
    private bool canMove = true;

    private string horizontalInput;
    private string verticalInput;
    private string jumpInput;
    private string dPadHorizontal;
    private string dPadVertical;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        
        int pnum = (int)playerNumber;
        horizontalInput = "Horizontal_P" + pnum;
        verticalInput = "Vertical_P" + pnum;
        jumpInput = "Jump_P" + pnum;
        dPadHorizontal = "DPadHorizontal_P" + pnum;
        dPadVertical = "DPadVertical_P" + pnum;

        playerNumberText.text = "P " + pnum;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) {
            return;
        }

        if (canMove)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontalInput), Input.GetAxisRaw(verticalInput));
            movement.SetDirectionalInput(directionalInput);

            if (Input.GetButtonDown(jumpInput) && canJump)
            {
                movement.OnJumpInputDown();
            }

            if (Input.GetButtonUp(jumpInput) && canJump)
            {
                movement.OnJumpInputUp();
            }
        }
    }

    public void DisableJump() {
        canJump = false;
    }

    public void DisableMovement()
    {
        canMove = false;
        movement.SetDirectionalInput(Vector2.zero);
    }

    public bool CanMove() {
        return canMove;
    }
}
