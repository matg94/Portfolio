#include "Application.h"

int main(int argc, char *argv[])
{
	
	Application app;

	app.Init();
	app.CreateWorld();

	while (!app.quit) {

		app.Input();
		app.Render();

	}


	SDL_Quit();
	return 0;
}
