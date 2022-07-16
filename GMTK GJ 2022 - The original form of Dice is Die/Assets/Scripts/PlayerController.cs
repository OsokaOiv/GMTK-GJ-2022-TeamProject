using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public int onTop, times;
    public bool fail;

    [SerializeField] private Transform[] Numbers;
    [SerializeField] private float moveSpeed = 10f;
    public LayerMask ground;
    public LayerMask goal;

    private int gridLenght = 2;
    private bool moving;
    private Vector3 targetPosition, startPosition;
    private Vector3 rotateAxis;


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
            if (!IsOnGoal() && IsOnGround())
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
            else
            {
                fail = true;
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (IsOnGround())
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

    public bool IsOnGoal()
    {
        RaycastHit hit;
        Ray downwardRay = new Ray(playerTransform.position, Vector3.down);
        Debug.DrawRay(playerTransform.position, Vector3.down*10, Color.red, 2f);

        if (Physics.Raycast(downwardRay, out hit, 10f, goal))
        {
            GetComponent<Animator>().enabled = true;
            return true;
        }
        return false;
    }

    private void MoveUp()
    {
        targetPosition = playerTransform.position + Vector3.forward * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.right;
        Quaternion rotR = Quaternion.AngleAxis(90, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveLeft()
    {
        targetPosition = playerTransform.position + Vector3.left * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.forward;
        Quaternion rotR = Quaternion.AngleAxis(90, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveDown()
    {
        targetPosition = playerTransform.position + Vector3.back * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.left;
        Quaternion rotR = Quaternion.AngleAxis(90, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private void MoveRight()
    {
        targetPosition = playerTransform.position + Vector3.right * gridLenght * times;
        startPosition = playerTransform.position;
        moving = true;
        rotateAxis = Vector3.back;
        Quaternion rotR = Quaternion.AngleAxis(90, rotateAxis);
        transform.rotation = rotR * transform.rotation;
    }

    private bool IsOnGround()
    {
        RaycastHit hit;
        Ray downwardRay = new Ray(playerTransform.position, Vector3.down);

        if(Physics.Raycast(downwardRay, out hit, 10f, ground))
        {
            return true;
        }
        return false;
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
