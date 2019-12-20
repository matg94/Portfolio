class Wall {
  
  float x, y;
  float sizeX, sizeY;
  
  Wall(float x, float y, float sizeX, float sizeY) {
    this.x = x; 
    this.y = y;
    this.sizeX = sizeX; 
    this.sizeY = sizeY;
  }

  boolean checkCollide(Orb o) {
    if (o.x > x && o.y > y && o.x < x+sizeX && o.y < y+sizeY) {
     return true; 
    }
    return false;
  }
  
  boolean checkCollide(float x, float y) {
   if (x > this.x && y > this.y && x < this.x+sizeX && y < this.y+sizeY) {
    return true; 
   }
   return false;
  }
  
  void draw() {
   fill(0);
   stroke(255);
   rect(x, y, sizeX, sizeY);
    
  }
}