import pika

print('Stating consumer')

connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost', port=5672))
channel = connection.channel()

channel.queue_declare('hotel-event-queue')

print(' [*] Waiting for messages. To exit press CTRL+C')

def callback(ch, method, properties, body):
    print(" [x] Received %r" % (body,))
    ch.basic_ack(delivery_tag = method.delivery_tag)

channel.queue_bind(queue='hotel-event-queue', exchange='MyApp.Transit:SimpleTextMessage')
channel.basic_consume(callback, queue='hotel-event-queue')
channel.start_consuming()