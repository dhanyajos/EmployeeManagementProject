Employee Management APIs 

An Auth API is used to generate a bearer token. To obtain the bearer token, users must provide a username and password. 

The token includes role information, specifying either 'admin' or 'user.'

With the bearer token, users can execute APIs according to their roles:

Admin: Can perform operations such as Add, Update, and Delete.
User: Can fetch employee details by providing an empid.


