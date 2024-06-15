using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.Player;
using MovementFramework.Scripts.General;


public partial class gravity_zone : Area2D
{
	[Export(PropertyHint.Range, "-1960,1960,10")]
	public float NewGravityMagnitude = 1000f; // Default magnitude

	[Export(PropertyHint.Range, "0,360, 15")]
	public float RotationAngleDegrees = 0.0f; // Angle to rotate gravity by

	// Called when the node enters the scene tree for the first time.
	private Vector2 NewGravityVector, OrigGravityVector;
	private float OrigGravityMag, angleRadians;

	public override void _Ready()
	{
		angleRadians = Mathf.DegToRad(RotationAngleDegrees);
		NewGravityVector = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)).Normalized();
		int x = 5;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is Character character)
		{
			character.IsInGravityZone = true;

			GD.Print("Entered Gravity Zone");
			if (Math.Abs(RotationAngleDegrees) > 0.01f)
			{


				// Adjust player velocity to match the new gravity direction
				character.Velocity = UtilityScripts.RotateVector(character.Velocity, angleRadians);
			}

			OrigGravityMag = character.gravityMagnitude;
			OrigGravityVector = character.gravityVector;
			character.gravityVector = NewGravityVector.Normalized();
			character.gravityMagnitude = NewGravityMagnitude;
		}

	}

	public void OnBodyExited(Node2D body)
	{

		if (body is Character character)
		{
			character.IsInGravityZone = false;
			GD.Print("Exited Gravity Zone");
			character.gravityVector.X = 0;
			character.gravityVector.Y = 1;

		}

	}
}
