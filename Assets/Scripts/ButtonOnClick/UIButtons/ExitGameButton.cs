
public class ExitGameButton : IButtonListener
{
    public override void OnClick()
    {
        SceneLoader.GetInstance().ExitGame();
    }

}
