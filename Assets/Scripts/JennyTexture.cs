using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JennyTexture : MonoBehaviour
{
    public GameObject jenny;

    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = jenny.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCameraAlpha = 0.5f;
        videoPlayer.url = "Assets/MPL/StreamingAssets/Jenny_Seated_Listening.mp4";
        //videoPlayer.frame=100;
        videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10
        //videoPlayer.loopPointReached += EndReached; 

        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
