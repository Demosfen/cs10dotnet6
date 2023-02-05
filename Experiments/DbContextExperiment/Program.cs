using DbContextExperiment;

using var db = new DbExperimentContext();
// создаем два объекта User
User tom = new User("Tom", 33 );
User alice = new User( "Alice", 26 );
 
// добавляем их в бд
db.Users.Add(tom);
db.Users.Add(alice);
db.SaveChanges();
Console.WriteLine("Объекты успешно сохранены");
 
// получаем объекты из бд и выводим на консоль
var users = db.Users.ToList();
Console.WriteLine("Список объектов:");
foreach (User u in users)
{
    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
}