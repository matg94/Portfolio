#include "Application.h";


void Application::Init() {

	SDL_Init(SDL_INIT_VIDEO);
	displayWindow = SDL_CreateWindow("My Game", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 640 * 2, 360 * 2, SDL_WINDOW_OPENGL);
	SDL_GLContext context = SDL_GL_CreateContext(displayWindow);
	SDL_GL_MakeCurrent(displayWindow, context);
#ifdef _WINDOWS
	glewInit();
#endif

	program = new ShaderProgram(RESOURCE_FOLDER"vertex_textured.glsl", RESOURCE_FOLDER"fragment_textured.glsl");
	
	glViewport(0, 0, 640 * 2, 360 * 2);

	projectionMatrix.setPerspectiveProjection(0.78f, 1.777f, -1.0f, 1.0f);

	earthTexture = LoadTexture("Earth.png");

	glEnable(GL_BLEND);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	Focus = 0;

	Year = 0;
	PastTime = 0.0f;
	Paused = true;

}

void Application::CreateWorld() {

	float AU = 149597870700.0f;

	G = (6.67408f*pow(10, -11));

	textures.push_back(LoadTexture("Sun.png"));
	textures.push_back(LoadTexture("QsMercury.png"));
	textures.push_back(LoadTexture("Venus.png"));
	textures.push_back(LoadTexture("Earth.png"));
	textures.push_back(LoadTexture("Mars.png"));
	textures.push_back(LoadTexture("QsJupiter.png"));
	textures.push_back(LoadTexture("QsSaturn.png"));
	textures.push_back(LoadTexture("QsUranus.png"));
	textures.push_back(LoadTexture("QsNeptune.png"));
	textures.push_back(LoadTexture("QsPluto.png"));
	textures.push_back(LoadTexture("QsMoon.png"));

	bool Sol = true;
	bool Lunar = false;

	TimeScale = 1000000.0f;
	//DrawScale = -1 / 370700000.0f;
	DrawScale = 6.68449f*pow(10, -12);

	TrailTexture = LoadTexture("TrailTexture.png");

	Planet* Sun = new Planet(0.0f, 0.0f, 0.0f, 0.0f, 0.25f, (1.988435f*(pow(10, 30))));
	Sun->Star = true;

	Sun->Texture = textures[0];

	Planets.push_back(Sun);


	if (Sol) {

		if (Lunar) {
		
			Planet* Earth = new Planet(AU*1.01671033f, 0.0f, 0.0f, 29290.0f, 0.12f, (5.97219f*(pow(10, 24))));
			Planet* Moon = new Planet((0.00271f*AU + AU*1.01671033f), 0.0f, 0.0f, 30254.0f, 0.12f, (0.07342f*(pow(10, 24))));


			Planets.push_back(Earth);
			Planets.push_back(Moon);
		}

		else {

			Planet* Mercury = new Planet(-0.466698351f*AU, 0.0f, 0.0f, -38860.0f, 0.12f, (3.301f*(pow(10, 23))));
			Planet* Venus = new Planet(0.0f, 0.7282316f*AU, -34700.0f, 0.0f, 0.12f, (4.8676f*(pow(10, 24))));
			Planet* Earth = new Planet(AU*1.01671033f, 0.0f, 0.0f, 29290.0f, 0.12f, (5.97219f*(pow(10, 24))));
			Planet* Mars = new Planet(-1.66599f*AU, 0.0f, 0.0f, -21970.0f, 0.12f, (6.4174f*(pow(10, 23))));

			Planet* Jupiter = new Planet(-5.418f*AU, 0.0f, 0.0f, -12534.0f, 0.12f, (6.39f*(pow(10, 23))));
			Planet* Saturn = new Planet(-10.01f*AU, 0.0f, 0.0f, -9157.0f, 0.12f, (6.39f*(pow(10, 23))));
			Planet* Uranus = new Planet(-19.97f*AU, 0.0f, 0.0f, -6518.0f, 0.12f, (6.39f*(pow(10, 23))));
			Planet* Neptune = new Planet(-29.96f*AU, 0.0f, 0.0f, -5442.0f, 0.12f, (6.39f*(pow(10, 23))));
			Planet* Pluto = new Planet(-33.02*AU, 0.0f, 0.0f, -5618.0f, 0.12f, (6.39f*(pow(10, 23))));


			Planets.push_back(Mercury);
			Planets.push_back(Venus);
			Planets.push_back(Earth);
			Planets.push_back(Mars);
			Planets.push_back(Jupiter);
			Planets.push_back(Saturn);
			Planets.push_back(Uranus);
			Planets.push_back(Neptune);
			Planets.push_back(Pluto);
		}

	}
	else {

		int NumberOfPlanetsRand = 6;

		for (int i = 0; i < NumberOfPlanetsRand; ++i) {

			float Distance = ((float)rand() / (float)RAND_MAX)*0.10f*NumberOfPlanetsRand*AU + 0.5f*AU;

			std::cout << Distance / AU << std::endl;

			int RandomAngle = (rand() % (360));

			float Angle = (float)RandomAngle*(3.1415f / (180.0f));

			int TextureRandom = (rand() % 11);

			float vRand = (float)rand() / (float)RAND_MAX;
			float velMag = (vRand/10.0f + 1.0f)*sqrt((G*Sun->Mass) / (Distance));

			float xV = velMag*cos(-Angle);
			float yV = velMag*sin(-Angle);

			float xLoc = Distance*sin(Angle);
			float yLoc = Distance*cos(Angle);

			Planet* p = new Planet(xLoc, yLoc, xV, yV, 0.10f, 5.97f*(pow(10, 24)));

			p->Texture = textures[TextureRandom];

			Planets.push_back(p);

		}
	}
	


	//Random Generator
	//  (rand()%(max-min))+min;
	
	if (Lunar) {
		Planets[1]->Texture = textures[3];
		Planets[2]->Texture = textures[10];
	}

	for (int i = 0; i < Planets.size(); ++i) {

		if (Sol && !Lunar) {
			Planets[i]->Texture = textures[i];
		}

		float xDistance = Planets[i]->xPos - Planets[0]->xPos;
		float yDistance = Planets[i]->yPos - Planets[0]->yPos;

		float Distance = sqrt(pow(xDistance, 2) + pow(yDistance, 2));

		float GM = (G*Planets[0]->Mass);

		float E = (pow(Planets[i]->Velocity, 2) / 2) - (GM / Distance);

		float Periapsis = (pow(Distance, 2)*pow(Planets[i]->Velocity, 2)) / (-2.0f*GM - (Distance*(pow(Planets[i]->Velocity, 2))));

		Planets[i]->majorAxis = -GM / (2.0f*E);

		Planets[i]->orbitalPeriod = 2.0f * 3.14159265359f*(sqrt((pow(Planets[i]->majorAxis, 3)) / (G*Planets[0]->Mass)));

		Planets[i]->trail.Texture = TrailTexture;
		Planets[i]->trail.TrailInit(Planets[i]->orbitalPeriod, 300);

	}
	
	camP = 0.0f;
	cam = 0.0f;
}

