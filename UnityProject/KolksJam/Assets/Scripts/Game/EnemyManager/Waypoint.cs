using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{

    public GameObject[] m_WaypointView = new GameObject[2];
    private Vector3[] m_pPositions;

    public Vector3[] Positions
    {
        get { return m_pPositions; }
        set { m_pPositions = value; }
    }

    // Use this for initialization
    void Start()
    {
        m_pPositions = new Vector3[2];
        for (int i = m_WaypointView.Length -1; i >= 0; i--)
        {
            m_pPositions[i] = m_WaypointView[i].transform.position;
            //Destroy(m_WaypointView[i]);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("OsPirata/Waypoints/Centralize All")]
    static public void CentralizeAll()
    {
        Waypoint[] ways = FindObjectsOfType(typeof(Waypoint)) as Waypoint[];
        foreach (Waypoint way in ways)
        {
             way.Centralize();
        }
    }
    public void Centralize()
    {
        Vector3 vNewPos = Vector3.Lerp(m_WaypointView[0].transform.position, m_WaypointView[1].transform.position, 0.5f);
        Vector3 Po1 = m_WaypointView[0].transform.position;
        Vector3 Po2 = m_WaypointView[1].transform.position;
        // set pos
        transform.position = vNewPos;
        m_WaypointView[0].transform.position = Po1;
        m_WaypointView[1].transform.position = Po2;
    }

    void OnDrawGizmos()
    {
        if (m_WaypointView[0] != null && m_WaypointView[1] != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(m_WaypointView[0].transform.position, m_WaypointView[1].transform.position);
            //Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
        else if (m_pPositions[0] != null && m_pPositions[1] != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(m_pPositions[0], m_pPositions[1]);
        }
    }
#endif


    /// <summary>
    /// gets a random position between positions
    /// </summary>
    /// <returns></returns>
    public void RandomizePosition()
    {
        m_pPositions.Shuffle<Vector3>();
        //return m_pPositions[Random.Range(0, m_pPositions.Length - 1)];
    }
}
