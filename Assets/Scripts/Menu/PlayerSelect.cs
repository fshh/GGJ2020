using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;

public class PlayerSelect : MonoBehaviour
{
    public PlayerNumber playerNumber;
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

    private Tween moveTween;
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

        if (player.GetButtonDown("Cancel"))
        {
            if (confirmed)
            {
                UnConfirm();
            }
            else
            {
                FindObjectOfType<TeamSelectManager>().GoBack();
            }
        }
    }

    private void UpdateTextPosition()
    {
        UpdateArrows();
        moveTween = selectionPanel.DOAnchorPos(textPositions[currentPos].anchoredPosition, transitionTime);
        moveTween.Play();
    }

    private void Confirm()
    {
        if (moveTween.IsPlaying()) { return; }

        TeamNumber team = currentPos < 1 ? TeamNumber.ONE : TeamNumber.TWO;
        if (PlayerToTeamMap.AssignPlayerToTeam(playerNumber, team))
        {
            confirmed = true;
            leftArrow.enabled = false;
            rightArrow.enabled = false;
            playerTextElement.DOShakeAnchorPos(confirmTime, confirmShakeStrength, confirmShakeVibrato);
            playerTextElement.DOScale(1.5f, confirmTime);
        } else
        {
            Sequence failConfirm = DOTween.Sequence();
            failConfirm.Append(playerTextElement.DOScale(1.2f, confirmTime / 2));
            failConfirm.Append(playerTextElement.DOScale(1f, confirmTime / 2));
        }
    }

    private void UnConfirm()
    {
        if (confirmed)
        {
            PlayerToTeamMap.ResetPlayerTeam(playerNumber);
            confirmed = false;
            playerTextElement.DOShakeAnchorPos(confirmTime, confirmShakeStrength * unConfirmShakeStrengthRatio, confirmShakeVibrato);
            playerTextElement.DOScale(1.0f, confirmTime);
            UpdateArrows();
        }
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
