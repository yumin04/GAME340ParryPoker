using UnityEngine;

public class ColorIncreaseRedModifier : IModifier
{
    private float step;   // Apply될 때마다 red를 얼마나 증가할지

    public ColorIncreaseRedModifier(float increaseAmount)
    {
        step = increaseAmount; // 예: 0.2f
    }

    public void Apply(AttackObject obj)
    {
        Color c = obj.color;

        // R만 올리고, G/B는 proportionally 감소시키기 위해  
        // white → red 로 점점 이동시키는 방식
        c.r = Mathf.Clamp01(c.r + step);
        c.g = Mathf.Clamp01(c.g - step);
        c.b = Mathf.Clamp01(c.b - step);

        obj.color = c;
    }

    public void UpdateModifier(AttackObject obj)
    {
        // 계속 업데이트할 필요 없음
    }
}