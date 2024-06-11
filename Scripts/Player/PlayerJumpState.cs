using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.General;
namespace MovementFramework.Scripts.Player;

public partial class PlayerJumpState : CharacterState
{
    /// <summary>
    /// Indicates whether the player has performed a double jump.
    /// </summary>
    private bool double_jump = false;
    
    /// <summary>
    /// Method to enter the PlayerJumpState.
    /// </summary>
    public override void EnterState()
    {
        Vector2 velocity = _character.Velocity;
        if (_character.IsOnFloor() || !double_jump)
        {
            velocity.Y = _character.JumpVelocity;
            if (!double_jump && !_character.IsOnFloor())
            {
                double_jump = true;
            }
            _character.Velocity = velocity;
        }
    }

    /// <summary>
    /// Update the physics of the player during the jump state.
    /// </summary>
    /// <param name="delta">The time elapsed since the last frame.</param>
    public override void PhysicsUpdate(double delta)
    {
        Vector2 velocity = _character.Velocity;

        // Add gravity
        if (!_character.IsOnFloor())
        {
            velocity.Y += _character.gravity * (float)delta;
        }
        else
        {
            double_jump = false;
            _stateMachine.TransitionTo("Move");
        }

        // Handle horizontal movement during jump
        Vector2 direction = Input.GetVector(GameConstants.Input.MoveLeft, GameConstants.Input.MoveRight, GameConstants.Input.MoveUp, GameConstants.Input.MoveDown);
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _character.Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(_character.Velocity.X, 0, _character.Speed);
        }

        _character.Velocity = velocity;
        _character.MoveAndSlide();
    }
}
