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
    Idle,
    Dead,
}

public class EnemyController : MonoBehaviour
{
    // serializeds
    public Transform MyFace;
	public Transform m_pTransform;
    public NavMeshAgent m_pNavMeshAgent;
    public Animator m_pAnimator;
    private PlayerScript m_pPlayer;
    public List<Transform> Path;    
    public float MoveSpeed = 5;
    [HideInInspector] public Vector3 StartPosition;

    public float m_WaitWalking = 0;

    // privates
    private SKStateMachine<EnemyController> m_pMachine;
    private EnemyStates m_eCurrentState;
    private Vector3 m_pTarget;



    // events
    public delegate void OnChangeStateCallback(EnemyController pEnemy, EnemyStates eOldState, EnemyStates eNewState);
    public event OnChangeStateCallback OnChangeState;


    void Reset()
    {
        Player = FindObjectOfType<PlayerScript>();
        m_pTransform= GetComponent<Transform>();
        m_pNavMeshAgent = GetComponent<NavMeshAgent>();
        m_pAnimator = GetComponent<Animator>();
    }

    #region gets/sets
    public PlayerScript Player
    {
        get { return m_pPlayer; }
        set { m_pPlayer = value; }
    }

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
    public Animator Animator
    {
        get { return m_pAnimator; }
    }
    /*public Transform Target
    {
        get { return m_pTarget; }
        set { m_pTarget = value; }
    }*/
    public Vector3 TargetPosition
    {
        get { return m_pTarget; }
        set { m_pTarget = value; }
    }
    #endregion

    #region Startup
    void Awake()
    {
        StartupStates();
    }

    public void Initialize(Waypoint pWaypoint)
    {
        // pos
        StartPosition = Position;
        // do not know why the hell the start position does not work
        StartPosition.y = 0.08332921f;  

        if (!Player)
            Player = FindObjectOfType<PlayerScript>();
        StopAllCoroutines();
        // set configs
        pWaypoint.RandomizePosition();
        TargetPosition = pWaypoint.Positions[1];
        SetPosition(pWaypoint.Positions[0]);
        gameObject.SetActive(true);
        // init
        SetState(EnemyStates.Spawn);

    }
    void Start()
	{
        gameObject.SetActive(false);
	}
    void StartupStates()
    {
        // the initial state has to be passed to the constructor
        Machine = new SKStateMachine<EnemyController>(this, new StNothing());

        // we can now add any additional states
        Machine.addState(new StSpawn());
        Machine.addState(new StFollowPath());
        Machine.addState(new StDespawn());
        Machine.addState(new StTurnToPlayer());
        Machine.addState(new StAttack());
        Machine.addState(new StIdle());
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

        CheckPlayerDistance();
	}

    void CheckPlayerDistance()
    {
        if (Player)
        {
            if (IAHelper.Distance2D(Position, Player.transform.position) > 30)
            {
                SetState(EnemyStates.Dead);
            }
        }
    }



    #region states
    /// <summary>
    /// change of natural states
    /// </summary>
    public virtual void EndState()
    {
        EnemyStates eNextState = EnemyStates.None;
        switch (CurrentState)
        {
            case EnemyStates.Spawn:
                eNextState = EnemyStates.FollowPath;
                break;
            case EnemyStates.FollowPath:
                eNextState = EnemyStates.Despawn;
                break;
            case EnemyStates.TurnToPlayer:
                eNextState = EnemyStates.Attack;
                break;
            case EnemyStates.Attack:
                eNextState = EnemyStates.Idle;
                break;
            case EnemyStates.Despawn:
                eNextState = EnemyStates.Dead;
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
            
            case EnemyStates.Idle: Machine.changeState<StIdle>(); break;
            case EnemyStates.Dead:
                 Machine.changeState<StIdle>();
                 gameObject.SetActive(false);
                 break;
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

    #endregion

    

    #region Triggers
    Collider m_pOnRange = null;
    private void OnTriggerStay(Collider pCollider)
    {
        //if (pCollider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            float fAngle = Vector3.Angle(Transform.forward, Player.transform.position - Position);
            if (fAngle <= 90 && m_pOnRange == null)
            {
                m_pOnRange = pCollider;
                TargetPosition = transform.position;
                SetState(EnemyStates.TurnToPlayer);
            }
            //Debug.Log(fAngle.ToString());
        }
    }
    private void OnTriggerExit(Collider pCollider)
    {
       // if (pCollider.gameObject.layer == LayerMask.NameToLayer("Player"))
        if (pCollider == m_pOnRange)
        {
            m_pOnRange = null;
            TargetPosition = Vector3.zero;
            SetState(EnemyStates.FollowPath);
            
        }
    }
    #endregion



    void OnGUI()
    {
        GUI.Label(new Rect(0, 50, 400, 50), CurrentState.ToString());// + "  - " + IAHelper.Distance2D(Position, Player.transform.position));
    }
}
