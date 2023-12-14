using Godot;
using System;
// using System.IO;

public partial class SimBeginScene : Node3D
{
	MeshInstance3D anchor;
	MeshInstance3D ball;
	SpringModel spring;
	Label KELabel;
	Label PELabel;
	Label TotLabel;
	int displayCtr;
	int displayThold;
	double L; // natural length of pendulum
	Vector3 dr; // distance between mass and anchor
	Vector3 v0; // initial speed
	double time;
	PendSim pend;

	//---------------------------------
	// _Ready: Called when the node enters the scene tree
	//--------------------------------

	Vector3 endA;	// end point of anchor
	Vector3 endB;	// end point for pendulum bob

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		endA = new Vector3(0.0f, 1.7f, 0.0f);
		dr = new Vector3(-0.7f, -0.2f, 0.5f);
		v0 = new Vector3(0.0f, 0.0f, 0.9f);
		endB = new Vector3();
		endB = endA + dr;

		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		spring = GetNode<SpringModel>("SpringModel");

		L = 0.9;
		spring.GenMesh(0.08f, 0.015f, (float)L, 6.0f, 62);

		anchor.Position = endA;
		ball.Position = endB;
		spring.PlaceEndPoints(endA, endB);		
		
		KELabel = GetNode<Label>("VBoxContainer/KELabel");
		PELabel = GetNode<Label>("VBoxContainer/PELabel");
		TotLabel = GetNode<Label>("VBoxContainer/TotLabel");
		displayCtr = 0;
		displayThold = 2;

		pend = new PendSim();

		//set default parameters;
		pend.X = dr.X;
		pend.Y = dr.Y;
		pend.Z = dr.Z;
		pend.Xdot = v0.X;
		pend.Ydot = v0.Y;
		pend.Zdot = v0.Z;
		time = 0.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(displayCtr > displayThold){
			double KE = pend.KEGet;
			double PE = pend.PEGet;
			double Tot = pend.TotGet;
			KELabel.Text = "KE: " + KE.ToString("0.0000");
			PELabel.Text = "PE: " + PE.ToString("0.0000");
			TotLabel.Text = "Total: " + Tot.ToString("0.0000");
			displayCtr = 0;
		}
		++displayCtr;

		dr.X = (float)pend.X;
		dr.Y = (float)pend.Y;
		dr.Z = (float)pend.Z;
		endB = endA + dr;
		ball.Position = endB;
		spring.PlaceEndPoints(endA, endB);
		
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		pend.StepRK4(time, delta);
		time += delta;
	}

}
