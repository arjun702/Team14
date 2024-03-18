using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas uiCanvas;
    public Button playButton1;
    public Button playButton2;
    public Button pauseButton;
    public Button exitButton;

    public string videoPath1 = "https://dl.dropbox.com/scl/fi/mc3otapzhzxy66br6f4oh/WhatsApp-Video-2024-03-09-at-16.43.56.mp4?rlkey=w8dnyalwkjo2u394sqxkznzjh";
    public string videoPath2 = "https://dl.dropbox.com/scl/fi/vfof69rqntg02lmgv19a2/0309.mov?rlkey=t63yrco32ho0033ygh8co11oa";

    private bool isMouseOverQuad = false;

    void Start()
    {
        // Hide UI Canvas initially
        uiCanvas.gameObject.SetActive(false);

        // Set up button click events
        playButton1.onClick.AddListener(() => PlayVideo(videoPath1));
        playButton2.onClick.AddListener(() => PlayVideo(videoPath2));
        pauseButton.onClick.AddListener(TogglePause);
        exitButton.onClick.AddListener(StopVideo);
    }

    void Update()
    {
        // Check if the mouse is over the Quad
        if (isMouseOverQuad)
        {
            // Show UI Canvas
            uiCanvas.gameObject.SetActive(true);
        }
        else
        {
            // Hide UI Canvas
            uiCanvas.gameObject.SetActive(false);
        }
    }

    public void OnMouseEnterQuad()
    {
        isMouseOverQuad = true;
    }

    public void OnMouseExitQuad()
    {
        isMouseOverQuad = false;
    }

    public void PlayVideo(string path)
    {
        // Load and play the specified video
        videoPlayer.url = path;
        videoPlayer.Play();
    }

    public void TogglePause()
    {
        if (videoPlayer.isPlaying)
        {
            // Pause the video if it's playing
            videoPlayer.Pause();
        }
        else
        {
            // Resume playing the video if it's paused
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        // Stop video playback and clear the video URL
        videoPlayer.Stop();
        videoPlayer.url = "";
    }
}
