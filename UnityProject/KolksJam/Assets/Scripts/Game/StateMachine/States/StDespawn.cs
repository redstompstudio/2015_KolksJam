using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StDespawn  : SKState<EnemyController>
{
    const iTween.EaseType EasyType = iTween.EaseType.linear;
    const float FinalPosition = 2;
    const float StartPosition = IADefs.GroundPosition;

    private bool m_bFinished = false;

    public override void begin()
    {
        base.begin();
        m_bFinished = false;

        Context.Animator.SetFloat("Speed", 0);
        Context.Animator.SetBool("Despawing", true);
        
        Context.StartCoroutine(WaitSpawnAnim());
    }

    void DoAnim()
    {
        if (Context.m_MoveOnSpawnAndDespawn)
        {
            //Context.SetPositionY(StartPosition);
            iTween.MoveTo(Context.gameObject,
                iTween.Hash(
                "y", Context.StartPosition.y - FinalPosition,
                "time", IADefs.DespawnTime,
                "easytype", EasyType
                ));
        }
        SoundList.Play3D("SfxDespawn");
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
