using Godot;
namespace MovementFramework.Scripts.General;

public class GameConstants
{
    // Animations
    public static class PlayerAnimation
    {
        public static readonly StringName AnimIdle = new ("idle");
        public static readonly StringName AnimMoving =  new ("running");
        public static readonly StringName AnimSlashing = new ("slashing");
        public static readonly StringName AnimSliding = new ("sliding");
        public static readonly StringName AnimDying = new ("dying");
    }

    public static class Input
    {
        // Inputs
        public static readonly StringName MoveLeft = new("player_left");
        public static readonly StringName MoveRight = new("player_right");
        public static readonly StringName MoveUp = new("player_up");
        public static readonly StringName MoveDown = new("player_down");
        public static readonly StringName Dash = new("player_dash");
        public static readonly StringName Jump = new("player_jump");
    }
    
    public static class EnemyAnimation
    {
        public static readonly StringName AnimIdle = new ("Idle");
        public static readonly StringName AnimMoving =  new ("Move");
        public static readonly StringName AnimSlashing = new ("Attack");
        public static readonly StringName AnimHurt = new ("Hit");
        public static readonly StringName AnimDying = new ("Death");
    }

}
