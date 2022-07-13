using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    #region Singleton 
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion
    public static event Action winEvent;
    public static event Action loseEvent;
    [SerializeField] private CinemachineVirtualCamera defaultCam, fallingCam, collisionCam;
    [SerializeField] private GameObject animatedDoll, ragdoll;
    [SerializeField] private Rigidbody rb;
    private int score;

    void Awake()
    {
        _instance = this;
    }
    private void OnEnable()
    {
        // Obstacle.collisionEvent += UpdateScore; 
        // PlayerMovement.floorLandingAction += EndGame; 

    }
    private void OnDisable()
    {
        // Obstacle.collisionEvent -= UpdateScore; 
        // PlayerMovement.floorLandingAction -= EndGame; 
    }
    public void StartGame()
    {
        // after the player touches the screen for the first time. 
        score = 0;
        animatedDoll.SetActive(false);
        ragdoll.SetActive(true);
        StartCoroutine(PushPlayer());

    }
    public IEnumerator PushPlayer()
    {
        Debug.Log("pushing!");
        rb.AddForce(new Vector3(0, 1, 1) * 125, ForceMode.Impulse);
        yield return new WaitForSeconds(.2f);
        defaultCam.Priority = 0;
        fallingCam.Priority = 1;
    }
    private void UpdateScore(Vector3 pos, Transform building)
    {
        score++;
        Debug.Log(score);
    }
    public void DecreaseScore()
    {
        score--;
        Debug.Log(score);
    }
    private void EndGame(Vector3 pos)
    {
        // when the game finished. 
        CheckWinOrLose();
    }
    private void CheckWinOrLose()
    {
        // switch states. 
        if (score <= 4)
            winEvent?.Invoke();
        else if (score > 0)
            loseEvent?.Invoke();

    }
}