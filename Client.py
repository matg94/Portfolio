import socket
import _thread
import time
#network manager for jack



class NetworkManager:
    
    def __init__(self, IP, PORT):
        self.ip = IP
        self.port = PORT
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.listening = True
        self.unreadMessages = {}
        
    def connectToServer(self):
        self.socket.connect((self.ip, self.port))
        
    def login(self, username, password):
        self.socket.send(username.encode() + b':' + password.encode())
        self.token = self.socket.recv(32)
        print(self.token)
    
    def sendMessage(self, message):
        self.socket.send(self.token + b' ' + message.encode())
        answer = self.socket.recv(256)
        return answer
    
    def logout(self):
        self.listening = False
        print(self.sendMessage("logout"))
        
    def echo(self, msg):
        print(self.sendMessage("echo " + msg))
        
    def sendMessageUser(self, user, message):
        print(self.sendMessage("msg " + user + " " + message))
        
    def receiveThread(self):
        while (self.listening):
            try:
                data = self.socket.recv(256)
                dat = data.decode("utf-8")
                if (dat.contains(":")):
                    sender = dat.split(":")[0]
                    self.unreadMessages.append({sender, dat})
            except Exception as e:
                print(e)
                
    def userInputThread(self):
        print("Welcome to chatApp")
        print("1. Chat with user")
        print("2. Send other commands")
        print("3. Logout")
        ans = input(":")
        if (ans == "1"):
            self.chatWithUser()
        elif (ans == "2"):
            self.sendCommand()
        elif (ans == "3"):
            self.logout()
        else:
            userInputThread()
    
    def chatWithUser(self):
        print("Select user")
        users = self.sendMessage("lson")
        usersList = users.split(b'\n')
        for x in range(1, len(usersList)):
            print(str(x) + ". " + usersList[x-1].decode("utf-8"))
        ans = input(":")
        self.chatLoop(usersList[int(ans)-1])
    
    def chatLoop(self, username):
        self.chatting = True
        while (self.chatting):
            for m in self.unreadMessages:
                if (m[0] == username):
                    print(m[1])
            msg = input(">")
            if (msg == "exit"):
                self.chatting = False
                self.userInputThread()
            else:
                self.sendMessageUser(username, msg)
                
    def sendCommand(self):
        self.commands = True
        while (self.commands):
            cmd = input("://")
            if (cmd == "//exit"):
                self.commands = False
                self.userInputThread()
            else:
                print(self.sendMessage(cmd))
        
    
#Your code goes here
server = NetworkManager("82.38.127.71", 35565)
server.connectToServer() 
server.login("Matg94", "password123")
time.sleep(1)
server.userInputThread()
_thread.start_new_thread(server.receiveThread, ())
    
