using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StDespawn  : SKState<EnemyController>
{
    const float MoveTime = 1f;
    const iTween.EaseType EasyType = iTween.EaseType.easeInElastic;
    const float FinalPosition = -10;
    const float StartPosition = 0;

    private bool m_bFinished = false;

    public override void begin()
    {
        base.begin();
        m_bFinished = false;

        Context.SetPositionY(StartPosition);
        iTween.MoveTo(Context.gameObject,
            iTween.Hash(
            "y", FinalPosition,
            "time", MoveTime,
            "easytype", EasyType
            ));

        Context.StartCoroutine(WaitSpawnAnim());
    }

    IEnumerator WaitSpawnAnim()
    {
        yield return new WaitForSeconds(MoveTime);
        Finish();
    }

    private void Finish()
    {
        m_bFinished = true;
        Context.EndState();
    }

    public override void update(float deltaTime)
    {
        
    }
}
