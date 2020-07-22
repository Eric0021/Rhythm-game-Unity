using System.Collections;
using System.Collections.Generic;
using Script.playscreen.NotesStrategy;
using Script.Songs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPlayScreen : MonoBehaviour{

    // difficulty of song is defaulted to easy.
    private static Difficulty _difficulty = Difficulty.Easy;
    private static ISong _song = new SecretGardenTest(); // change this 
    
    public void ButtonMoveScene(string songAndLevel) {
        var songAndLevelArr = songAndLevel.Split(':');
        string level = songAndLevelArr[0];
        string songStr = songAndLevelArr[1];
        _song = GetSong(songStr);
        
        SceneManager.LoadScene(level);
    }

    private ISong GetSong(string songStr) {
        ISong song = null;
        switch (songStr) {
            case "SecretGarden":
                song = new SecretGardenTest();
                break;
        }

        return song;
    }

    public void SetDifficulty(string difficulty) {
        Difficulty difficultyEnum = Difficulty.Easy;
        switch (difficulty) {
            case "hard":
                difficultyEnum = Difficulty.Hard;
                break;
            case "medium":
                difficultyEnum = Difficulty.Medium;
                break;
            case "easy":
                difficultyEnum = Difficulty.Easy;
                break;
        }
        _difficulty = difficultyEnum;
    }
    
    public static Difficulty Difficulty {
        get => _difficulty;
        set => _difficulty = value;
    }

    public static ISong Song {
        get => _song;
        set => _song = value;
    }
}
