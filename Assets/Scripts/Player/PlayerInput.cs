using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

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
    public bool canToss;
    private GrabCog grabCog;
    public PlayerInput myTeammate;

    private Player player;
    private string horizontalInput = "Move Horizontal";
    private string verticalInput = "Move Vertical";
    private string jumpInput = "Jump";
    private string interactInput = "Interact";

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        player = ReInput.players.GetPlayer((int)playerNumber - 1);
        grabCog = GetComponent<GrabCog>();
        playerNumberText.text = "P " + (int)playerNumber;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (canMove)
        {
            Vector2 directionalInput = new Vector2(player.GetAxisRaw(horizontalInput), player.GetAxisRaw(verticalInput));
            movement.SetDirectionalInput(directionalInput);

            if (player.GetButtonDown(interactInput))
            {
                Debug.Log("press");
                if (grabCog.myCog != null)
                {
                    grabCog.ThrowCog();
                }
                else if (grabCog.cogNearMe != null)
                {
                    grabCog.PickUp();
                }
            }

            if (player.GetButtonDown(jumpInput) && canJump) {
                movement.OnJumpInputDown();
            }

            if (player.GetButtonUp(jumpInput) && canJump) {
                movement.OnJumpInputUp();
            }

           
        }
    }

    public void DisableJump()
    {
        canJump = false;
    }

    public void DisableMovement()
    {
        canMove = false;
        movement.SetDirectionalInput(Vector2.zero);
    }

    public void DisableMovementForTime(float duration)
    {
        StartCoroutine(DisableMovementRoutine(duration));
    }

    private IEnumerator DisableMovementRoutine(float duration)
    {
        DisableMovement();
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canMove = true;
    }

    public bool CanMove()
    {
        return canMove;
    }


}