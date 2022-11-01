using Packt.Shared;
using static System.Console;

Person bob = new();
WriteLine(bob.ToString());

bob.Name = "Bob Smith";
bob.DateofBirth = new DateTime(1965, 12, 22); // C# 1.0 or later
bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;
bob.Children.Add(new Person { Name = "Alfred" });
bob.Children.Add(new Person { Name = "Zoe" });

WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
    arg0: bob.Name,
    arg1: bob.DateofBirth);

WriteLine(format:
    "{0}'s favorite wonder is {1}. It's integer is {2}.",
    arg0: bob.Name,
    arg1: bob.FavoriteAncientWonder,
    arg2: (int)bob.FavoriteAncientWonder);

WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

WriteLine($"{bob.Name} has {bob.Children.Count} children:");
for (int child = 0; child < bob.Children.Count; child++)
{
    WriteLine($" {bob.Children[child].Name}");
}

foreach(Person child in bob.Children)
{
    WriteLine(child.Name);
}

Person alice = new()
{
    Name = "Alice Jones",
    DateofBirth = new(1987, 12, 12) // C# 9.0 or later  
};

WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
    arg0: alice.Name,
    arg1: alice.DateofBirth);

//Create Bank Account of Jones and static field InterestRate
BankAccount.InterestRate = 0.012M;

BankAccount jonesAccount = new();
jonesAccount.AccountName = "Mr. Jones";
jonesAccount.Balance = 2400;

WriteLine(format: "{0} earned {1:C} interest.",
    arg0: jonesAccount.AccountName,
    arg1: jonesAccount.Balance * BankAccount.InterestRate);

BankAccount gerrierAccount = new();
gerrierAccount.AccountName = "Ms. Gerrier";
gerrierAccount.Balance = 98;
WriteLine(format: "{0} earned {1:C} interest.",
    arg0: gerrierAccount.AccountName,
    arg1: gerrierAccount.Balance * BankAccount.InterestRate);

WriteLine($"{bob.Name} is {Person.Species}"); //почему нельзя bob.Species? 
                                              //Ведь константа входит в перечень полей типа Person?

WriteLine($"{bob.Name} was born on {bob.HomePlanet}");

Person blankPerson = new();

WriteLine(format:
    "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
    arg0: blankPerson.Name,
    arg1: blankPerson.HomePlanet,
    arg2: blankPerson.Instantiated);

Person gunny = new(initialName: "Gunny", homePlanet: "Mars");

WriteLine(format:
    "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
        arg0: gunny.Name,
        arg1: gunny.HomePlanet,
        arg2: gunny.Instantiated);
