using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    private int gridLenght = 2;
    private Vector3 targetPosition, startPosition;
    private bool moving;

    public int onTop;
    [SerializeField] private Transform[] Numbers;
    [SerializeField] private float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        DetermineOnTop();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(startPosition, playerTransform.position) > 1f*gridLenght)
            {
                playerTransform.position = targetPosition;
                moving = false;
                DetermineOnTop();
                return;
            }
            playerTransform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            Vector3 relativePos = targetPosition - startPosition;
            playerTransform.rotation = Quaternion.LookRotation(relativePos);
            return;
        }

        GetInput();
    }

    #region Input

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    #endregion

    #region Movement and Animation

    private void MoveUp()
    {
        targetPosition = playerTransform.position + Vector3.forward * gridLenght;
        startPosition = playerTransform.position;
        moving = true;
        //playerTransform.Rotate(new Vector3(90, 0, 0), Space.World);
    }

    private void MoveLeft()
    {
        targetPosition = playerTransform.position + Vector3.left * gridLenght;
        startPosition = playerTransform.position;
        moving = true;
        playerTransform.Rotate(new Vector3(0, 0, 90), Space.World);
    }

    private void MoveDown()
    {
        targetPosition = playerTransform.position + Vector3.back * gridLenght;
        startPosition = playerTransform.position;
        moving = true;
        playerTransform.Rotate(new Vector3(-90, 0, 0), Space.World);
    }

    private void MoveRight()
    {
        targetPosition = playerTransform.position + Vector3.right * gridLenght;
        startPosition = playerTransform.position;
        moving = true;
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
