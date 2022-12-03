
using UnityEngine;
using UnityEngine.Video;



public class VideoInstructions7 : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _videoPlayer.Pause();
    }
}
