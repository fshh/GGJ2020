﻿using UnityEngine;
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
    public GrabCog grabCog;
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
        
        playerNumberText.text = "P " + (int)playerNumber;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) {
            return;
        }

        if (canMove) {
            Vector2 directionalInput = new Vector2(player.GetAxisRaw(horizontalInput), player.GetAxisRaw(verticalInput));
            movement.SetDirectionalInput(directionalInput);

            if (player.GetButtonDown(jumpInput) && canJump) {
                movement.OnJumpInputDown();
            }

            if (player.GetButtonUp(jumpInput) && canJump) {
                movement.OnJumpInputUp();
            }

            if (player.GetButtonDown(interactInput)) {
                // TODO: interact with cogs
            }

            /*if (player.GetKeyDown(KeyCode.E))
            {
                if (grabCog.cogNearMe != null)
                {
                    grabCog.PickUp();
                }
                else if(grabCog.myCog != null)
                {
                    grabCog.ThrowCog();
                }
            }*/
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
