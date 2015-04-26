using UnityEngine;
using System.Collections;
using Prime31.StateKit;


public class StAttack : SKState<EnemyController>
{

    public override void begin()
    {
        base.begin();
        Context.Animator.SetTrigger("Attack");
        Context.StartCoroutine(AttackLoop());

        Context.Player.Kill();
        Context.Player.transform.LookAt(Context.MyFace);

        SoundList.PlayOneShot("SfxSusto");
    }

    IEnumerator AttackLoop()
    {
        Context.Player.transform.LookAt(Context.MyFace);
        yield return new WaitForSeconds(.5f);
        Context.EndState();
    }

    public override void update(float deltaTime)
    {
        Context.Player.transform.LookAt(Context.MyFace);
        Vector3 pPos = Context.Player.transform.position;
        pPos.y = Context.Position.y;
        Context.Transform.LookAt(pPos);

        if (Context.IsInRange(Context.TargetPosition))
            return;
        MoveTo(Context.TargetPosition);
        Context.Transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
    }

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
}