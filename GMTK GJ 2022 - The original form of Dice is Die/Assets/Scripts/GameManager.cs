using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsOnGoal())
            StartCoroutine(NextLevel());

        if (playerController.fail)
            StartCoroutine(Restart());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);

        ui.LoadNextLevel();
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);

        ui.Restart();
    }
}
