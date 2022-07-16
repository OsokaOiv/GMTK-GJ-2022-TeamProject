using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    private int gridLenght = 2;

    public int onTop;
    public Transform[] Numbers;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        DetermineOnTop();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    #region Input

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
            DetermineOnTop();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            DetermineOnTop();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
            DetermineOnTop();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            DetermineOnTop();
        }
    }

    #endregion

    #region Movement and Animation

    private void MoveUp()
    {
        //playerTransform.position = playerTransform.position + Vector3.forward * gridLenght;
        //playerTransform.Rotate(new Vector3(90, 0, 0), Space.World);
        for (int i = 0; i < 30; i++)
        {
            StartCoroutine(MoveUpFrame());
        }
    }
    IEnumerator MoveUpFrame()
    {
        playerTransform.Rotate(new Vector3(3, 0, 0), Space.World);
        playerTransform.position = playerTransform.position + Vector3.forward * gridLenght / 30;
        yield return new WaitForSeconds(.1f);
    }

    private void MoveLeft()
    {
        playerTransform.position = playerTransform.position + Vector3.left * gridLenght;
        playerTransform.Rotate(new Vector3(0, 0, 90), Space.World);
    }

    private void MoveDown()
    {
        playerTransform.position = playerTransform.position + Vector3.back * gridLenght;
        playerTransform.Rotate(new Vector3(-90, 0, 0), Space.World);
    }

    private void MoveRight()
    {
        playerTransform.position = playerTransform.position + Vector3.right * gridLenght;
        playerTransform.Rotate(new Vector3(0, 0, -90), Space.World);
    }

    #endregion

    #region Dice Face

    private void DetermineOnTop()
    {
        for (int i = 0; i < Numbers.Length; i++)
        {
            if (Numbers[i].position.y == 1 * gridLenght)
            {
                onTop = i + 1;
            }
        }
    }

    #endregion
}
