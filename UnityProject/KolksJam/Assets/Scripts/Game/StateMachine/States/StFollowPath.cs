using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31.StateKit;


public class StFollowPath : SKState<EnemyController>
{
    int m_iCurrentWaypointIndex;
    List<Vector3> m_pPath;
    private Vector3 m_pReachPoint;

    public override void begin()
    {
        base.begin();

        List<Transform> pTrans = Context.GetPath();
        m_pPath = new List<Vector3>();
        for (int i = 0; i < pTrans.Count; i++)
            m_pPath.Add(pTrans[i].position);
        m_iCurrentWaypointIndex = 0;

        m_pReachPoint = m_pPath[m_pPath.Count - 1];;
        Context.NavMeshAgent.destination = m_pReachPoint;
    }

    public override void reason()
    {
        base.reason();

        if (Context.IsInRange(m_pReachPoint))
            Context.EndState();
    }

    public override void update(float deltaTime)
    {
        // do not need it if using navmesh
        //UpdateManualPath();
    }

    private void UpdateManualPath()
    {
        while (true)
        {
            if (m_iCurrentWaypointIndex < m_pPath.Count)
            {
                //There is a "next path segment"
                float fRad = Mathf.Max(Context.ColliderRadius / 2, 0.5f);
                bool bInside = Context.IsInRange(m_pPath[m_iCurrentWaypointIndex]);
                if (bInside)
                {
                    m_iCurrentWaypointIndex++;
                    if (m_iCurrentWaypointIndex >= m_pPath.Count)
                    {
                        OnReachPath();
                        return;
                    }
                }
                else
                    break;
            }
            else
                break;
        }

        if (m_iCurrentWaypointIndex < m_pPath.Count)
            MoveTo(m_pPath[m_iCurrentWaypointIndex]);
    }

    /// <summary>
    /// move that modafucka
    /// </summary>
    /// <param name="pPoint"></param>
    private void MoveTo(Vector3 pPoint)
    {
        // uses same Y
        Vector3 vPos = Context.Position;
        pPoint.y = vPos.y;
        Vector3 pp = pPoint - vPos;
        Vector3 vSpeed = IAHelper.Normalize(pPoint - vPos) * Context.MoveSpeed * Time.deltaTime;

        if (float.IsNaN(vSpeed.x) || float.IsNaN(vSpeed.y) || float.IsNaN(vSpeed.z))
            vSpeed = Vector3.zero;

        //move
        Vector3 vNewPos = vPos + vSpeed;
        Context.SetPosition(vNewPos);
    }

    /// <summary>
    /// reachhh
    /// </summary>
    private void OnReachPath()
    {
        Context.EndState();
    }
}
