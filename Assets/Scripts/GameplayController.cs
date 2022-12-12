using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public BoxSpawnerScript boxSpawner;
    [HideInInspector]public BoxScript currentBox;
    public CameraFollow cameraFollow;

    private int moveCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        boxSpawner.SpawnBox();
    }
    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBox != null)
            {
                currentBox.DropBox();
                currentBox = null;
            }
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 0.5f);
    }

    void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 3)
        {
            moveCount = 0;
            cameraFollow.targetPos.y += 2f;
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
