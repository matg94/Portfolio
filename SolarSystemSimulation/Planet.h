#pragma once
#include "Emitter.h"


class Planet {

public:

	float Velocity;
	float xVelocity;
	float yVelocity;
	float Angle;

	float majorAxis;
	float orbitalPeriod;

	Emitter trail;

	bool Active;

	float Mass;

	float Distance;
	float Radius;

	bool Star;

	float yPos;
	float xPos;

	float yAcc;
	float xAcc;

	int Texture;

	Planet(float x, float y, float xv, float yv, float radius, float mass);

	void Update(float);

};