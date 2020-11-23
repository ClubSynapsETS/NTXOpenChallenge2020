# NTXOpenChallenge2020
Project submitted for the NeurotechX2020 open challenge

## Myo
We used the myoware muscle sensor kit of sparkfun : https://www.sparkfun.com/products/13723
We connected it to an arduino programmed with the script "data_transfer/AnalogVisualizationArduino.ino" to convert the analog signal to a digital signal and send it to the serial port "/dev/ttyACM0"

## Kafka
### The required python version is 3.7 
With the python script "data_transfer/producer.py" we read the data from the serial port "/dev/ttyACM0" and we send it to our Kafka server at the ip address 10.194.24.26:9092 (requires the ETS vpn to connect) at the topic "micsaData".
Then, the python script "data_transfer/consumer_unity.py" reads the Kafka topic "micsaData" and sends the signal at the port 5020 via the UDP protocol

## Unity
### The required Unity version is 2019.4.11f1
The Unity project can be openned directly in Unity Hub from the folder "Unity_NTXOpenChallenge2020/"
