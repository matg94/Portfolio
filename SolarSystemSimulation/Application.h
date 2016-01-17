
#pragma once

#ifdef _WINDOWS
#include <GL/glew.h>
#endif
#include <SDL.h>
#include <SDL_opengl.h>
#include <SDL_image.h>

#include "ShaderProgram.h"
#include "Planet.h"
#include "Matrix.h"
#include <vector>

#ifdef _WINDOWS
#define RESOURCE_FOLDER ""
#else
#define RESOURCE_FOLDER "NYUCodebase.app/Contents/Resources/"
#endif

class Application {

public:

	SDL_Window* displayWindow;
	ShaderProgram* program;
	Matrix projectionMatrix;
	Matrix viewMatrix;
	Matrix modelMatrix;

	GLuint earthTexture;

	std::vector<int> textures;
	std::vector<Planet*> Planets;

	float LastFrameTick;
	bool quit;

	float G;

	float DrawScale;
	float TimeScale;

	float cam;
	float camP;

	float PastTime;
	int Year;

	bool Paused;

	int Focus;

	int TrailTexture;

	void Init();
	void CreateWorld();
	void Render();
	void Physics(float);
	void Input();

	GLuint LoadTexture(const char *image_path);
	
};