const int trigPin = 6; // Trigger
const int echoPin = 7; // Echo
long duration;
int sum;
int mappedsum;

int AVmeasure[6];
byte lowest = 0;
byte highest =0;

//Vars and functions for E

int AnalogY = 0;
int AnalogX = 1;
int joystickread = 7;

int AnalogYVal = 0;
int AnalogXVal = 0;
bool joystickstate = true;
bool statetosend = false;

int Red = 9;
int Green = 10;
int Blue = 11;



void setup()
{
  Serial.begin(9600);
pinMode(trigPin, OUTPUT); // Sets the trigPin as an Output
pinMode(echoPin, INPUT); // Sets the echoPin as an Input
digitalWrite(trigPin, LOW);
}

void loop()
{

//Serial.println (distanceAv(trigPin,echoPin));
VUMeter();
delay(200);
}
//Opdracht C
int distance(int trig, int echo)
{
  digitalWrite(trig, HIGH); // output puls
  delayMicroseconds(10);
  digitalWrite(trig, LOW);
  duration = pulseIn(echoPin, HIGH, 100000UL);
  byte distance = (duration * 0.034)/2;
  if(distance == 0)
  {
     return -1;
  }else
  {
    return distance;
  }
}

//Opdracht D
int distanceAv(int trig, int echo) //gemiddelde van 6
{
  // Clear values
  highest= 0;
  lowest = 0;
  sum = 0;
   // Get 6 values. Store the index of the highest and lowest in a byte.
  for(byte i=0;i<6;i++)
  {
    AVmeasure[i] = distance(trig,echo);
 
   
    if(AVmeasure[i] > highest)
    {
      highest = AVmeasure[i];
    } 
    else if(AVmeasure[i] < lowest)
    {
      lowest =AVmeasure[i];
    }
  }
  // Remove the lowest and highest numbers from the array
  AVmeasure[lowest] = 0;
  AVmeasure[highest] = 0;
  
  
  {
   // Re-use the duration value from Distance to avoid making another variable
   for(byte i=0;i<6;i++)
   {
    sum += AVmeasure[i];

   }
   return sum /4;  
  }
}

  //Opdracht E

  void VUMeter()
  {
    sum = distanceAv(trigPin,echoPin);
    mappedsum = map(sum,0,20,0,255);
    Serial.println(sum);
   //analogWrite( Green , mappedsum);

  if (sum <0)
  {
       // Foute meting,flikker alle lampjes
       for(int i = 0;i<10;i++){
        PORTB = 0x3F;
        delay(50);
        PORTB=0;
        delay(50);
       }
  } else
  {
    if(sum < 10)
    {
      PORTB=0;
    }
    else if(sum > 10 &&  sum <= 20)
    {
      digitalWrite(Red,HIGH);
      digitalWrite(Green,LOW);
      digitalWrite(Blue,LOW);
    } else if(sum >20 && sum <  30)
    {
       digitalWrite(Red,HIGH);
      digitalWrite(Green,HIGH);
      digitalWrite(Blue,LOW);
    } else 
    {
        digitalWrite(Red,HIGH);
      digitalWrite(Green,HIGH);
      digitalWrite(Blue,HIGH);
    }
  }
/*
  
 //Waarom werkt dit wel, maar analogwrite niet!?!?
 
   if(mappedsum > 100){
    digitalWrite(Green,HIGH);
   }
   else{digitalWrite(Green,LOW);}
   
   */
  }

  
