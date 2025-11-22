using UnityEngine;

public class MovingAttackBehavior : IAttackBehavior
{
    public void Tick(AttackObject obj)
    {
        // 1) 앞으로 이동
        obj.transform.Translate(Vector3.right * obj.speed * Time.deltaTime, Space.World);

        // 2) 수리검 회전
        if (obj.rotationSpeed != 0f)
        {
            // 누적
            obj.rotationAngle += obj.rotationSpeed * Time.deltaTime;

            // transform에 반영
            obj.transform.rotation = Quaternion.Euler(0, 0, obj.rotationAngle);
        }

        // 3) 색 적용
        obj.sr.color = obj.color;
    }
}