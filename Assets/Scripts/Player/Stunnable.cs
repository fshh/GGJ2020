using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Stunnable : MonoBehaviour
{
    public float stunDuration = 1f;

    private bool stunnable = true;
    private PlayerInput input;
    private AudioSource audioSource;
    private Animator anim;
    public ParticleSystem stunParticles;

    // Start is called before the first frame update
    void Start()
    {
        stunParticles.Stop();
        input = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    private void LateUpdate()
    {
        anim.SetBool("Stunned", !stunnable);
    }

    public bool Stun()
    {
        if (!stunnable) { return false; }
        input.DisableMovementForTime(stunDuration);
        StartCoroutine(DisableStunningForTime(stunDuration));
        // TODO: play stun sound and animation
        return true;
    }

    private IEnumerator DisableStunningForTime(float duration)
    {
        stunParticles.Play();
        GetComponent<PlayerMovement>().audioSource.PlayOneShot(GetComponent<PlayerMovement>().stunSound);
        GetComponent<GrabCog>().Drop();
        stunnable = false;
        yield return new WaitForSecondsRealtime(stunDuration);
        stunnable = true;
    }
}
