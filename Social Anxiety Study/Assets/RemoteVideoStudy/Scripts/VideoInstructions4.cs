using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoInstructions4 : MonoBehaviour
{
    public GameObject instructionsAnimationToEnable;

    // Start is called before the first frame update
    void Start()
    {
        instructionsAnimationToEnable.SetActive(true);
    }

    private void OnDisable()
    {
        instructionsAnimationToEnable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
