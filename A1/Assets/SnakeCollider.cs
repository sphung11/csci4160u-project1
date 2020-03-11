using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class SnakeCollider : MonoBehaviour
{
    private Animator animator;
    private AudioSource impact;

    void Start()
    {
        animator = GetComponent<Animator>();
        impact = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CollideWithPlayer(other.gameObject));
        }
    }

    private IEnumerator CollideWithPlayer(GameObject player)
    {
        Debug.Log("Snake touched.");

        Animator playerAnimator = player.GetComponent<Animator>();

        playerAnimator.SetBool("Damaged", true);
        impact.Play();
        HP hp = player.GetComponent<HP>();
        hp.TakeDamage();
        yield return 2;
        playerAnimator.SetBool("Damaged", false);
    }
}
