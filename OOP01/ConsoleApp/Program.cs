using People.Library;
using static System.Console;

Person sosok = new("Sosok Slepuhin");
sosok.SayHello();

Student alex = new("Alex Ozr");
alex.SetAge(35);
alex.SayHello();
alex.Study();
alex.SayAge();

Professor masha = new("Masha Voloshina");
masha.SetAge(65);
masha.SayHello();
masha.Explain();