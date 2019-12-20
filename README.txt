This is a server in python created by Mathieu Gautron, for educational purposes only.
It is a work in progress and not yet completed.
The original intent for the creation of this was to have a connection to some of the automation scripts I run at home,
with basic authentication.


The server has several commands listed in the Commands.txt file.

It was created for educational purposes, and to demonstrate a python based server using a command prompt.
The server has three accounts created, 
user: admin, pwd: admin, with admin permissions.
user: test_user, pwd: test, with user permissions.
user: test_user_two, pwd: test, with user permissions.

The server includes basic authentication, though should not be considered secure and should not be opened to the internet.
The security is simply for an exercise and should not be considered final.

To run the server,

Using python3 run ConnectionManager.py

ClientTest.py allows for simple connection to the server, and to run some command.
Part of the reason to write this server, was to leave the client side open to customization,
thus any client scripts can connect to the server, allowing for future creation of custom clients
or GUI based clients. The client side at this time is created just to be able to test the server.

I understand this is not necessarily how a server is meant to be implemented, but again, this was for personal 
educational purposes.
