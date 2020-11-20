// reads analog input from the five inputs from your arduino board 
// and sends it out via serial

// variables for input pins and
int analogInput[6];
  
// variable to store the value 
int value[6]; 
long int compteur = 0;
 
void setup()
{
  // declaration of pin modes
  for(int i=0;i<6;i++)
  {
    analogInput[i] = i+1;
    value[i] = 0;     
    pinMode(analogInput[i], INPUT);    
  }
  
  // begin sending over serial port
  Serial.begin(9600);
}

void loop() {
  // read the input on analog pin 0:
  int sensorValue = analogRead(A0);
  // Convert the analog reading (which goes from 0 - 1023) to a voltage (0 - 5V):
  float voltage = sensorValue * (5.0 / 1023.0);
  // print out the value you read:
  Serial.println(voltage);
}


