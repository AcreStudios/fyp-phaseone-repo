using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    //Start Screen//*****************
    public Canvas StartScreen;
    public Button PressToStart;

    //Main Menu//*****************
    public Canvas MainMenu;
    public Button RedNewGame;
    public Button RedSettings;
    public Button RedCredits;
    public Button RedQuit;
    public RawImage GORedNewGame;
    public RawImage GORedSettings;
    public RawImage GORedCredits;
    public RawImage GORedQuit;
    public RawImage NewGame;
    public RawImage Settings;
    public RawImage Credits;
    public RawImage Quit;

    //Settings//*****************
    public Canvas SettingsMenu;
    public Button RedGame;
    public Button RedAudio;
    public Button RedVideo;
    public Button RedBack;
    public RawImage GORedGame;
    public RawImage GORedAudio;
    public RawImage GORedVideo;
    public RawImage GORedBack;
    public RawImage Game;
    public RawImage Audio;
    public RawImage Video;
    public RawImage Back;

    //Quit//*****************
    public Canvas QuitMenu;
    public Button RedYes;
    public Button RedNo;
    public RawImage GORedYes;
    public RawImage GORedNo;
    public RawImage Yes;
    public RawImage No;

    //Credits//*****************
    public Canvas CreditsMenu;
    public Button RedCreditsBack;
    public RawImage GORedCreditsBack;

    //Loading//*****************
    public Canvas LoadingScreen;

    // Use this for initialization *****************
    void Start () {
        //Start Screen//
        StartScreen = StartScreen.GetComponent<Canvas>();

        PressToStart = PressToStart.GetComponent<Button>();

        StartScreen.enabled = true;

        PressToStart.enabled = true;

        //Main Menu//
        MainMenu = MainMenu.GetComponent<Canvas>();

        RedNewGame = RedNewGame.GetComponent<Button>();
        RedSettings = RedSettings.GetComponent<Button>();
        RedCredits = RedCredits.GetComponent<Button>();
        RedQuit = RedQuit.GetComponent<Button>();

        GORedNewGame = GORedNewGame.GetComponent<RawImage>();
        GORedSettings = GORedSettings.GetComponent<RawImage>();
        GORedCredits = GORedCredits.GetComponent<RawImage>();
        GORedQuit = GORedQuit.GetComponent<RawImage>();

        NewGame = NewGame.GetComponent<RawImage>();
        Settings = Settings.GetComponent<RawImage>();
        Credits = Credits.GetComponent<RawImage>();
        Quit = Quit.GetComponent<RawImage>();

        MainMenu.enabled = false;

        RedNewGame.enabled = true;
        RedSettings.enabled = false;
        RedCredits.enabled = false;
        RedQuit.enabled = false;

        GORedNewGame.enabled = true;
        GORedSettings.enabled = false;
        GORedCredits.enabled = false;
        GORedQuit.enabled = false;

        NewGame.enabled = false;
        Settings.enabled = true;
        Credits.enabled = true;
        Quit.enabled = true;

        //Settings//
        SettingsMenu = SettingsMenu.GetComponent<Canvas>();

        RedGame = RedGame.GetComponent<Button>();
        RedAudio = RedAudio.GetComponent<Button>();
        RedVideo = RedVideo.GetComponent<Button>();
        RedBack = RedBack.GetComponent<Button>();

        GORedGame = GORedGame.GetComponent<RawImage>();
        GORedAudio = GORedAudio.GetComponent<RawImage>();
        GORedVideo = GORedVideo.GetComponent<RawImage>();
        GORedBack = GORedBack.GetComponent<RawImage>();

        Game = Game.GetComponent<RawImage>();
        Audio = Audio.GetComponent<RawImage>();
        Video = Video.GetComponent<RawImage>();
        Back = Back.GetComponent<RawImage>();

        SettingsMenu.enabled = false;

        RedGame.enabled = true;
        RedAudio.enabled = false;
        RedVideo.enabled = false;
        RedBack.enabled = false;

        GORedGame.enabled = true;
        GORedAudio.enabled = false;
        GORedVideo.enabled = false;
        GORedBack.enabled = false;

        Game.enabled = false;
        Audio.enabled = true;
        Video.enabled = true;
        Back.enabled = true;

        //Quit Menu//*****************
        QuitMenu = QuitMenu.GetComponent<Canvas>();

        RedYes = RedYes.GetComponent<Button>();
        RedNo = RedNo.GetComponent<Button>();

        GORedYes = GORedYes.GetComponent<RawImage>();
        GORedNo = GORedNo.GetComponent<RawImage>();

        Yes = Yes.GetComponent<RawImage>();
        No = No.GetComponent<RawImage>();

        QuitMenu.enabled = false;

        RedYes.enabled = false;
        RedNo.enabled = true;

        GORedYes.enabled = false;
        GORedNo.enabled = true;

        Yes.enabled = true;
        No.enabled = false;

        //Credits Menu//*****************
        CreditsMenu = CreditsMenu.GetComponent<Canvas>();

        RedCreditsBack = RedCreditsBack.GetComponent<Button>();

        GORedCreditsBack = GORedCreditsBack.GetComponent<RawImage>();

        CreditsMenu.enabled = false;

        RedCreditsBack.enabled = true;

        GORedCreditsBack.enabled = true;

        //Loading Screen//*****************
        LoadingScreen = LoadingScreen.GetComponent<Canvas>();

        LoadingScreen.enabled = false;
    }

    //Main Menu Button Handling System *****************
    public void MONewGame() {
        NewGame.enabled = false;
        Settings.enabled = true;
        Credits.enabled = true;
        Quit.enabled = true;

        RedNewGame.enabled = true;
        RedSettings.enabled = false;
        RedCredits.enabled = false;
        RedQuit.enabled = false;

        GORedNewGame.enabled = true;
        GORedSettings.enabled = false;
        GORedCredits.enabled = false;
        GORedQuit.enabled = false;

        Debug.Log("new game");
    }
    public void MOSettings() {
        NewGame.enabled = true;
        Settings.enabled = false;
        Credits.enabled = true;
        Quit.enabled = true;

        RedNewGame.enabled = false;
        RedSettings.enabled = true;
        RedCredits.enabled = false;
        RedQuit.enabled = false;

        GORedNewGame.enabled = false;
        GORedSettings.enabled = true;
        GORedCredits.enabled = false;
        GORedQuit.enabled = false;

        Debug.Log("settings");
    }
    public void MOCredits() {
        NewGame.enabled = true;
        Settings.enabled = true;
        Credits.enabled = false;
        Quit.enabled = true;

        RedNewGame.enabled = false;
        RedSettings.enabled = false;
        RedCredits.enabled = true;
        RedQuit.enabled = false;

        GORedNewGame.enabled = false;
        GORedSettings.enabled = false;
        GORedCredits.enabled = true;
        GORedQuit.enabled = false;

        Debug.Log("credits");
    }
    public void MOQuit() {
        NewGame.enabled = true;
        Settings.enabled = true;
        Credits.enabled = true;
        Quit.enabled = false;

        RedNewGame.enabled = false;
        RedSettings.enabled = false;
        RedCredits.enabled = false;
        RedQuit.enabled = true;

        GORedNewGame.enabled = false;
        GORedSettings.enabled = false;
        GORedCredits.enabled = false;
        GORedQuit.enabled = true;

        Debug.Log("quit");
    }

    //Settings Button Handling System *****************
    public void MOGame() {
        RedGame.enabled = true;
        RedAudio.enabled = false;
        RedVideo.enabled = false;
        RedBack.enabled = false;

        GORedGame.enabled = true;
        GORedAudio.enabled = false;
        GORedVideo.enabled = false;
        GORedBack.enabled = false;

        Game.enabled = false;
        Audio.enabled = true;
        Video.enabled = true;
        Back.enabled = true;
    }
    public void MOAudio() {
        RedGame.enabled = false;
        RedAudio.enabled = true;
        RedVideo.enabled = false;
        RedBack.enabled = false;

        GORedGame.enabled = false;
        GORedAudio.enabled = true;
        GORedVideo.enabled = false;
        GORedBack.enabled = false;

        Game.enabled = true;
        Audio.enabled = false;
        Video.enabled = true;
        Back.enabled = true;
    }
    public void MOVideo() {
        RedGame.enabled = false;
        RedAudio.enabled = false;
        RedVideo.enabled = true;
        RedBack.enabled = false;

        GORedGame.enabled = false;
        GORedAudio.enabled = false;
        GORedVideo.enabled = true;
        GORedBack.enabled = false;

        Game.enabled = true;
        Audio.enabled = true;
        Video.enabled = false;
        Back.enabled = true;
    }
    public void MOBack() {
        RedGame.enabled = false;
        RedAudio.enabled = false;
        RedVideo.enabled = false;
        RedBack.enabled = true;

        GORedGame.enabled = false;
        GORedAudio.enabled = false;
        GORedVideo.enabled = false;
        GORedBack.enabled = true;

        Game.enabled = true;
        Audio.enabled = true;
        Video.enabled = true;
        Back.enabled = false;
    }

    //Quit Button Handling System *****************
    public void MOYes() {
        RedYes.enabled = true;
        RedNo.enabled = false;

        GORedYes.enabled = true;
        GORedNo.enabled = false;

        Yes.enabled = false;
        No.enabled = true;
    }
    public void MONo() {
        RedYes.enabled = false;
        RedNo.enabled = true;

        GORedYes.enabled = false;
        GORedNo.enabled = true;

        Yes.enabled = true;
        No.enabled = false;
    }

    //Selecting Individual Buttons Handling System *****************
    public void SelectSettings() {
        MainMenu.enabled = false;
        SettingsMenu.enabled = true;
    }
    public void SelectBack() {
        SettingsMenu.enabled = false;
        CreditsMenu.enabled = false;
        MainMenu.enabled = true;
    }
    public void SelectNewGame() {
        LoadingScreen.enabled = true;
        MainMenu.enabled = false;

        StartCoroutine(loadingtime());
    }
    public void SelectCredits() {
        CreditsMenu.enabled = true;
        MainMenu.enabled = false;
    }
    public void SelectQuit() {
        QuitMenu.enabled = true;
        MainMenu.enabled = false;
    }
    public void SelectStart() {

        MainMenu.enabled = true;
        StartScreen.enabled = false;

    }
    public void SelectNo() {
        QuitMenu.enabled = false;
        MainMenu.enabled = true;
    }
    public void SelectYes() {
        Application.Quit();
    }

    IEnumerator loadingtime() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GI and Reflective Test v2");
    }

}
