using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.General;

namespace MovementFramework.Scripts.Player
{
    public partial class PlayerMoveState : CharacterState
    {
        [Export] protected Timer dashReloadNode;
        private bool double_jump = false;
        private float nAngle;

        public override void EnterState()
        {
            GD.Print("Move Enter");
        }

        /// <summary>
        /// Updates the physics of the player.
        /// </summary>
        /// <param name="delta">The time elapsed since the last physics frame.</param>
        public override void PhysicsUpdate(double delta)
        {
            
            base.PhysicsUpdate(delta);
            Vector2 velocity = _character.Velocity;

            // Handle horizontal movement
            Vector2 direction = Input.GetVector(GameConstants.Input.MoveLeft, GameConstants.Input.MoveRight, GameConstants.Input.MoveUp, GameConstants.Input.MoveDown);
            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * _character.Speed;
                
            }
            else
            {
                velocity.X = Mathf.MoveToward(_character.Velocity.X, 0, _character.Speed);
            }

            if (_character.IsInGravityZone)
            {
                velocity = UtilityScripts.RotateDirectionBasedOnGravity(velocity, _gravity);
            }

            // Handle Jump
            if (Input.IsActionJustPressed(GameConstants.Input.Jump))
            {
                if (_character.IsOnFloor() || !double_jump)
                {
                    velocity.Y = _character.JumpVelocity * _character.gravityVector.Y;
                    if (!double_jump && !_character.IsOnFloor())
                    {
                        double_jump = true;
                    }
                }
            }
            
            // Add the gravity.
            if (!_character.IsOnFloor () )
            {
                velocity += _gravity * (float) delta;
            }   
            else
            {
                double_jump = false;
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
}
