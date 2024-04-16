
public class EndGameDoor : Door
{
	public override void Unlock()
	{
		LoadScene.LoadNextScene("Win Scene");
		Destroy(this.gameObject);
	}
}
