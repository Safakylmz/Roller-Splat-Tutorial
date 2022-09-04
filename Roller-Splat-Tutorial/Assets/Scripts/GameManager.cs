 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] grounds;
    public float groundNumbers;
    private int currentLevel;

    private void Start()
    {
        grounds = GameObject.FindGameObjectsWithTag("Ground");
        currentLevel = SceneManager.GetActiveScene().buildIndex; //bulunduðumuz seviyenin index no al.
    }

    private void Update()
    {
        groundNumbers = grounds.Length;
    }

    public void LevelUpdate()
    {
        SceneManager.LoadScene(currentLevel + 1);
    }
}
