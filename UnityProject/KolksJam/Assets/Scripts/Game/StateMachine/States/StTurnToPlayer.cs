using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StTurnToPlayer : SKState<EnemyController>
{
    private float m_SmoothLookAtFactor = 1.0f;
    Quaternion m_pLastQuartenion;

    public override void update(float deltaTime)
    {
        Transform p;
        Context.Transform.LookAt(Context.TargetPosition);

        Quaternion pQ = Context.Transform.rotation;
        Vector3 v = Context.Position - Context.Position;
        if (v != Vector3.zero)
        {
            Quaternion pQuart = Quaternion.Lerp(pQ, Quaternion.LookRotation(v), m_SmoothLookAtFactor * Time.deltaTime);
            if (pQuart != m_pLastQuartenion)
            {
                m_pLastQuartenion = pQuart;
                Context.Transform.rotation = m_pLastQuartenion;
            }
            else
                Finish();
        }
        else
            Finish();
    }

    private void Finish()
    {
        Context.EndState();
    }
}
