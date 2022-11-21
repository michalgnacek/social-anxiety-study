using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoInstructions15New : MonoBehaviour
{

    public GameObject videoPlayerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        DisableScreenCanvas();
    }


    /// <summary>
    /// Screen canvas intereferes with button canvas
    /// </summary>
    void DisableScreenCanvas()
    {
        videoPlayerGameObject.SetActive(false);
    }
}
