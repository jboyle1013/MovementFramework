using Godot;
using System;
using MovementFramework.Scripts.Character;
using MovementFramework.Scripts.Player;

public partial class RotateZone : Area2D
{
	[Export(PropertyHint.Range, "-360,360,15")]
	private float NewAngle = 0.0f;
	
	private float OldAngle;

	private float weight = 1;

	public float NewAngleRad { get; private set; }

	[Export] private Camera2D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewAngleRad = Mathf.DegToRad(NewAngle);
	}
	
	public void OnBodyEntered(Node2D body)
	{
		
		if (body is not Character character) return;
		character.IsInRotateZone = true;
		GD.Print("Entered Rotate Zone");
		OldAngle = character.Rotation;
		character.UpDirection = character.UpDirection.Rotated(NewAngleRad);
		character.Rotation = NewAngleRad;

	}
	public void OnBodyExited(Node2D body)
	{
		
		if (body is not Character character) return;
		character.IsInRotateZone = false;
		GD.Print("Left Rotate Zone");
		character.UpDirection = character.UpDirection.Rotated(OldAngle);
		character.Rotation = OldAngle;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
