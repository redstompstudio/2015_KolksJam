using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour 
{
	private static SceneManager instance;

    private int valvesOpenned = 0;

	public PlayerScript player;
	public EnemyController enemy;

    public Text ValvesCount;

    void Update()
    {
        ValvesCount.text = valvesOpenned.ToString() + " / 4";
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
    }

    public int GetValveCount()
    {
        return valvesOpenned;
    }
}
