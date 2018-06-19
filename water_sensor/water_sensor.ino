/* Print values from analog pin A0 to serial monitor */


void setup()
{
  Serial.begin(9600);
  
}

void loop()
{
  int sensorvalue = analogRead(A0);
  Serial.println(sensorvalue);

  
  delay(150);

  if(sensorvalue <300){


    Serial.println("Bijvullen");
    delay(1000);
  }

  if(sensorvalue >300){

    Serial.println("Genoeg water");
    delay(1000);
  }

 
}

