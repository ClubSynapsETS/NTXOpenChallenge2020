#!/usr/bin/python
import serial
import time
from time import sleep
from kafka import KafkaProducer

# USB source is "/dev/ttyUSB0" when using custom circuit and "/dev/ttyACMX" (X is number, depends on port used) for Myoware circuit with arduino
ser = serial.Serial('/dev/ttyACM0')
ser.flushInput()

# Connect to Kafka and send data to topic micsaData
producer = KafkaProducer(bootstrap_servers=['10.194.24.26:9092'])
topic = 'micsaData'

# Keep sending data
while True:
	try:
	    data = ser.readline()
	    if data:
	    	data = data.rstrip(b'\r\n')
	    	print(data)
	    	producer.send(topic, data)
	except:
		print("Error read")


