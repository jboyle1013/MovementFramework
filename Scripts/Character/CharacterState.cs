using Godot;
using System;
using MovementFramework.Scripts.StateMachine;

namespace MovementFramework.Scripts.Character;

public abstract partial class CharacterState : State
{
    /// <summary>
    ///  Typed reference to the player node.
    /// </summary>
    protected Character _character;

    public override void _Ready()
    {
        // Use call_deferred to safely wait for the owner to be ready
        CallDeferred(nameof(Initialize));
        _character = Owner as Character;
    }

    private void Initialize()
    {
        // The `as` keyword casts the `owner` variable to the `Player` type. If the `owner` is not a `Player`, we'll get `null`.
        

        // This check will tell us if we inadvertently assign a derived state script in a scene other than `Player.tscn`, which would be unintended. This can help prevent some bugs that are difficult to understand.
        if (_character == null)
        {
            throw new InvalidProgramException("Player is null in the PlayerState type check.");
        }
    }
    public void _Flip()
    {
        if(_character.Velocity.X != 0)
            _character.SpriteNode.FlipH = _character.Velocity.X<0;
    }

    // public void _Rotate(float delta)
    // {
    //     _enemy.LookTarget.Position = _enemy.LookTarget.Position.Lerp(_enemy.Velocity, delta * _enemy.Speed * 12);
    //     
    //     _enemy.EnemySpriteNode.LookAt(_enemy.LookTarget.GlobalPosition, Vector3.Up, true);
    //
    // }
    
}