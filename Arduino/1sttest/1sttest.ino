#include <Button.h>

#define outA 3 //Output A from Rotary Encoder
#define outB 2 //Output B from Rotary Encoder

Button btnRU(7);
Button btnRD(8);
Button btnLU(5);
Button btnLD(6);

int currentAstate;
int lastAstate;
int counterR = 0;
int counterL = 0;

void setup() {
  pinMode(outA, INPUT_PULLUP);
  pinMode(outB, INPUT_PULLUP);
  btnRU.begin();
  btnRD.begin();
  btnLU.begin();
  btnLD.begin();
  
  Serial.begin(115200);
  while (!Serial) {
  }

  lastAstate = digitalRead(outA);
}

void loop() {
  if (btnRU.pressed())
    Serial.write(0x01);

  if (btnRD.pressed())
    Serial.write(0x11);

  if (btnLU.pressed())
    Serial.write(0x00);

  if (btnLD.pressed())
    Serial.write(0x10);

  currentAstate = digitalRead(outA);

  if(currentAstate != lastAstate){
    if(digitalRead(outB) != currentAstate){
      counterR++;
      if(counterR == 10)
      {
        Serial.write(0xFF);
        counterR = 0;
        counterL = 0;
      }
    }
    else{
      counterL++;
      if(counterL == 10){
        Serial.write(0x99);
        counterL = 0;
        counterR = 0;
      }
    }
  }

  lastAstate = currentAstate;

  //delay(10);
}
