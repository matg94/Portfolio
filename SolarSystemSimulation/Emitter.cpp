#include "Emitter.h"


void Emitter::Update(float elapsed, float OrbitPeriod) {

	if (currentIndex == particles.size()) {
		currentIndex = 0;
	}

	if (CurrentTime >= DropTime) {
		particles[currentIndex].activate();
		particles[currentIndex].xPos = xPos;
		particles[currentIndex].yPos = yPos;
		particles[currentIndex].age = 0.0f;
		CurrentTime = 0.0f;
		currentIndex += 1;
	}
	else { 
		CurrentTime += elapsed;
	}

	for (int i = 0; i < particles.size(); ++i) {
		if (particles[i].age >= particles[i].maxAge) {
			particles[i].age = 0.0f;
			particles[i].Visible = false;
			particles[i].xPos = 0.0f;
			particles[i].yPos = 0.0f;
		}
		else {
			if (particles[i].Visible) {
				particles[i].age += elapsed;
			}
		}
	}

}

void Emitter::TrailInit(float OrbitPeriod, int size) {

	DropTime = (OrbitPeriod/1000000.0f)/size;
	CurrentTime = 0.0f;
	currentIndex = 0;

	for (int i = 0; i < (size); ++i) {
		
		Particle p;
		p.xPos = 0.0f;
		p.yPos = 0.0f;
		p.Visible = false;
		p.maxAge = OrbitPeriod / 1000000.0f;
		p.age = 0.0f;
		particles.push_back(p);
		
	}


}

void Emitter::Draw(ShaderProgram* program) {


	std::vector<float> vertices;
	std::vector<float> texCoords;

	for (int i = 0; i < particles.size(); ++i) {

		if (particles[i].Visible) {

			float size = 0.005f;

			float x = particles[i].xPos*(6.68449f*pow(10, -12));
			float y = particles[i].yPos*(6.68449f*pow(10, -12));

			vertices.insert(vertices.end(), {
				x- size, y+ size,
				x- size, y- size,
				x+ size, y+ size,

				x+ size, y+ size,
				x- size, y- size,
				x+ size, y- size
			});

			texCoords.insert(texCoords.end(), {

				0.0f, 0.0f,
				0.0f, 1.0f,
				1.0f, 0.0f,

				1.0f, 0.0f,
				0.0f, 1.0f,
				1.0f, 1.0f

			});
		}

			glUseProgram(program->programID);

			glVertexAttribPointer(program->positionAttribute, 2, GL_FLOAT, false, 0,
				vertices.data());
			glEnableVertexAttribArray(program->positionAttribute);

			glVertexAttribPointer(program->texCoordAttribute, 2, GL_FLOAT, false, 0,
				texCoords.data());
			glEnableVertexAttribArray(program->texCoordAttribute);

			glBindTexture(GL_TEXTURE_2D, Texture);
			glDrawArrays(GL_TRIANGLES, 0, (vertices.size() / 2));

			glDisableVertexAttribArray(program->positionAttribute);
			glDisableVertexAttribArray(program->texCoordAttribute);

	}

}