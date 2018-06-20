#include <SPI.h>
#include <Ethernet.h>


byte mac[] = {0x40, 0x6c, 0x8f, 0x36, 0x84, 0x8a}; //mac adres van het ethernetshield
IPAddress ip(192,168,1,3);
EthernetServer server(3300);
bool connected = false;

int WaterSensor = 0; //Watersensor variable
bool GenoegWater;


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

      //drempel waardes
  if(WaterSensor <300){
    GenoegWater = false;
  }
  else{
    GenoegWater = true;
  }

    case 'w': //sensor waardes van watersensor naar app
            intToCharBuf(WaterSensor, buf, 4);                // convert to charbuffer
            
           if(GenoegWater = true){  
            client.println("1");
            }

            else(GenoegWater = false){

              client.println("0");
            }


            
            server.write(buf, 4);                             // response is always 4 chars (\n included)
            Serial.print("Waterniveau Status: "); Serial.println(buf);


    }

  


  
  

 
}

