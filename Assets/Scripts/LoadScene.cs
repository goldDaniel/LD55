using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	private static string nextSceneName;

	public static void LoadNextScene(string sceneName)
	{
		SceneManager.LoadScene("Loading Screen");
		nextSceneName = sceneName;
	}

	[SerializeField]
	private Image progressBar;

	void Start()
	{
		StartCoroutine(LoadSceneProgress());
	}

	IEnumerator LoadSceneProgress()
	{
		AsyncOperation loadLevelOp = SceneManager.LoadSceneAsync(nextSceneName);

		while (!loadLevelOp.isDone)
		{
			progressBar.fillAmount = Mathf.Clamp01(loadLevelOp.progress / 0.9f);
			yield return null;
		}

		nextSceneName = null;
	}
}
