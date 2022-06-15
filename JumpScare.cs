using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpScare : MonoBehaviour
{
    private readonly int jumpScareHash = Animator.StringToHash("jumpScare");
    public Camera jumpscareCam;
    public Animator animator;
    public GameObject fadeOut;
    public AudioClip clip;
    private AudioSource source;
    public float delay;
    private void Start()
    {
        jumpscareCam.gameObject.SetActive(false);
        fadeOut.SetActive(false);
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnJumpScare(other.gameObject);
        }
    }

    void OnJumpScare(GameObject player)
    {
        player.SetActive(false);
        jumpscareCam.gameObject.SetActive(true);
        animator.SetTrigger(jumpScareHash);
        source.Play();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        fadeOut.SetActive(true);

    }
}
