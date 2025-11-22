using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputHandler : MonoBehaviour
{
    private GameInputAction inputActions;

    private void Awake()
    {
        inputActions = new GameInputAction();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        // ----- Player 1 -----
        // inputActions.Player1.Up.performed += _ => Player1Up();
        // inputActions.Player1.Down.performed += _ => Player1Down();
        // inputActions.Player1.Left.performed += _ => Player1Left();
        // inputActions.Player1.Right.performed += _ => Player1Right();
        // inputActions.Player1.MainAction.performed += _ => Player1MainAction();

        // // ----- Player 2 -----
        // inputActions.Player2.Up.performed += _ => Player2Up();
        // inputActions.Player2.Down.performed += _ => Player2Down();
        // inputActions.Player2.Left.performed += _ => Player2Left();
        // inputActions.Player2.Right.performed += _ => Player2Right();
        // TODO: change this, this is space key btw
        inputActions.Player2.MainAction.performed += _ => Player1MainAction();
        //
        // // ----- Editor -----
        // inputActions.Editor.CheatKey.performed += _ => EditorCheatKey();
        // inputActions.Editor.PauseGame.performed += _ => EditorPauseGame();
    }

    // ---------------- Player 1 ----------------
    // private void Player1Up() => IUserInterface.GetInstance().HandlePlayer1Up();
    // private void Player1Down() => IUserInterface.GetInstance().HandlePlayer1Down();
    // private void Player1Left() => IUserInterface.GetInstance().HandlePlayer1Left();
    // private void Player1Right() => IUserInterface.GetInstance().HandlePlayer1Right();
    private void Player1MainAction() => Player.GetInstance().HandleMainAction();

//     // ---------------- Player 2 ----------------
//     private void Player2Up() => IUserInterface.GetInstance().HandlePlayer2Up();
//     private void Player2Down() => IUserInterface.GetInstance().HandlePlayer2Down();
//     private void Player2Left() => IUserInterface.GetInstance().HandlePlayer2Left();
//     private void Player2Right() => IUserInterface.GetInstance().HandlePlayer2Right();
//     private void Player2MainAction() => IUserInterface.GetInstance().HandlePlayer2MainAction();
//     
//     // ---------------- Editor ----------------
//     private void EditorCheatKey() => IUserInterface.GetInstance().HandleEditorCheatKey();
//     private void EditorPauseGame() => IUserInterface.GetInstance().HandleEditorPauseKey();
}