void Application::Render() {

	glClear(GL_COLOR_BUFFER_BIT);

	float ticks = (float)SDL_GetTicks() / 1000.0f;
	float elapsed = ticks - LastFrameTick;
	LastFrameTick = ticks;

	float FixedTimeStep = 1 / 60.0f;
	float fixedElapsed = elapsed;
	if (fixedElapsed > FixedTimeStep * 6) {
		fixedElapsed = FixedTimeStep * 6;
	}

	while (fixedElapsed >= FixedTimeStep) {
		fixedElapsed -= FixedTimeStep;
		Physics(FixedTimeStep);
	}
	camP += 6.0f*cam*elapsed;
	cam = 0.0f;

	/*
	PastTime += elapsed*1000000.0f;

	if (PastTime - ((float)Year * 31557600.0f) >= 31557600.0f) {
		Year += 1;
	}
	*/

	if (camP >= -1.0f) {
		camP = -1.0f;
	}
	else if (camP <= -105.0f) {
		camP = -105.0f;
	}

	Physics(fixedElapsed);

	viewMatrix.identity();

	viewMatrix.Translate(Planets[Focus]->xPos*-DrawScale, Planets[Focus]->yPos*-DrawScale, camP);

	program->setProjectionMatrix(projectionMatrix);
	program->setViewMatrix(viewMatrix);

	glUseProgram(program->programID);

	for (int i = 0; i < Planets.size(); ++i) {

		Matrix pMatrix;
		program->setModelMatrix(pMatrix);

		Planets[i]->trail.Draw(program);

		Matrix modMatrix;

		modMatrix.Translate((Planets[i]->xPos*DrawScale), (Planets[i]->yPos*DrawScale), 0.0f);
		modMatrix.Scale(Planets[i]->Radius, Planets[i]->Radius, 1.0f);

		program->setModelMatrix(modMatrix);

		float vertices[] = { -0.5f, -0.5f, 0.5f, 0.5f, -0.5f, 0.5f, 0.5f, 0.5f, -0.5f, -0.5f, 0.5f, -0.5f };
		glVertexAttribPointer(program->positionAttribute, 2, GL_FLOAT, false, 0, vertices);
		glEnableVertexAttribArray(program->positionAttribute);

		float texCoord[] = { 0.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f };
		glVertexAttribPointer(program->texCoordAttribute, 2, GL_FLOAT, false, 0, texCoord);
		glEnableVertexAttribArray(program->texCoordAttribute);

		glBindTexture(GL_TEXTURE_2D, Planets[i]->Texture);
		glDrawArrays(GL_TRIANGLES, 0, 6);

		glDisableVertexAttribArray(program->positionAttribute);
		glDisableVertexAttribArray(program->texCoordAttribute);

		modMatrix.identity();

	}

	// std::cout << " YEAR : " << Year << std::endl;

	SDL_GL_SwapWindow(displayWindow);
}

