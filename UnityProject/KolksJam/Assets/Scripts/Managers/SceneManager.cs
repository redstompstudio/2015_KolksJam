using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
	private static SceneManager instance;

    private int valvesOpenned = 0;

	public PlayerScript player;
	public EnemyController enemy;

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
    }

    public int GetValveCount()
    {
        return valvesOpenned;
    }
}
