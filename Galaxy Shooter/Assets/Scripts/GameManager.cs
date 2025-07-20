using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    public UIManager manager;

    private void Start()
    {
        manager = GameObject.Find("Canvas").GetComponent<UIManager>(); ;
    }
    public void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player,Vector3.zero,Quaternion.identity);
                gameOver = false;
                manager.HideTitleScreen();
            }
        }
    }
}
