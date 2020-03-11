using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SwitchScene : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LevelOne()
    {
        StartCoroutine(SwitchToScene("LevelOne", 0f, 1f));
    }

    public void MainMenu()
    {
        StartCoroutine(SwitchToScene("MainMenu", 0f, 1f));
    }

    public void GameOver()
    {
        StartCoroutine(SwitchToScene("GameOver", 0f, 3f));
    }

    public void Winner()
    {
        StartCoroutine(SwitchToScene("Winner", 2f, 0f));
    }

    private IEnumerator SwitchToScene(string sceneName, float before, float after)
    {
        yield return new WaitForSeconds(before);
        animator.SetBool("animateOut", true);
        yield return new WaitForSeconds(after);

        SceneManager.LoadScene(sceneName);
    }


}
