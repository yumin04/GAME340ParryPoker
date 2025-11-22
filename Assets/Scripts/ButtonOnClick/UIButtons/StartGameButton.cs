public class StartGameButton : IButtonListener
{
    public override void OnClick()
    {
        SceneLoader.GetInstance().LoadGameScene();
    }

}