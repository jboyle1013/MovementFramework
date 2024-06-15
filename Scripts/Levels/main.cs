using Godot;
using System;
using MovementFramework.Scripts.Character;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_gravity_zone_body_entered(Character body)
	{
		GD.Print("Entered Gravity Zone");

	}
}
