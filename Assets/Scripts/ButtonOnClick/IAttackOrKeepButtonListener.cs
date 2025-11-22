public abstract class IAttackOrKeepButtonListener : ISelectionButton
{
    protected int row = 0;
    public void OnEnable()
    {
        GameEvents.OnAttackOrKeepButtonClicked += OnOtherButtonClicked;
        
    }
    public void OnDisable()
    {
        GameEvents.OnAttackOrKeepButtonClicked -= OnOtherButtonClicked;

    }
}