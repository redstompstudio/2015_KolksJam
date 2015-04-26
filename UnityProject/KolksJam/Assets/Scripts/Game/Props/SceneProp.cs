using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class SceneProp : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] m_MonoEnabler;
    [SerializeField] BoxCollider m_Collider;
    [SerializeField] GameObject m_Object;

    bool m_bReleased = false;

    // Use this for initialization
    void Start()
    {
        m_Collider.isTrigger = true;
    }

    void Awake()
    {
        m_Object.SetActive(false);
    }

    private void OnTriggerEnter(Collider pCollider)
    {
        Debug.Log("OnEnter");
        if (m_bReleased)
            return;
        foreach (MonoBehaviour mono in m_MonoEnabler)
        {
            mono.enabled = true;
            this.enabled = false;
        }
        m_bReleased = true;
        m_Object.SetActive(true);
        SoundList.Play("SfxProp01");
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("OsPirata/Props/Adjust Triggers")]
    static public void CentralizeAll()
    {
        SceneProp[] ways = FindObjectsOfType(typeof(SceneProp)) as SceneProp[];
        foreach (SceneProp way in ways)
        {
             way.SetTrigger();
        }
    }
    public void SetTrigger()
    {
        if (!m_Collider)
            m_Collider = GetComponent<BoxCollider>();
        Vector3 vPos = m_Object.gameObject.transform.position;
        Vector3 vPosTo = vPos;
        vPosTo.y = 0;
        RaycastHit pHit;
        bool bCollide = !Physics.Raycast(vPos, IAHelper.Normalize( vPos - vPosTo), out pHit, 100);
        Debug.LogError("collide:" + bCollide);
        if (bCollide)
        {
            Vector3 pCenter = m_Collider.center;
            pCenter.y = - (transform.position.y - pHit.point.y);
            m_Collider.center = pCenter;
        }
        else
            Debug.LogError("nooo" + bCollide);

        Debug.DrawLine(vPos, IAHelper.Normalize(vPosTo - vPos) * 100);
    }
#endif
}