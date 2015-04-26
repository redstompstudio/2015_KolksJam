using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class StSpawn : SKState<EnemyController>
{
    const iTween.EaseType EasyType = iTween.EaseType.easeInElastic;
    const float FinalPosition = IADefs.GroundPosition;
    const float StartPosition = 2;
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

        Context.SetPositionY(Context.StartPosition.y - StartPosition);
        iTween.MoveTo(Context.gameObject,
            iTween.Hash(
            "y", Context.StartPosition.y,
            "time", IADefs.SpawnTime,
            "easytype", EasyType
            ));
        SoundList.Play3D("SfxSpawn");
            
    }

    IEnumerator WaitSpawnAnim()
    {
        DoAnim();
        yield return null;
        yield return new WaitForSeconds(IADefs.SpawnTime);
        yield return new WaitForSeconds(IADefs.SpawnTimeDelay);
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
