public class RotationModifier : IModifier
{
    private float extraRotationSpeed;

    public RotationModifier(float extraSpeed)
    {
        extraRotationSpeed = extraSpeed;
    }

    public void Apply(AttackObject obj)
    {
        obj.rotationSpeed += extraRotationSpeed;
    }

    public void UpdateModifier(AttackObject obj)
    {
        // per-frame 로직 필요 없음 (Behavior에서 처리)
    }
}