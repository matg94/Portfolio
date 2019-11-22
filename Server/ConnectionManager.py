import socket
from login import Login

class Server:
        
    def __init__(self):
        self.connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.connection.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.connection.bind(('192.168.0.14', 35565))
        self.connection.listen(10)
        self.accepting = True
        self.accept_connections()
        
    def accept_connections(self):
        while (self.accepting):
            current_connection, address = self.connection.accept()
            print("connected " + str(address))
            data = current_connection.recv(2048)
            data = data.decode("utf-8")
            print(data)
            login_info = data.split(':')
            if (len(login_info) < 2):
                continue
            tk = Login(login_info, current_connection, address)
            if (tk != -1):
                current_connection.sendall(tk)
            else:
                current_connection.sendall(b"Invalid Login / Already logged in")
                continue
            
            
            
serv = Server()
print("Server is now accepting connections.")
