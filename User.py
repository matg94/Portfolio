import socket
import threading
from Shell import ProcessCommand

class User:
    
    def __init__(self, conn, addr, user):
        self.connection = conn
        self.address = addr
        self.Active = True
        self.userName = user
        self.token = None
        
    def receive_data(self):
        while self.Active:
            data = self.connection.recv(256)
            if (data != b''):
                data = data.decode("UTF-8")
                inf = data.split(" ")
                tk = inf[0]
                if (tk != self.token):
                    print("Invalid Token From : " + str(self.address))
                    self.connection.sendall(b"Invalid Token " + tk.encode())
                else:
                    try:
                        self.connection.sendall(ProcessCommand(data).encode())
                    except:
                        return
                    
    def send_message(self, message):
        self.connection.sendall(message.encode())
        return ""