void Application::Input() {
	SDL_Event event;
	while (SDL_PollEvent(&event)) {
		if (event.type == SDL_QUIT || event.type == SDL_WINDOWEVENT_CLOSE) {
			quit = true;
		}
		if (event.type == SDL_MOUSEWHEEL) {
				cam = event.wheel.y / abs(event.wheel.y);
				cam *= 5.0f;
		}
		if (event.type == SDL_KEYUP) {
			if (event.button.button == SDL_SCANCODE_LEFT) {
				Focus += 1;
				if (Focus >= Planets.size()) {
					Focus = 0;
				}
			}
			else if (event.button.button == SDL_SCANCODE_RIGHT) {
				Focus -= 1;
				if (Focus < 0) {
					Focus = Planets.size()-1;
				}
			}
			else if (event.button.button == SDL_SCANCODE_SPACE) {
				Paused = !Paused;
			}
		}
	}
}

GLuint Application::LoadTexture(const char *image_path) {

	SDL_Surface *surface = IMG_Load(image_path);

	GLuint textureID;
	glGenTextures(1, &textureID);
	glBindTexture(GL_TEXTURE_2D, textureID);

	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, surface->w, surface->h, 0, GL_RGBA,
		GL_UNSIGNED_BYTE, surface->pixels);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);

	SDL_FreeSurface(surface);

	return textureID;

}

void Application::Physics(float elapsed) {


	if (!Paused) {
		PastTime += elapsed;
	}


	if (!Paused) {

		for (int i = 0; i < Planets.size(); ++i) {

			Planets[i]->xAcc = 0.0f;
			Planets[i]->yAcc = 0.0f;

			for (int a = 0; a < Planets.size(); ++a) {

				if (Planets[i] != Planets[a]) {

					float xDistance = Planets[i]->xPos - Planets[a]->xPos;
					float yDistance = Planets[i]->yPos - Planets[a]->yPos;

					float Distance = sqrt(pow(xDistance, 2) + pow(yDistance, 2));

					if (Distance >= (Planets[i]->Radius + Planets[a]->Radius)) {

						float AccMag = (G*Planets[a]->Mass) / (pow(Distance, 2));

						float Angle = std::atan2(-yDistance, -xDistance);

						Planets[i]->xAcc += AccMag*cos(Angle);
						Planets[i]->yAcc += AccMag*sin(Angle);

					}
				}
			}

			Planets[i]->Update(elapsed * TimeScale);

		}
	}

}