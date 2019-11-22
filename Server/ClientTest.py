import socket
import _thread


sock = socket.socket(socket.AF_INET, # Internet
    socket.SOCK_STREAM) # TCP

def login():
    sock.connect(("82.38.127.71", 35565))
    username = input("Enter username: ")
    pwd = input("Password: ")
    sock.send(username.encode() + b":" + pwd.encode())
    token = sock.recv(32)
    print(token)
    _thread.start_new_thread(receive, ())
    commandPrompt(token)

def receive():
    while True:
        msg = sock.recv(1024).decode("UTF-8")
        if (msg != " " and msg != ''):
            print(msg)

def send_message(token, message):
    if (message == ""):
        return
    sock.send(token + b" " + message.encode())

def commandPrompt(token):
    while True:
        msg = input("")
        send_message(token, msg)
    
login()
