//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Pvr_UnitySDKAPI;

//public class Welcome : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown("space") || Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.HOME))
//        {
//            SceneManager.LoadScene("Menu");
//        }
//    }

//    public void StartButtonPress()
//    {
//        Debug.Log("Start button pressed in Welcome scene");
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//    }
//}
