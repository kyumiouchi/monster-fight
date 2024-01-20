using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _leftBar;

    private void Start()
    {
        Vector3 firstPosition = new Vector3(0, Camera.main.pixelHeight, 0);
        Vector3 cameraPosition = Camera.main.ScreenToViewportPoint (firstPosition);
        Debug.Log("position " + cameraPosition + " first " + firstPosition);
        _leftBar.gameObject.transform.position = cameraPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
