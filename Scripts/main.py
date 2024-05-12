import pika

print('Stating consumer')

connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
channel = connection.channel()

channel.queue_declare('python_consumer_1')

print(' [*] Waiting for messages. To exit press CTRL+C')

def callback(ch, method, properties, body):
    print(" [x] Received %r" % (body,))
    ch.basic_ack(delivery_tag = method.delivery_tag)

channel.queue_bind(queue='python_consumer_1', exchange='MyApp.Transit:SimpleTextMessage')
channel.basic_consume(callback, queue='python_consumer_1')
channel.start_consuming()