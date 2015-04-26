using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StDespawn  : SKState<EnemyController>
{
    const iTween.EaseType EasyType = iTween.EaseType.easeInElastic;
    const float FinalPosition = 2;
    const float StartPosition = IADefs.GroundPosition;

    private bool m_bFinished = false;

    public override void begin()
    {
        base.begin();
        m_bFinished = false;

        Context.Animator.SetFloat("Speed", 0);
        Context.StartCoroutine(WaitSpawnAnim());
    }

    void DoAnim()
    {
        //Context.SetPositionY(StartPosition);
        iTween.MoveTo(Context.gameObject,
            iTween.Hash(
            "y", Context.Position.y - FinalPosition,
            "time", IADefs.DespawnTime,
            "easytype", EasyType
            ));
    }

    IEnumerator WaitSpawnAnim()
    {
        yield return new WaitForSeconds(IADefs.DespawnTimeDelay);
        DoAnim();
        yield return new WaitForSeconds(IADefs.DespawnTime);
        
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
