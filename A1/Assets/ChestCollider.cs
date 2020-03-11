using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GameObject))]
public class ChestCollider : MonoBehaviour
{
    public GameObject transition;
    private Animator animator;
    private AudioSource success;
    private bool triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        success = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Chest tagged.");
            if (!triggered)
            {
                other.GetComponent<AudioSource>().Stop();
                success.Play();
                triggered = true;
                animator.SetBool("Triggered", triggered);
                transition.GetComponent<SwitchScene>().Winner();
            }
        }
    }
}
