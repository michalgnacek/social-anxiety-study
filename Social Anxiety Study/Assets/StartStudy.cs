using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStudy : MonoBehaviour
{
    public GameObject calibrationGameObject;
    public GameObject welcomeInstructions;

    // Start is called before the first frame update
    void Start()
    {
        welcomeInstructions.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ButtonPressed();
        }
    }

    public void ButtonPressed()
    {
        welcomeInstructions.SetActive(false);
        calibrationGameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
