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
        GD.Print("Jump Enter");
        Vector2 velocity = _character.Velocity;
        if (_character.IsOnFloor() || !double_jump)
        {
            velocity.Y = _character.JumpVelocity;
            if (!double_jump && !_character.IsOnFloor())
            {
                double_jump = true;
            }
            _character.Velocity = velocity * _character.gravityVector;
        }
    }

    /// <summary>
    /// Update the physics of the player during the jump state.
    /// </summary>
    /// <param name="delta">The time elapsed since the last frame.</param>
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        Vector2 velocity = _character.Velocity;

        // Add gravity
        if (!_character.IsOnFloor())
        {
            velocity.Y += _gravity.Y * (float)delta;
            velocity.X += _gravity.X * (float)delta;
           
            // this turns the acceleration vector of gravity into a velocity vector by multiplying it by time.
            // since (m/t^2) * t = (m/t)
            
        }
        else
        {
            double_jump = false;
            if (velocity == Vector2.Zero)
            {
                _stateMachine.TransitionTo("Idle");
            }
            else
            {
                _stateMachine.TransitionTo("Move");
            }
        }

        // Handle horizontal movement during jump
        Vector2 direction = Input.GetVector(GameConstants.Input.MoveLeft, GameConstants.Input.MoveRight, GameConstants.Input.MoveUp, GameConstants.Input.MoveDown);
        if (direction != Vector2.Zero)
        {
            velocity.X = (direction.X * _character.Speed);
        }
        else
        {
            velocity.X = Mathf.MoveToward(_character.Velocity.X, 0, _character.Speed);
        }
        if (_character.IsInGravityZone)
        {
            velocity = UtilityScripts.RotateDirectionBasedOnGravity(velocity, _gravity);
        }

        _character.Velocity = velocity;
       
        if (velocity == Vector2.Zero)
        {
            _stateMachine.TransitionTo("Idle");
        }
        
        _character.MoveAndSlide();
    }
}
