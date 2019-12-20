#pragma once
#include <vector>
#include "Emitter.h"
#include "Particle.h"
#include "ShaderProgram.h"

class Emitter {

public:

	float xPos;
	float yPos;

	float DropTime;
	float CurrentTime;

	int Texture;

	int currentIndex;

	std::vector<Particle> particles;

	void Update(float, float);
	void Draw(ShaderProgram*);
	void TrailInit(float, int);

};