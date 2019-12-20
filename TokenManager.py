import os,binascii
import hashlib
import User

currentValidTokens = []

def generate_token():
    hex = binascii.b2a_hex(os.urandom(5))
    if hex not in currentValidTokens:
        return hex
    else:
        return generate_token()
    
class TokenPermission:
    
    def __init__(self, token, permission, username, user):
        self.token = token.decode("UTF-8")
        self.permission = permission
        self.username = username
        self.user = user
        
def token_user(permission, username, user):
    tk = generate_token()
    currentValidTokens.append(TokenPermission(tk, permission, username, user))
    return tk

def remove_token_user(token):
    for t in currentValidTokens:
        if (t.token == token):
            currentValidTokens.remove(t)
            if (t.user.connection != None):
                t.user.connection.close()
    return -1
            
def get_token_permission(token):
    for t in currentValidTokens:
        if (t.token == token):
            return t.permission
    return -1    

def get_username_from_token(token):
    for t in currentValidTokens:
        if (t.token == token):
            return t.username
    return -1

def get_token_from_username(username):
    for t in currentValidTokens:
        if (t.username == username):
            return t.token
    return -1
        
def get_permission_from_username(username):
    file = open("Files/Account.txt",'r')
    for t in file:
        dat = t.split(":")
        if (dat[0] == username):
            return dat[2]
    return -1
    
def get_file_permission(token, filename):
    file = open("Files/FilePermission.txt",'r')
    filePerm = ""
    username = get_username_from_token(token).rstrip()
    userPerm = get_token_permission(token).rstrip()
    if (userPerm == "ADMIN"):
        return True
    for l in file:
        dat = l.split(":")
        if (dat[0] == filename):
            filePerm = dat[1].rstrip()
    if (username == filePerm or filePerm == "ALL"):
        return True
    return False

def create_account_on_file(username, password, permission):
    try:
        hash = get_password_hash(password)
        file = open("Files/Account.txt", 'a')
        file.write(username + ":" + hash + ":" + permission + '\n')
        return True
    except Exception as e:
        return e
    
def delete_account_on_file(username):
    file = open("Files/Account.txt", 'r')
    lines = file.readlines()
    file.close()
    file = open("Files/Account.txt", 'w')
    for line in lines:
        dat = line.split(":")
        if (username == dat[0]):
            removed = True
            lines.remove(line)
    if (removed == False):
        return -1
    for line in lines:
        file.write(line)
    file.close()
    tk = get_token_from_username(username)
    if (tk != -1):
        remove_token_user(tk)
    return True

def get_password_hash(password):
    return(hashlib.sha256(password.encode()).hexdigest())

def get_file_owner(token, filename):
    file = open("Files/FilePermission.txt",'r')
    userPerm = get_token_permission(token).rstrip()
    filePerm = ""
    for l in file:
        dat = l.split(":")
        if (dat[0] == filename):
            filePerm = dat[1].rstrip()
    if ((userPerm == "ADMIN" or filePerm != "ADMIN") and filePerm != ""):
        return filePerm
    return -1

def remove_file_from_permissions(filename):
    file = open("Files/FilePermission.txt", 'r')
    lines = file.readlines()
    file.close()
    file = open("Files/FilePermission.txt", 'w')
    for line in lines:
        dat = line.split(":")
        if (filename == dat[0]):
            removed = True
            lines.remove(line)
    if (removed == False):
        return -1
    for line in lines:
        file.write(line)
    file.close()

def get_filenames_of(token, username):
    f = open("Files/FilePermission.txt",'r')
    files = []
    userPerm = get_token_permission(token).rstrip()
    for line in f:
        dat = line.split(":")
        print(dat[1].rstrip() + " " + username)
        if (dat[1].rstrip() == username or dat[1].rstrip() == "ALL" or (userPerm == "ADMIN" and username == get_username_from_token(token))):
            files.append(dat[0])
    print(files)
    f.close()
    return files
        
def get_accounts(token):
    if (get_token_permission(token).rstrip() != "ADMIN"):
        return -1
    f = open("Files/Account.txt", 'r')
    accs = ""
    for line in f:
        dat = line.split(":")
        accs = accs + str(dat[0] + " : " + dat[2])
    return accs
    
def get_online_usernames():
    online = []
    for token in currentValidTokens:
        online.append(token.username)
    return online

def send_user_message(sender, receiver, message):
    if (receiver in get_online_usernames()):
        for t in currentValidTokens:
            if (t.username == receiver):
                return t.user.send_message(sender + ":" + (' '.join(message)))
    return "User does not exist."
    
    