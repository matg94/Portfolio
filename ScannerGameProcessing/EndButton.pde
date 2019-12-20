  class EndButton {
  String txt = "Congrats, you made it!";
  float time = 0;
  String txt2;
  String txt3 = "Next Level";
  PFont f;
  boolean hovering = false;
  boolean iWon = false;
  int lvl = 0;

  EndButton() {
    f = createFont("Ariel", 22);
  }

  void setupButton(boolean won, int level) {
    iWon = won;
    lvl = level;
    if (lvl == 0) {
      txt = "Welcome! get to the circle,\n don't touch the rectangles!\nClick to see!";
      txt3 = "Start";
      return;
    }
    if (won) {
      txt = "Congrats, you made it!";
      txt3 = "Next Level";
    } else {
      txt = "Sorry, you hit a block!";
      txt3 = "Restart";
    }
  }

  boolean checkHoverAndClick(float x, float y) {
    if (x > 300 && y > 375 && x < 500 && y < 425) {
      return true;
    }

    return false;
  }

  void draw() {
    if (lvl != 0) {
      txt2 = ("You took: " + int((time)/1000) + " Seconds");
    } else {
      txt2 = "";
    }
    fill(0);
    stroke(255);
    strokeWeight(4);
    rect(0, 200, 800, 200);
    strokeWeight(2);
    fill(0);
    if (hovering) {
      fill(30);
    }
    rect(300, 375, 200, 50);
    fill(255);
    textAlign(CENTER);
    textFont(f, 22);
    text(txt, 400, 250);
    text(txt2, 400, 300);
    if (lvl!=0) {
      text("Level: "+lvl, 400, 330);
    } else {
      textFont(f, 30);
      text("Scanner", 400, 100);
    }

    textFont(f, 18);
    text(txt3, 400, 405);
    strokeWeight(0);
  }
}