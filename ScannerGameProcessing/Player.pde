class Player {

  float x, y;
  float velX, velY;
  ArrayList<Orb> orbs;
  int counter = 20;
  float orbRechargeRate = 0.1;
  int currentOrb = 0;
  float angle = 0;
  int rad = 5;
  float scatterTimer = 2.0;


  Player() {
    x = 60;
    y = 300;
    velX = 0;
    velY = 0;
    orbs = new ArrayList<Orb>();
  }

  void update(float time) {
    x+=velX*time;
    y+=velY*time;
    orbRechargeRate -= time;
    scatterTimer -= time;
    if (orbRechargeRate <= 0 && counter < 20) {
      counter ++; 
      orbRechargeRate = 0.1;
    }
    for (int i = 0; i < orbs.size(); i++) {
      orbs.get(i).update(time);
    }
    for (int i = 0; i < orbs.size(); i++) {
      if (orbs.get(i).toKill) {
        orbs.remove(i);
      }
    }
  }

  void fire(boolean scatterFire) {
    if (scatterFire) {
     if (scatterTimer > 0) {
      return; 
     }
    }
    float ang;
    int swit;
    float mult;
    int oriCounter;
    if (counter < 4) {
      return;
    }
    if (!scatterFire) {
      ang = angle;
      swit = -1;
      mult = 0.61/counter;
      oriCounter = counter;
    } else {
      counter = 200;
      ang = angle;
      swit = -1;
      mult = 6.28/counter;
      oriCounter = counter;
      scatterTimer = 10.0;
    }
    while (counter >= 0) {

      orbs.add(new Orb(x, y, 200, 0, 0));
      orbs.get(orbs.size()-1).emit(x, y, ang, scatterFire);
      //currentOrb++;
      ang = ang+((oriCounter-counter)*mult*swit);
      swit = -1*swit;

      counter --;
    }
  }

  void draw() {
    fill(255, 255, 255);
    ellipse(x, y, rad*2, rad*2);
    for (int i = 0; i < orbs.size(); i++) {
      orbs.get(i).draw();
    }
  }
}