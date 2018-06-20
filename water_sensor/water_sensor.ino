#include <SPI.h>
#include <Ethernet.h>


byte mac[] = {0x40, 0x6c, 0x8f, 0x36, 0x84, 0x8a}; //mac adres van het ethernetshield
IPAddress ip(192,168,1,3);
EthernetServer server(3300);
bool connected = false;

int WaterSensor = 0; //Watersensor variable

void setup()
{
  Serial.begin(9600);
  Ethernet.begin(mac, ip);
    Serial.print("Listening on adres: "); Serial.println(Ethernet.localIP());

server.begin();
  delay(100);
}

void loop()
{
    EthernetClient client = server.available();
    
    if(!client) return;
      Serial.println("Niet verbonden");


  while(client.connected())
    {

//watersensor lezen
  WaterSensor = analogRead(A0);
  Serial.println(WaterSensor);

client.print(WaterSensor);
      delay(150);


  if(WaterSensor <300){


    client.println("Bijvullen");
    delay(1000);
  }

  if(WaterSensor >300){

    client.println("Genoeg water");
    delay(1000);
  }



    case 'w': //sensor waardes van watersensor naar app
            intToCharBuf(WaterSensor, buf, 4);                // convert to charbuffer
            if (bufferGlobal != 0)
            {
              sensorReadInterval = 1000 * bufferGlobal.toInt();
            }
            server.write(buf, 4);                             // response is always 4 chars (\n included)
            Serial.print("Sensor: Case A "); Serial.println(buf);


    }

  


  
  

 
}

