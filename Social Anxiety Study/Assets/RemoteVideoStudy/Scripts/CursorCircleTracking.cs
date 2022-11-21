//using EmteqLabs;
//using System.Collections;
//using UnityEngine;

//public class CursorCircleTracking : MonoBehaviour
//{

//    Color CursorNotOverCubeColour = new Color(232, 253, 0);
//    Color CursorOverCubeColour = new Color(0, 0, 255);
//    bool isCursorOverCube = false;

//    public Sprite CursorNotOverCubeSprite;
//    public Sprite CursorOverCubeSprite;

//    public GameObject fastMovementManager;
//    FastMovementManager fastMovementManagerScript;

//    void Start()
//    {
//        fastMovementManagerScript = fastMovementManager.GetComponent<FastMovementManager>();
//    }

//    void Update()
//    {

//    }

//    void FixedUpdate()
//    {
//        // Bit shift the index of the layer (8) to get a bit mask
//        int layerMask = 1 << 8;

//        // This would cast rays only against colliders in layer 8.
//        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
//        layerMask = ~layerMask;

//        RaycastHit hit;
//        // Does the ray intersect any objects excluding the player layer
//        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
//        {
//            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
//            if (!isCursorOverCube)
//            {
                
//                //If we hit an instructions circle, go to the next stage
//                if(hit.transform.gameObject.CompareTag("InstructionsCircle"))
//                {
//                    fastMovementManagerScript.FastMovementSceneNextStage();
//                } else if (hit.transform.gameObject.CompareTag("DataCircle"))
//                {
//                    fastMovementManagerScript.NextCircleCycle();
//                } else if (hit.transform.gameObject.CompareTag("RecentreZone"))
//                {
//                    if(fastMovementManagerScript.IsTutorialFinished())
//                    {
//                        //fastMovementManagerScript.NextCircle();
//                    }
//                    fastMovementManagerScript.PointerOverRecenterZone();
//                    CursorOverCube();
//                } else if (hit.transform.gameObject.CompareTag("MiddleCircle"))
//                {
//                    fastMovementManagerScript.FastMovementSceneNextStage();
//                }
//            }
//        }
//        else
//        {
//            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
//            if (isCursorOverCube)
//            {
//                CursorNotOverCube();
//                fastMovementManagerScript.PointerOverRecenterZone();
//            }
//        }
//    }

//    void CursorOverCube()
//    {
//        //GetComponent<SpriteRenderer>().color = CursorOverCubeColour;
//        GetComponent<SpriteRenderer>().sprite = CursorOverCubeSprite;
//        isCursorOverCube = true;
//        if (fastMovementManagerScript.IsTutorialFinished())
//        {
//            //EmteqVRDevice.SetEventMarker("Start baseline measurement. Type: " + fastMovementManagerScript.GetCircleType() + " Iteration: " + fastMovementManagerScript.GetCurrentIterationValue().ToString());
//            EmteqManager.SetDataPoint("Start baseline measurement");
//        }
//        fastMovementManagerScript.DisableRecentreInstructions();
//    }

//    void CursorNotOverCube()
//    {
//        //GetComponent<SpriteRenderer>().color = CursorNotOverCubeColour;
//        GetComponent<SpriteRenderer>().sprite = CursorNotOverCubeSprite;
//        isCursorOverCube = false;
//        //Debug.Log("test5");
//        StartCoroutine(WaitForCursorNotToBeOverCubeForTime());
//       // fastMovementManagerScript.EnableRecentreInstructions();
//    }

//    public bool IsCursorOverCube
//    {
//        get { return isCursorOverCube; }
//    }

//    IEnumerator WaitForCursorNotToBeOverCubeForTime()
//    {
//        yield return new WaitForSeconds(0.2f);
//        if(!IsCursorOverCube)
//        {
//            fastMovementManagerScript.EnableRecentreInstructions();
//        }
//    }
//}
