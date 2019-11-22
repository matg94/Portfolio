class Goal {

  float x, y;
  float rad;

  Goal(float x, float y) {

    this.x = x;
    this.y = y;
    rad = 20;
  }

  boolean checkWin(Player p) {
    float dist = sqrt(sq(p.x-x)+sq(p.y-y));

    if (dist < rad + p.rad) {
      return true;
    }

    return false;
  }

  boolean checkCollide(Orb o) {
    float dist = sqrt(sq(o.x-x)+sq(o.y-y));

    if (dist < rad + o.rad) {
      return true;
    }

    return false;
  }


  void draw() {
    fill(0);
    stroke(255);
    ellipse(x, y, rad*2, rad*2);
  }
}