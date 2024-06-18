using Godot;
using System;
namespace MovementFramework.Scripts.General;

public static class UtilityScripts
{
    /// <summary>
    /// Rotates a 2D vector by the specified angle in radians.
    /// </summary>
    /// <param name="v">The vector to rotate.</param>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The rotated vector.</returns>
    public static Vector2 RotateVector(Vector2 v, float angle)
    {
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        return new Vector2(
            v.X * cosAngle - v.Y * sinAngle,
            v.X * sinAngle + v.Y * cosAngle
        );
    }
    
    public static Vector2 RotateDirectionBasedOnGravity(Vector2 velocity, Vector2 gravityVector)
    {
        // Rotate direction based on the gravity vector
        if (gravityVector == Vector2.Right)
        {
            // Gravity to the right
            return new Vector2(velocity.Y, velocity.X);
        }
        else if (gravityVector == Vector2.Left)
        {
            // Gravity to the left
            return new Vector2(-velocity.Y, velocity.X);
        }
        else if (gravityVector == Vector2.Up)
        {
            // Gravity upwards
            return new Vector2(velocity.X, -velocity.Y);
        }
        else
        {
            // Gravity downwards or other directions
            return velocity;
        }
    }
}