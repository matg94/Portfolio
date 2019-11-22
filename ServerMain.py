import socket
from User import User

CurrentUsers = []

class Client:
    
    def __init__(self, conn, addr):
        self.connection = conn
        self.address = addr
        CurrentUsers.append(User(self.connection, self.address))

class Server:
        
    def __init__(self):
        self.connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.connection.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.connection.bind(('localhost', 35565))
        self.connection.listen(10)
        self.accepting = True
        self.accept_connections()
        
    def accept_connections(self):
        while (self.accepting):
            current_connection, address = self.connection.accept()
            print("connected " + str(address))
            client = Client(current_connection, address)
            
serv = Server()
