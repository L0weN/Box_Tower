using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private float max_X = 1.55f;
    private float min_X = -1.55f;
    private float moveSpeed = 2f;

    private bool canMove;
    private bool gameOver;

    private bool ignoreCollision;
    private bool ignoreTrigger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Start()
    {
        canMove = true;
        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1;
        }

        GameplayController.instance.currentBox = this;
    }
    private void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > max_X)
            {
                moveSpeed *= -1;
            }
            else if (temp.x < min_X)
            {
                moveSpeed *= -1;
            }
            
            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ignoreCollision)
            return;
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Box"))
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreTrigger)
            return;
        if (collision.CompareTag("GameOver"))
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;
            GameplayController.instance.RestartGame();
        }
    }
}
