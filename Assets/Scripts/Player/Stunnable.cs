using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Stunnable : MonoBehaviour
{
    public float stunDuration = 1f;

    private PlayerInput input;
    private AudioSource audioSource;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Stun()
    {
        input.DisableMovementForTime(stunDuration);
        // TODO: play stun sound and animation
    }
}
