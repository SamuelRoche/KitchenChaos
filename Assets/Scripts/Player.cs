using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 MoveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = 0.7f;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, MoveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move towards moveDir

            //Attempt only x movement
            Vector3 MoveDirX = new Vector3(MoveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, MoveDirX, moveDistance);

            if (canMove)
            {
                MoveDir = MoveDirX;
            }
            else
            {
                // Cannot move only on the x
                // Attempt only z movement
                Vector3 MoveDirZ = new Vector3(0f, 0f, MoveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, MoveDirZ, moveDistance);

                // Can nly move on the z axis
                if (canMove)
                {
                    MoveDir = MoveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }

        }

        if (canMove)
        {
            transform.position += MoveDir * moveDistance;
        }

        isWalking = MoveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}