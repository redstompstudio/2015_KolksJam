using UnityEngine;
using System.Collections.Generic;
using Prime31.StateKit;

public enum EnemyStates
{
    None,
    Spawn,
    FollowPath,
    Despawn,
    TurnToPlayer,
    Attack,
}

public class EnemyController : MonoBehaviour
{
    // serializeds
	public Transform m_pTransform;
    public NavMeshAgent m_pNavMeshAgent;
    public List<Transform> Path;
    public float MoveSpeed = 5;

    // privates
    private SKStateMachine<EnemyController> m_pMachine;
    private EnemyStates m_eCurrentState;

    // events
    public delegate void OnChangeStateCallback(EnemyController pEnemy, EnemyStates eOldState, EnemyStates eNewState);
    public event OnChangeStateCallback OnChangeState;

    
    

    #region gets/sets

    /// <summary>
    /// the machine
    /// </summary>
    public SKStateMachine<EnemyController> Machine
    {
        get { return m_pMachine; }
        set { m_pMachine = value; }
    }
    public EnemyStates CurrentState
    {
        get { return m_eCurrentState; }
        set { m_eCurrentState = value; }
    }
    public Transform Transform
    {
        get { if (m_pTransform)  m_pTransform = gameObject.transform; return m_pTransform; }
    }
    public Vector3 Position
    {
        get { return Transform.position; }
    }
    public NavMeshAgent NavMeshAgent
    {
        get { return m_pNavMeshAgent; }
    }
    #endregion

    #region Startup

    void Start()
	{
        StartupStates();
        SetState(EnemyStates.Spawn);
	}
    void StartupStates()
    {
        // the initial state has to be passed to the constructor
        Machine = new SKStateMachine<EnemyController>(this, new StSpawn());

        // we can now add any additional states
        Machine.addState(new StFollowPath());
        Machine.addState(new StDespawn());
        Machine.addState(new StTurnToPlayer());
        Machine.addState(new StAttack());
    }

    #endregion


    #region helps
    public void SetPosition(Vector3 vPos)
    {
        gameObject.transform.position = vPos;
    }
    public void SetPositionY(float fY)
    {
        Vector3 pPos = Position;
        pPos.y = fY;
        SetPosition(pPos);
    }
    public float ColliderRadius
    {
        get { return 5; }
    }

    public bool IsInRange(Vector3 pPoint)
    {
        return IsInRange(Position, pPoint);
    }
    public bool IsInRange(Vector3 pOrigin, Vector3 pTarget, float fTargetRadius = 1)
    {
        float fDistance = IAHelper.Distance2D(pOrigin, pTarget);
        float fSize = fTargetRadius;
        return fDistance <= fSize;
    }

    public List<Transform> GetPath()
    {
        return Path;
    }
    #endregion

    protected virtual void Update()
	{
		// update the state machine
		Machine.update( Time.deltaTime );
	}


    /// <summary>
    /// change of natural states
    /// </summary>
    public virtual void EndState()
    {
        EnemyStates eNextState = EnemyStates.None;
        switch (CurrentState)
        {
            case EnemyStates.Spawn:
                //eNextState = EnemyStates.Despawn;
                eNextState = EnemyStates.FollowPath;
                break;
            case EnemyStates.FollowPath:
                eNextState = EnemyStates.Despawn;
                break;
            case EnemyStates.TurnToPlayer:
                eNextState = EnemyStates.Attack;
                break;
        }
        if (eNextState != EnemyStates.None)
            SetState(eNextState);
    }

    /// <summary>
    /// sets the state of the enemy
    /// </summary>
    /// <param name="eNextState"></param>
    public virtual void SetState(EnemyStates eNextState)
    {
        EnemyStates eOldState = CurrentState;
        switch (eNextState)
        {
            case EnemyStates.Spawn: Machine.changeState<StSpawn>(); break;
            case EnemyStates.FollowPath: Machine.changeState<StFollowPath>(); break;
            case EnemyStates.TurnToPlayer: Machine.changeState<StTurnToPlayer>(); break;
            case EnemyStates.Attack: Machine.changeState<StAttack>(); break;
            case EnemyStates.Despawn: Machine.changeState<StDespawn>(); break;
        }
        CurrentState = eNextState;
    }

    /// <summary>
    /// change state callback
    /// </summary>
    /// <param name="eOldState"></param>
    /// <param name="eNewState"></param>
    public virtual void _OnChangeState(EnemyStates eOldState, EnemyStates eNewState)
    {
        if (OnChangeState != null)
            OnChangeState(this, eOldState, eNewState);
    }



    void OnGUI()
    {
        GUI.Label(new Rect(0, 50, 400, 50), CurrentState.ToString());
    }
}
