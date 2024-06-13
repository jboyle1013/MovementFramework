using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.General;
namespace MovementFramework.Scripts.Enemy;

public partial class EnemyMoveState : CharacterState
{
    [Export] protected Timer dashReloadNode;

    /// <summary>
    /// Updates the physics of the player.
    /// </summary>
    /// <param name="delta">The time elapsed since the last physics frame.</param>
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        Vector2 velocity = _character.Velocity;

        // Add the gravity.
        if (!_character.IsOnFloor())
            velocity.Y -= _gravity.Y * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed(GameConstants.Input.Jump))
        {
            _stateMachine.TransitionTo("Jump");
            return; // Exit early to prevent further updates in this frame
        }

        // Get the input direction and handle the movement/deceleration.
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
        _character.SpriteNode.Play(GameConstants.PlayerAnimation.AnimMoving);
        _Flip();

        if (velocity == Vector2.Zero)
        {
            _stateMachine.TransitionTo("Idle");
        }
        else if (Input.IsActionPressed(GameConstants.Input.Dash) && dashReloadNode.IsStopped())
        {
            _stateMachine.TransitionTo("Dash");
        }
    }
}