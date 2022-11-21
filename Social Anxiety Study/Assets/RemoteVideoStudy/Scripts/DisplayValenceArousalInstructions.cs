using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayValenceArousalInstructions : MonoBehaviour
{

    public GameObject[] NeutralNegativePositive;
    int counter = 1;
    int positiveOrNegative = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        foreach (GameObject instructionAnimation in NeutralNegativePositive)
        {
            instructionAnimation.SetActive(false);
        }
    }

    public void UpdateInstructionsAnimationCycle()
    {
        int remainder = counter % 2;
        if (remainder==0)
        {
            //Show neutral
            NeutralNegativePositive[0].SetActive(true);
            NeutralNegativePositive[1].SetActive(false);
            NeutralNegativePositive[2].SetActive(false);

        } else
        {
            if(positiveOrNegative==0)
            {
                //Negative
                NeutralNegativePositive[0].SetActive(false);
                NeutralNegativePositive[1].SetActive(true);
                positiveOrNegative = 1;
            } else
            {
                //Positive
                NeutralNegativePositive[0].SetActive(false);
                NeutralNegativePositive[2].SetActive(true);
                positiveOrNegative = 0;
            }
        }
        counter++;
    }
}
