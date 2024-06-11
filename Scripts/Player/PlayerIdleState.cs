using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.General;
namespace MovementFramework.Scripts.Player;

public partial class PlayerIdleState : CharacterState
{	
    [Export] protected Timer dashReloadNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Idle Ready");
        base._Ready();
		
		
    }

    public override void EnterState()
    {
        // We must declare all the properties we access through `owner` in the `Player.cs` script.
        GD.Print("Idle Enter");
        _character.SpriteNode.Play(GameConstants.PlayerAnimation.AnimIdle);

    }

    public override void PhysicsUpdate(double delta)
    {
        if (Input.IsActionPressed(GameConstants.Input.MoveLeft) ||
            Input.IsActionPressed(GameConstants.Input.MoveRight) || Input.IsActionPressed(GameConstants.Input.MoveUp) ||
            Input.IsActionPressed(GameConstants.Input.MoveDown) || Input.IsActionPressed(GameConstants.Input.Jump))
        {
            _stateMachine.TransitionTo("Move");
        }
        else if (Input.IsActionPressed(GameConstants.Input.Dash) && dashReloadNode.IsStopped())
        {
            _stateMachine.TransitionTo("Dash");
        }
			
    }
}