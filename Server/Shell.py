import TokenManager
import os

def ProcessCommand(command):
    args = command.split(" ")
    token = args[0]
    permission = TokenManager.get_token_permission(token)
    if (permission == None or permission == -1):
        return ("Invalid Token")
        
    cmd = args[1]
    
    if (cmd == "echo"):
        return Echo(args[2:])
    elif (cmd == "logout"):
        return Logout(token)
    elif (cmd == "head"):
        return Head(token, args[2:])
    elif (cmd == "perm"):
        return Perm(token, args[2:])
    elif (cmd == "permf"):
        return PermFile(token, args[2:])
    elif (cmd == "writef"):
        return AppendFile(token, args[2:])
    elif (cmd == "createf"):
        return CreateFile(token, args[2:])
    elif (cmd == "delf"):
        return DeleteFile(token, args[2:])
    elif (cmd == "mkuser"):
        return CreateAccount(token, args[2:])
    elif (cmd == "rmuser"):
        return RemoveAccount(token, args[2:])
    elif (cmd == "ls"):
        return ListFiles(token, args[2:])
    elif (cmd == "lsacc"):
        return ListAccounts(token)
    elif (cmd == "msg"):
        return MessageUser(token, args[2:])
    elif (cmd == "lson"):
        return ListOnlineUsers(token)
    elif (cmd == ""):
        return ("")
          
    else:
        return ("Invalid command")
    

def Echo(arg):
    return (' '.join(arg))

def Logout(token):
    TokenManager.remove_token_user(token)
    return ("logout success! No more commands will be accepted.")

def Head(token, args):
    if (len(args) < 1):
        return ("Invalid command arguments.")
    filename = args[0]
    if (TokenManager.get_file_permission(token, filename)):
        lines = 20
        count = 0
        head = " \n"
        if (len(args) > 1):
            lines = int(args[1])
        try:
            file = open(filename, 'r')
            for l in file:
                head += l
                count += 1
                if (count >= lines): break;
            file.close()
        except Exception as e:
            return str(e)
        return head
    else:
        return ("You do not have access to that file.")
    
def Perm(token, args):
    permission = TokenManager.get_token_permission(token)
    username = TokenManager.get_username_from_token(token)
    if (len(args) > 0 and permission.rstrip() == "ADMIN"):
        username = args[0]
        permission = TokenManager.get_permission_from_username(username)
        if (permission == -1):
            return ("User not found.")
    return ("User: " + username + " has permission status : " + permission).rstrip()

def PermFile(token, args):
    if (len(args) < 1):
        return "Invalid command arguments."
    filename = args[0]
    owner = TokenManager.get_file_owner(token, filename)
    if (owner == -1):
        return "File not found"
    return owner
    

def CreateFile(token, args):
    if (len(args) < 1):
        return "Invalid command arguments."
    filename = args[0]
    file = open(filename, 'w+')
    public = 0
    if (len(args) > 1 and args[1] != ''):
        public = int(args[1])
    permFile = open("Files/FilePermission.txt", 'a')
    if (public == 1):
        permission = "ALL"
    else:
        permission = TokenManager.get_username_from_token(token)
    permFile.write(filename + ":" + permission + "\n")
    permFile.close()
    return ("File " + filename + " has been created.")

def AppendFile(token, args):
    if (len(args) < 1):
        return "Invalid command arguments."
    filename = args[0]
    text = args[1:]
    if (TokenManager.get_file_permission(token, filename)):
        file = open(filename, 'a')
        file.write((' ').join(text) + '\n')
        file.close()
        return ("Text added to file : " + filename)
    return ("Invalid Permissions")

def DeleteFile(token, args):
    if (len(args) < 1):
        return "Invalid command arguments."
    filename = args[0]
    if (TokenManager.get_file_permission(token, filename)):
        if (filename == "Files/FilePermission.txt" or filename == "Files/Account.txt"):
            return ("Cannot delete Permission or Account files.")
        os.remove(filename)
        TokenManager.remove_file_from_permissions(filename)
        return ("File has been deleted")
    return ("File cannot be deleted")

def CreateAccount(token, args):
    if (len(args) < 2):
        return "Invalid command arguments."
    username = args[0]
    password = args[1]
    admin = "0"
    if (len(args) > 2):
        admin = args[2]
    if (TokenManager.get_token_permission(token).rstrip() != "ADMIN"):
        return "Invalid Command"
    permission = "USER"
    if (admin == "1"):
        permission = "ADMIN"
    code = TokenManager.create_account_on_file(username, password, permission)
    if (code):
        return "Account has been created."
    return "Could not create account: " + code

def RemoveAccount(token, args):
    if (len(args) < 1):
        return "Invalid command arguments."
    username = args[0]
    toDeletePermission = TokenManager.get_permission_from_username(username)
    perm = TokenManager.get_token_permission(token)
    if (perm.rstrip() != "ADMIN"):
        return "You cannot delete accounts."
    if (toDeletePermission.rstrip() == "ADMIN"):
        return "Cannot delete admin account"
    
    if (TokenManager.delete_account_on_file(username) == True):
        return "Account has been deleted."
    return "Account not found."

def ListFiles(token, args):
    if (len(args) > 0):
        username = args[0]
    else:
        username = TokenManager.get_username_from_token(token)
    files = TokenManager.get_filenames_of(token, username)
    if (files == []):
        return "No files found."
    message = ""
    for f in files:
        message += f + "\n"
    return message
    
def ListAccounts(token):
    accounts = TokenManager.get_accounts(token)
    if (accounts == -1):
        return "You do not have access to this command."
    return TokenManager.get_accounts(token)

def ListOnlineUsers(token):
    online = TokenManager.get_online_usernames()
    msg = ""
    for u in online:
        msg = msg + "\n" + u
    return msg

def MessageUser(token, args):
    receiver = args[0]
    sender = TokenManager.get_username_from_token(token)
    if (sender != -1):
        return TokenManager.send_user_message(sender, receiver, args[1:])

