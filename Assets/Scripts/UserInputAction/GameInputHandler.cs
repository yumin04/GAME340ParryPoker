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
        inputActions.Player1.Up.performed += _ => Player1Up();
        inputActions.Player1.Down.performed += _ => Player1Down();
        inputActions.Player1.Left.performed += _ => Player1Left();
        inputActions.Player1.Right.performed += _ => Player1Right();
        inputActions.Player1.MainAction.performed += _ => Player1MainAction();

        // ----- Player 2 -----
        inputActions.Player2.Up.performed += _ => Player2Up();
        inputActions.Player2.Down.performed += _ => Player2Down();
        inputActions.Player2.Left.performed += _ => Player2Left();
        inputActions.Player2.Right.performed += _ => Player2Right();
        inputActions.Player2.MainAction.performed += _ => Player2MainAction();
        
        // ----- Editor -----
        inputActions.Editor.CheatKey.performed += _ => EditorCheatKey();
        inputActions.Editor.PauseGame.performed += _ => EditorPauseGame();
    }

    // ---------------- Player 1 ----------------
    private void Player1Up() => throw new System. NotImplementedException();
    private void Player1Down() => throw new System.NotImplementedException();
    private void Player1Left() => throw new System.NotImplementedException();
    private void Player1Right() => throw new System.NotImplementedException();
    private void Player1MainAction() => throw new System.NotImplementedException();

    // ---------------- Player 2 ----------------
    private void Player2Up() => throw new System.NotImplementedException();
    private void Player2Down() => throw new System.NotImplementedException();
    private void Player2Left() => throw new System.NotImplementedException();
    private void Player2Right() => throw new System.NotImplementedException();
    private void Player2MainAction() => throw new System.NotImplementedException();
    
    // ---------------- Editor ----------------
    private void EditorCheatKey() => throw new System.NotImplementedException();
    private void EditorPauseGame() => throw new System.NotImplementedException();
}
