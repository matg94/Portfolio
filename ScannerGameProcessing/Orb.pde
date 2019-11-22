
class Orb {
  float x, y;
  float rad;
  float r, g, b;
  float timer = 20;
  float velx, vely;
  boolean active = false;
  boolean collided = false;
  boolean toKill = false;
  boolean scatter;

  Orb(float X, float Y, float r, float g, float b) {
    x = X;
    y = Y;
    this.r = r;
    this.g = g;
    this.b = b;
    rad = 3;
  }

  void emit(float x, float y, float ang, boolean scatter) {
    this.x = x; 
    this.y = y;
    this.scatter = scatter;
    if (scatter) {
      timer = random(2, 4);
    } else {
      timer = random(15, 25);
    }
    velx = sin(ang)*160;
    vely = cos(ang)*160;
    if (scatter) {
      velx *= 1.2;
      vely *= 1.2;
    }

    active = true;
    collided = false;
    if (scatter) {
      r=0;
      g=200;
      b=0;
    } else {
      r=200;
      g=0;
      b=0;
    }
  }

  void update(float time) {
    
    if (active) {
      timer -= time;
      if (timer <= 0) {
        toKill = true;
        return;
      }
      if (!collided) {
        x += velx*time;
        y += vely*time;
      }
    }
  }

  void draw() {
    if (active) {
      fill(r, g, b); 
      noStroke();
      ellipse(x, y, rad*2, rad*2);
    }
  }
}