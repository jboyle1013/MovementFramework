using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.Player;
using MovementFramework.Scripts.General;


public partial class gravity_zone : Area2D
{
	[Export(PropertyHint.Range, "-1960,1960,10")]
	public float NewGravityMagnitude = 1000f; // Default magnitude

	[Export(PropertyHint.Range, "-1,1,.01")] public Vector2 gravityVector = ProjectSettings.GetSetting("physics/2d/default_gravity_vector").AsVector2();


	// Called when the node enters the scene tree for the first time.
	private Vector2 NewGravityVector, OrigGravityVector;
	private float OrigGravityMag, angleRadians;

	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is not Character character) return;
		character.IsInGravityZone = true;

		GD.Print("Entered Gravity Zone");
		character.gravityVector.X = 0;
		character.gravityVector.Y = -1;
		character.SpriteNode.FlipV = true;

	}

	public void OnBodyExited(Node2D body)
	{
		if (body is not Character character) return;
		character.IsInGravityZone = false;
		GD.Print("Exited Gravity Zone");
		character.gravityVector.X = 0;
		character.gravityVector.Y = 1;
		character.SpriteNode.FlipV = false;

	}
}
