    using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31.StateKit;


public class StFollowPath : SKState<EnemyController>
{
    int m_iCurrentWaypointIndex;
    //List<Vector3> m_pPath;
    private Vector3 m_pReachPoint;

    private bool m_bWalking = false;
    float m_fWalkingTime = 0;
    float m_fWaitingTime = 0;
    bool m_bRunning = false;
    float m_fOriginalVelocity;
    bool m_bFirstRun = true;

    public override void begin()
    {
        base.begin();
        if (m_bFirstRun)
        {
            m_bFirstRun = false;
            m_fOriginalVelocity = Context.NavMeshAgent.speed;
        }
        List<Transform> pTrans = Context.GetPath();
        m_pReachPoint = Context.TargetPosition;
        Context.NavMeshAgent.Resume();
        Context.NavMeshAgent.destination = m_pReachPoint;
        Context.Animator.SetFloat("Speed", 1);
        m_bWalking = true;
        m_bRunning = !m_bRunning;
    }


    public override void reason()
    {
        base.reason();
        /*if (!Context.NavMeshAgent.remainingDistance)
            Context.EndState();*/        
        if (Context.IsInRange(m_pReachPoint))
            Context.EndState();
    }

    public override void update(float deltaTime)
    {
        // do not need to stop
        if(Context.m_WaitWalking <= 0)
            return;

        if (m_bWalking )
        {
            m_fWalkingTime += deltaTime;
            if (m_fWalkingTime >= Context.m_WaitWalking)
            {
                m_fWalkingTime = 0;
                Context.NavMeshAgent.Stop();
                //Vector3 pVect = IAHelper.Normalize(Context.NavMeshAgent.velocity);
                //if (!float.IsNaN(pVect.x) && !float.IsNaN(pVect.y) && !float.IsNaN(pVect.z) )
                //    Context.NavMeshAgent.velocity = pVect;
                Context.NavMeshAgent.velocity = Vector3.zero;
                m_bWalking = false;
                Context.Animator.SetFloat("Speed", 0);
            }
        }
        if (!m_bWalking)
        {
            m_fWaitingTime += deltaTime;
            if (m_fWaitingTime >= Context.m_WaitWalkingBack)
            {
                m_fWaitingTime = 0;
                if (m_bRunning)
                    Context.NavMeshAgent.speed = m_fOriginalVelocity * 2;
                else
                    Context.NavMeshAgent.speed = m_fOriginalVelocity;
                Context.NavMeshAgent.acceleration = Context.NavMeshAgent.speed;
                begin();
            }
        }

        // do not need it if using navmesh
        //UpdateManualPath();
    }
    /*
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
    }*/

    /// <summary>
    /// reachhh
    /// </summary>
    private void OnReachPath()
    {
        Context.EndState();
    }

    public override void end()
    {
        base.end();
        // stop moving
        //Context.NavMeshAgent.destination = Context.Position;
        Context.NavMeshAgent.Stop();
    }
}
