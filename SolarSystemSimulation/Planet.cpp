#include "Planet.h"

Planet::Planet(float x, float y, float xv, float yv, float radius, float mass)
	: xPos(x), yPos(y), xVelocity(xv), yVelocity(yv), Radius(radius), Mass(mass) {

	xAcc = 0.0f;
	yAcc = 0.0f;
	Angle = 0.0f;
	Active = true;
	Star = false;

	Distance = 0.0f;

	trail.xPos = x;
	trail.yPos = y;

	Velocity = sqrt((pow(xVelocity, 2) + pow(yVelocity, 2)));

}

void Planet::Update(float elapsed) {


		xVelocity += xAcc*elapsed;
		yVelocity += yAcc*elapsed;

		xPos += xVelocity*elapsed;
		yPos += yVelocity*elapsed;

		trail.xPos = xPos;
		trail.yPos = yPos;

		trail.Update((elapsed / 1000000.0f), orbitalPeriod);

}