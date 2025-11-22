public class SpeedModifier : IModifier
{
    private float extraSpeed;

    public SpeedModifier(float speed)
    {
        extraSpeed = speed;
    }

    public void Apply(AttackObject obj)
    {
        obj.speed += extraSpeed;
    }

    public void UpdateModifier(AttackObject obj)
    {
        // not needed per frame
    }
}