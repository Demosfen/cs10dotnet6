using People.Library;
using static System.Console;

Person person = new();
person.SetAge(22);
WriteLine(person.SayHello);

Student student = new();
student.SetAge(22);
WriteLine(student.Study);
WriteLine(student.SayAge);

Professor teacher = new();
teacher.SetAge(65);
WriteLine(teacher.SayHello);
WriteLine(teacher.Explain);