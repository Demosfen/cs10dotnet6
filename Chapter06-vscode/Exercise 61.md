**1. What is a delegate?**

*Delegate is a type-safe method pointer. A delegate contains the memory address of a method that matches the same signature as the delegate so that it can be called safely with the correct parameter types.*

**2. What is an event?**

*Events are an example of loose coupling between components because the components do not need to know about each other, they just need to know the event signature.*

**3. How are a base class and a derived class related, and how can the derived class access the base class?**

*A new class inherited (derived) from object, the alias for System.Object. Access to the base class using ": object".*

**4. What is the difference between is and as operators?**

*"IS" keyword is applied when checking an object type. "AS" keyword is applied when casting, it returns null if the type cannot be cast instead of tgrowing an exception.*

**5. Which keyword is used to prevent a class from being derived from or a method from being further overridden?**

*sealed*

**6. Which keyword is used to prevent a class from being instantiated with the new keyword?**

*abstract*

**7. Which keyword is used to allow a member to be overridden?**

*virtual*

**8. What's the difference between a destructor and a deconstruct method?**

*A destructor releases resources; that is, it destroys an object in memory. A Deconstruct method returns an object split up into its constituent parts and uses the C#  deconstruction syntax, for example, when working with tuples*

**9. What are the signatures of the constructors that all exceptions should have?**

*Unlike ordinary methods, constructors are not inherited, so we must explicitly declare and explicitly call the base constructor implementations in System.Exception to make them available to programmers who might want to use those constructors with our custom exception.*

*When defining your own exceptions, give them the same three constructors that explicitly call the built-in ones:*
*public PersonException() : base() { }*
*public PersonException(string message) : base(message) { }*
*public PersonException(string message, Exception innerException): base(message, innerException) { }*

**10.  What is an extension method, and how do you define one?**

  *Extension method was introduced since C# 3.0 and is used to reuse functionality. Add the **static** modifier before the class, and add the **this** modifier before for example the string type.*