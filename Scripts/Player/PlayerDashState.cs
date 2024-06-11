using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.General;
namespace MovementFramework.Scripts.Player;

/// <summary>
/// Represents the state when the player is dashing.
/// </summary>
public partial class PlayerDashState : CharacterState
{
    /// <summary>
    /// Represents the dash timer node in the PlayerDashState class.
    /// </summary>
    [Export] protected Timer dashTimerNode;

    /// <summary>
    /// Represents the timer node used for dash reload in the PlayerDashState class.
    /// </summary>
    [Export] protected Timer dashReloadNode;

    /// <summary>
    /// The speed of the player when dashing.
    /// </summary>
    [Export(PropertyHint.Range, "300,800,10")] private float speed = 500;
    
    // Called when the node enters the scene tree for the first time.
    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        GD.Print("Dash Ready");
        dashTimerNode.Timeout += HandleDashTimeout;
        base._Ready();
	
		
    }

    /// <summary>
    /// Enters the player dash state.
    /// </summary>
    /// <remarks>
    /// This method is called when the player transitions into the dash state.
    /// It sets the player's velocity to the dash direction and starts the dash timer.
    /// </remarks>
    public override void EnterState()
    {
        // We must declare all the properties we access through `owner` in the `Player.cs` script.
        GD.Print("Dash Enter");
        Vector2 direction = Input.GetVector(GameConstants.Input.MoveLeft, GameConstants.Input.MoveRight, GameConstants.Input.MoveUp, GameConstants.Input.MoveDown);
        
        _character.Velocity = new(
            direction.X, 0
        );

        if (_character.Velocity == Vector2.Zero)
        {
            _character.Velocity = _character.SpriteNode.FlipH ?
                Vector2.Left :
                Vector2.Right;
        }

        _character.Velocity *= speed;
        _character.SpriteNode.Play(GameConstants.PlayerAnimation.AnimSliding);
        dashTimerNode.Start();


    }

    /// <summary>
    /// This method is called during the physics update of the PlayerDashState.
    /// It moves and slides the player and flips its direction.
    /// </summary>
    /// <param name="delta">The time elapsed since the last frame, in seconds.</param>
    public override void PhysicsUpdate(double delta)
    {
        _character.MoveAndSlide();
        _Flip();

    }

    /// <summary>
    /// Handles the timeout of the dash timer.
    /// </summary>
    private void HandleDashTimeout()
    {
        _character.Velocity = Vector2.Zero;
        _stateMachine.TransitionTo("Idle");
        dashReloadNode.Start();
    }


}