

Map m;

void setup() {
  size(800, 600);
  m = new Map();
  m.init();
}
void draw() {
  background(0,0,0);
  m.update();
  m.draw();
}

void keyPressed() {
  if (key == 'w') {
    m.p.velY = -50;
  }
  if (key == 's') {
    m.p.velY = 50;
  }
  if (key == 'a') {
    m.p.velX = -50;
  }
  if (key == 'd') {
    m.p.velX = 50;
  }
}

void mousePressed() {
  if (!m.hasWon && !m.start) {
    float angle = 0;

    angle = atan2(mouseX - m.p.x, mouseY - m.p.y);

    m.p.angle = angle;
    //scannerSend.trigger();
    boolean scatter = false;
    
    if (mouseButton == LEFT) {
     m.p.fire(false); 
    } else if (mouseButton == RIGHT) {
     m.p.fire(true); 
    }
  }
  if (m.start && m.currentLevel == 1) {
    if (m.bt.checkHoverAndClick(mouseX, mouseY)) { 
      m.start = false;
      return;
    }
  }

  if (m.hasWon && !m.showWalls) {
    if (m.bt.checkHoverAndClick(mouseX, mouseY)) { 
      m.bt.hovering = false;
      if (m.bt.iWon) {
        m.currentLevel++;
      } else {
        m.currentLevel = 1;
      }
      //buttonPressed.trigger();
      m.init();
      m.hasWon = false;
    } else {
      m.bt.hovering = false;
    }
  }
}

void keyReleased() {
  if (key == 'w') {
    m.p.velY = 0;
  }
  if (key == 's') {
    m.p.velY = 0;
  }
  if (key == 'a') {
    m.p.velX = 0;
  }
  if (key == 'd') {
    m.p.velX = 0;
  }
  if (key == 'g') {
    m.showWalls = !m.showWalls;
  }
}

void mouseMoved() {
  if (m.bt.checkHoverAndClick(mouseX, mouseY)) { 
    m.bt.hovering = true;
  } else {
    m.bt.hovering = false;
  }
}