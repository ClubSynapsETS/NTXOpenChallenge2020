import socket
from time import sleep
from kafka import KafkaConsumer

# Kafka topic
topics = [
    'micsaData'
]

# Kafka options
options = {
    'bootstrap_servers'      : '10.194.24.26:9092',
    'enable_auto_commit'     : True,
    'auto_commit_interval_ms': 5000,
    'fetch_max_wait_ms'      : 100,
    'fetch_min_bytes'        : 1,
    'fetch_max_bytes'        : 1024 * 1024
}

# UDP CONFIG
UDP_IP   = '127.0.0.1'
UDP_PORT = 5020
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Create Kafka consumer
consumer = KafkaConsumer(*topics, **options)

# Read kafka topic
for message in consumer:

    sock.connect((UDP_IP, UDP_PORT))
    sock.send(message.value)

    print(message.value)