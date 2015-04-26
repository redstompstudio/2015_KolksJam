using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour 
{
	private static SceneManager instance;

    private int valvesOpenned = 0;

	public PlayerScript player;
	public EnemyController enemy;

    public Text valvesCountText;

	public int valvesCount = 9;

    void Update()
    {
		if(valvesCountText != null)
        valvesCountText.text = valvesOpenned.ToString() + " / " + valvesCount.ToString();
    }

	public static SceneManager Instance
	{
		get{
			if(!instance)
			{
				instance = FindObjectOfType(typeof(SceneManager)) as SceneManager;

				if(!instance)
					instance = new GameObject("SceneManager").AddComponent<SceneManager>();
			}

			return instance;
		}
	}

    public void IncrementValveCount()
    {
        valvesOpenned++;

		if(valvesOpenned >= valvesCount)
			FinalCutscene.instance.StartCutscene();
    }

    public int GetValveCount()
    {
        return valvesOpenned;
    }
}