from TokenManager import token_user, remove_token_user, get_password_hash, get_online_usernames
from User import User
import _thread


def Login(login_info, current_connection, address):
    username = login_info[0]
    password = login_info[1]
    """
    users = get_online_usernames()
    if (username in users):
        return -1
    """
    f = open("Files/Account.txt", 'r')
    hash = get_password_hash(password)
    for l in f:
        dat = l.split(":")
        if (dat[0] == username):
            if (hash == dat[1]):
                usr = User(current_connection, address, username)
                tk = token_user(dat[2], username, usr)
                usr.token = tk.decode("UTF-8")
                if (tk != -1):
                    _thread.start_new_thread(usr.receive_data, ())
                    return tk
    return -1    
