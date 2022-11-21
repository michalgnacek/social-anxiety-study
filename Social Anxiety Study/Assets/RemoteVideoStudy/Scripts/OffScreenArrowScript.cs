using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenArrowScript : MonoBehaviour
{

    public GameObject arrowToCubeIfOutOfVision;

    Renderer circleRenderer;
    bool isCubeInVision = true;

    Vector3 screenPos;
    Vector3 updatedWorldPos;
    Vector2 onScreenPos;
    float max;
    public Camera mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        circleRenderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (arrowToCubeIfOutOfVision != null)
        {
            screenPos = mainCamera.WorldToViewportPoint(this.transform.position); //get viewport positions

            if (!isCubeInVision && circleRenderer.isVisible)
            {
                //  Debug.Log("already on screen, don't bother with the rest!");

                HidePointerToCube();

            }
            else if (isCubeInVision && !circleRenderer.isVisible)
            {
                DisplayPointerToCube();
            }

            if (!isCubeInVision)
            {
                UpdatePointerPosition();
            }
        }
    }

    void DisplayPointerToCube()
    {
        isCubeInVision = false;
        if (!arrowToCubeIfOutOfVision.activeSelf)
        {
            arrowToCubeIfOutOfVision.SetActive(true);
        }
    }

    void HidePointerToCube()
    {
        isCubeInVision = true;
        if (arrowToCubeIfOutOfVision.activeSelf)
        {
            arrowToCubeIfOutOfVision.SetActive(false);
        }
    }

    void UpdatePointerPosition()
    {
        if (screenPos.z < 0F)
        {
            //invert vector when behind camera
            screenPos = Vector3.one - screenPos;
        }

        onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

        updatedWorldPos = new Vector3(Mathf.Clamp(onScreenPos.x, 0.1f, 0.9f), Mathf.Clamp(onScreenPos.y, 0.15f, 0.85f), 5);

        arrowToCubeIfOutOfVision.transform.position = mainCamera.ViewportToWorldPoint(updatedWorldPos);
        arrowToCubeIfOutOfVision.transform.LookAt(Vector3.zero); //reset z rotation

        float angle = Mathf.Atan2(arrowToCubeIfOutOfVision.transform.position.y, arrowToCubeIfOutOfVision.transform.position.x);
        angle -= 90 * Mathf.Deg2Rad;

        float cos = Mathf.Cos(angle);
        float sin = -Mathf.Sin(angle);

        float m = cos / sin;

        if (sin > 0.5)
        {
            arrowToCubeIfOutOfVision.transform.Rotate(0, 0, 180);
        }

        else if (sin < -0.5)
        {
            arrowToCubeIfOutOfVision.transform.Rotate(0, 0, 0);
        }

        else if (cos > 0)
        {
            arrowToCubeIfOutOfVision.transform.Rotate(0, 0, 90);
        }

        else if (cos < 0)
        {
            arrowToCubeIfOutOfVision.transform.Rotate(0, 0, 270);
        }

    }
}
