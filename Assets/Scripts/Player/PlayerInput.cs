using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement player;
    private bool canJump = true;
    private bool canMove = true;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) {
            return;
        }

        if (canMove)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            if (Input.GetButtonDown("Jump") && canJump)
            {
                player.OnJumpInputDown();
            }

            if (Input.GetButtonUp("Jump") && canJump)
            {
                player.OnJumpInputUp();
            }
        }
    }

    public void DisableJump() {
        canJump = false;
    }

    public void DisableMovement()
    {
        canMove = false;
        player.SetDirectionalInput(Vector2.zero);
    }

    public bool CanMove() {
        return canMove;
    }
}
