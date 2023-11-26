using Godot;
using System;

public partial class SimBeginScene : Node3D
{
	MeshInstance3D anchor;
	MeshInstance3D ball;
	SpringModel spring;
	double xA, yA, zA; //coordinates of anchor
	float length; // length of pendulum
	double angle; // pendulum angle
	double angleInit; // initial pendulum angle
	double time; 

	Vector3 endA;	// end point of anchor
	Vector3 endB;	// end point for pendulum bob

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello MEE 381 in Godot!");
		xA = 0.0; yA = 1.1; zA = 0.0;
		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		spring = GetNode<SpringModel>("SpringModel");
		endA = new Vector3((float)xA, (float)yA, (float)zA);
		anchor.Position = endA;

		spring.GenMesh(0.5f, 0.08f, 2.0f, 2.0f, 62);

		length = 0.9f;
		angleInit = Mathf.DegToRad(60.0);
		float angleF = (float)angleInit;
		endB.X = endA.X + length*Mathf.Sin(angleF);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z;
		PlacePendulum(endB);
		//PlacePendulum((float)angle);

		time = 0.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float angleF = (float)Math.Sin(3.0 * time);
		endB.X = endA.X + length*Mathf.Sin(angleF);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z;
		PlacePendulum(endB);
		time += delta;
	}

	private void PlacePendulum(Vector3 endBB)
	{
		ball.Position = endBB;
	}
}
