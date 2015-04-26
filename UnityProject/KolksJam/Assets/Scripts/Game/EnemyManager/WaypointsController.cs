using UnityEngine;
using System.Collections;


public class WaypointsController : MonoBehaviour 
{

    public Waypoint m_NearestWaypoint;
    public Waypoint[] m_Waypoints;
    public PlayerScript m_Player;

    [Header("Enemy")]
    public float m_SpawTime;
    public EnemyController[] m_pEnemies;
    public EnemyController m_pCurrentEnemy = null;
    public float TimeToSpawn = 5;
    private float m_fTimeCounter;

	// Use this for initialization
	void Start () 
    {
        m_fTimeCounter = Time.time;
        FindAll();
	}


	[ContextMenu("Find All")]
    public void FindAll()
    {
        //m_pEnemies = FindObjectsOfType<EnemyController>();
        m_Player = FindObjectOfType<PlayerScript>();
        m_Waypoints = GameObject.FindObjectsOfType(typeof(Waypoint)) as Waypoint[];
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (m_NearestWaypoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(m_NearestWaypoint.transform.position, new Vector3(3, 3, 3));
    }
#endif

    void FixedUpdate()
    {
        if (m_pEnemies == null || m_pEnemies.Length <= 0)
        {
            Debug.LogError("porra ta zuado");
            return;
        }
        /*
        if (m_Waypoints != null || m_Waypoints.Length <= 0)
            CheckNearest();*/

        if (m_pCurrentEnemy != null)
        {
            if (m_pCurrentEnemy.CurrentState == EnemyStates.Dead)
            {
                m_pCurrentEnemy = null;
                m_fTimeCounter = 0;
            }
        }
        else
        {
            m_fTimeCounter += Time.time;
            if (m_fTimeCounter > TimeToSpawn)
            {
                CheckNearest();
                TimeToSpawn = Random.Range(5, 15);
                m_pCurrentEnemy = m_pEnemies[Random.Range(0, m_pEnemies.Length)];
                m_pCurrentEnemy.Initialize(m_NearestWaypoint);
            }
        }
    }

    void CheckNearest()
    {
        Waypoint pNearest = null;
        float fSmallestDistance = 999999;
        Vector3 pPosition = m_Player.transform.position;
        foreach (Waypoint pWay in m_Waypoints)
        {
            float fDistance = IAHelper.Distance2D(pPosition, pWay.transform.position);
            if (fDistance < fSmallestDistance)
            {
                fSmallestDistance = fDistance;
                pNearest = pWay;
            }
        }
        m_NearestWaypoint = pNearest;
    }
}
