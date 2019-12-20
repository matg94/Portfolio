
class Map {

  Player p;
  Wall[] walls;
  float timeLastFrame;
  Goal g;
  boolean hasWon = false;
  EndButton bt;
  int currentLevel = 1;
  boolean showWalls = false;
  boolean start = true;
  float levelTime = 0;


  void setupGoal() {
    float x=780, y=random(20, 580);
    g = new Goal(x, y);
    for (int j = 0; j < 20; j ++) {
      if (walls[j].checkCollide(g.x, g.y)) {
        setupGoal();
      }
    }
  }

  void init() {
    bt = new EndButton();
    p = new Player();
    walls = new Wall[24];
    timeLastFrame = millis();
    for (int i = 0; i < 20; i++) {
      walls[i] = new Wall(random(100, 850), random(0, 600), random(30, 130), random(30, 130));
    }
    walls[20] = new Wall(0, 0, 10, 600);
    walls[21] = new Wall(0, 0, 800, 10);
    walls[22] = new Wall(790, 0, 10, 600);
    walls[23] = new Wall(0, 600, 790, 10);
    setupGoal();
    bt.setupButton(false, 0);
    levelTime = 0;
  }
  void update() {
    checkWin(); 
    checkLoss();
    float time = millis()-timeLastFrame; 
    if (!showWalls && !hasWon) {
      levelTime += time;
    }
    bt.time = levelTime;
    p.update(time/1000);
    for (int i = 0; i < p.orbs.size(); i++) {
      for (int j = 0; j < 24; j ++) {
        if (walls[j].checkCollide(p.orbs.get(i))) {
          p.orbs.get(i).velx = 0;
          p.orbs.get(i).vely = 0;
          p.orbs.get(i).collided = true;
        }
      }

      if (g.checkCollide(p.orbs.get(i))) {
        p.orbs.get(i).velx = 0;
        p.orbs.get(i).vely = 0;
        p.orbs.get(i).collided = true;
      }
    }

    timeLastFrame = millis();
  }

  void checkWin() {
    if (g.checkWin(p)) {
      hasWon = true;
      bt.setupButton(true, currentLevel);
    }
  }

  void checkLoss() {
    for (int i = 0; i < 24; i++) {
      if (walls[i].checkCollide(p.x, p.y)) {
        bt.setupButton(false, currentLevel);
        hasWon = true;
      }
    }
  }

  void draw() {
    p.draw();
    if (currentLevel == 1) {
      g.draw();
    }
    if (hasWon || showWalls) {
      g.draw();
      for (int i = 0; i < 24; i++) {
        walls[i].draw();
      }
      if (!showWalls) {
        bt.draw();
      }
    }
    if (start && currentLevel == 1) {
      bt.draw();
    }
  }
}