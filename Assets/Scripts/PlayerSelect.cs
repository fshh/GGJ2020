using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;

public class PlayerSelect : MonoBehaviour
{
    public PlayerInput.PlayerNumber playerNumber;
    public float transitionTime = 0.5f;
    public float confirmTime = 0.5f;
    public float confirmShakeStrength = 100f;
    public float unConfirmShakeStrengthRatio = 0.25f;
    public int confirmShakeVibrato = 50;
    public RectTransform selectionPanel;
    public RectTransform playerTextElement;
    public RectTransform[] textPositions;
    public Image leftArrow;
    public Image rightArrow;

    private Player player;
    private int currentPos = 1;
    private bool confirmed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer((int)playerNumber - 1);
        UpdateTextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        int moveHorizontal = (int)player.GetAxisRaw("Move Horizontal");
        if (!confirmed && player.GetAxisRawPrev("Move Horizontal") == 0f && moveHorizontal != 0)
        {
            currentPos = Mathf.Clamp(currentPos + moveHorizontal, 0, 2);
            UpdateTextPosition();
        }

        if (!confirmed && currentPos != 1 && player.GetButtonDown("Confirm"))
        {
            Confirm();
        }

        if (confirmed && player.GetButtonDown("Cancel"))
        {
            UnConfirm();
        }
    }

    private void UpdateTextPosition()
    {
        UpdateArrows();
        selectionPanel.DOAnchorPos(textPositions[currentPos].anchoredPosition, transitionTime);
    }

    private void Confirm()
    {
        confirmed = true;
        leftArrow.enabled = false;
        rightArrow.enabled = false;
        playerTextElement.DOShakeAnchorPos(confirmTime, confirmShakeStrength, confirmShakeVibrato);
        playerTextElement.DOScale(1.5f, confirmTime);
    }

    private void UnConfirm()
    {
        confirmed = false;
        playerTextElement.DOShakeAnchorPos(confirmTime, confirmShakeStrength * unConfirmShakeStrengthRatio, confirmShakeVibrato);
        playerTextElement.DOScale(1.0f, confirmTime);
        UpdateArrows();
    }

    private void UpdateArrows()
    {
        switch (currentPos)
        {
            case 0:
                leftArrow.enabled = false;
                rightArrow.enabled = true;
                break;
            case 2:
                leftArrow.enabled = true;
                rightArrow.enabled = false;
                break;
            default:
                leftArrow.enabled = true;
                rightArrow.enabled = true;
                break;
        }
    }
}
