using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    private int gridLenght = 2;
    private Vector3 targetPosition, startPosition;
    private bool moving;
    private Vector3 rotateAxis = Vector3.right;

    public int onTop, times;
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
        //playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.Euler(new Vector3(0, 0, 90)), moveSpeed * Time.deltaTime);
        if (moving)
        {
            if (Vector3.Distance(startPosition, playerTransform.position) > 1f*gridLenght*times)
            {
                playerTransform.position = targetPosition;
                moving = false;
                DetermineOnTop();
                return;
            }
            playerTransform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime / times;
            return;
        }

        GetInput();
    }

    #region Input

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DetermineNext(Vector3.back);
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            DetermineNext(Vector3.right);
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DetermineNext(Vector3.forward);
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DetermineNext(Vector3.left);
            MoveRight();
        }
    }

    #endregion

    #region Movement and Animation

    private void MoveUp()
    {
        targetPosition = playerTransform.position + Vector3.forward * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.right;
        Quaternion rotR = Quaternion.AngleAxis(90 * times, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveLeft()
    {
        targetPosition = playerTransform.position + Vector3.left * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.forward;
        Quaternion rotR = Quaternion.AngleAxis(90 * times, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveDown()
    {
        targetPosition = playerTransform.position + Vector3.back * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.left;
        Quaternion rotR = Quaternion.AngleAxis(90 * times, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveRight()
    {
        targetPosition = playerTransform.position + Vector3.right * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.back;
        Quaternion rotR = Quaternion.AngleAxis(90 * times, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    #endregion

    #region Dice Face

    private void DetermineOnTop()
    {
        for (int i = 0; i < Numbers.Length; i++)
        {
            if (Numbers[i].forward == Vector3.up)
            {
                onTop = i + 1;
            }
        }
    }

    private void DetermineNext(Vector3 direction)
    {
        for (int i = 0; i < Numbers.Length; i++)
        {
            if (Numbers[i].forward == direction)
            {
                times = i + 1;
            }
        }
    }

    #endregion
}
