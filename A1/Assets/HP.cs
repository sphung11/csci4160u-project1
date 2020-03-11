using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(Animator))]
public class HP : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject mainCamera;
    public GameObject transition;
    private AudioSource gameOver;
    private AudioSource sceneMusic;
    private Animator animator;

    public int hitPoints = 6;

    void Start()
    {
        animator = GetComponent<Animator>();
        sceneMusic = mainCamera.GetComponent<AudioSource>();

        if (healthBar != null)
        {
            gameOver = healthBar.GetComponent<AudioSource>();
        }
    }

    public void TakeDamage()
    {
        hitPoints -= 2;

        if (hitPoints <= 0)
        {
            Debug.Log("Dead.");
            sceneMusic.Stop();
            gameOver.Play();
            transition.GetComponent<SwitchScene>().GameOver();
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        Animator animator = healthBar.GetComponent<Animator>();
        animator.SetInteger("Health", hitPoints);
    }
}
