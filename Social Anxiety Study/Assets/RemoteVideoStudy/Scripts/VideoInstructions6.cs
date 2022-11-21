using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoInstructions6 : MonoBehaviour
{

    public GameObject highLight;
    public GameObject affectScale;
    public GameObject g2_controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        highLight.SetActive(false);
    }
    void OnEnable()
    {
        affectScale.SetActive(true);
        highLight.SetActive(true);
        g2_controller.SetActive(false);
    }
}
