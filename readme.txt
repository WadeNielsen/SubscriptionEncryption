The start of a program has been provided for you which references an external library with customer subscription information. 
The library has a method named GetSubscriptions that takes a customer ID as input and returns a string with subscription information in an XML object. 

Finish the program to do the following things:
	1. Call the method GetSubscriptions to get a list of Subscription information in XML (In this case the customer ID does not matter so choose your favorite number).
	2. Encode, encrypt or somehow obfuscate the data and save it to a file.
	3. Load the file you saved and decode or decrypt the data.
	4. Print out each subscription name and whether or not the customer is subscribed to the console. 
	
There are two classes started, a Subscription class which is a DTO for the subscriptions and a security class that you can use to put in your logic for obfuscation.  Feel free to add more classes or change the existing as you see fit.