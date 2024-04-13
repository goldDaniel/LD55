using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void UI_OnStart()
	{
		LoadScene.LoadNextScene("Gameplay");
	}

	public void UI_OnHowToPlay()
	{
		Debug.Log("HOW TO PLAY");
	}

	public void UI_OnExit()
	{
		Application.Quit();
	}
}
