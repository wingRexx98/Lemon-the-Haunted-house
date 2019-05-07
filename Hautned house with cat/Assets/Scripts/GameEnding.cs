using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 2f;//how ,long it would take for the credit or end screen to apear
    public GameObject player;// the player
    bool playerExist;//is the player at the exit
    bool isCaught;// is the player caught

    public CanvasGroup canvasGroup;//the win screen
    public AudioSource exitAudio;//the win sound

    public CanvasGroup caughtGroup;//the lose screen
    public AudioSource caughtAudio;//the lose sound

    public float fadeTimer;
    bool hasAudioPlayed;

    public float displayImageDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerExist = true;
        }
    }

    public void Caught()
    {
        isCaught = true;
    }

    private void Update()
    {
        if (playerExist)
        {
            EndGame(canvasGroup, false, exitAudio);
        }
        else if (isCaught)
            EndGame(caughtGroup, true, caughtAudio);
    }

    void EndGame(CanvasGroup imageGroup, bool restart, AudioSource audio)
    {
        if (!hasAudioPlayed)
        {
            audio.Play();
            hasAudioPlayed = true;
        }

        fadeTimer += Time.deltaTime;
        imageGroup.alpha = fadeTimer / fadeDuration;//calculate the time for the canvas group to fade in and out

        if(fadeTimer > fadeDuration + displayImageDuration)//to turn off the fade/ canvas
        {
            if(restart == false)
            {
                Application.Quit();
            }
            else if(restart == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//LOAD THE current level
            }
        }
    }
}
