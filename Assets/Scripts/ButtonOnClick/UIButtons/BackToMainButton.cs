public class BackToMainButton : IButtonListener
{
    public override void OnClick()
    {
        SceneLoader.GetInstance().LoadMainScene();
    }

}