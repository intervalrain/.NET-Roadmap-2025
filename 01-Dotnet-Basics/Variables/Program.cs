static void Header(string message)
{
    if (message == null) return;
    Console.WriteLine("===============================");
    Console.WriteLine(message);
    Console.WriteLine("===============================");
}

Header("1. Variables and Data Types");

string message = "Hello, C#!";
int year = 2025;
bool isLearning = true;
double temperature = 28.5;
char grade = 'A';
decimal price = 99.95m;

object[] objects = [message, year, isLearning, temperature, grade, price];

foreach (var obj in objects)
{
    Console.WriteLine(obj);
}

Header("2. Control Flow");

if (temperature > 30)
{
    Console.WriteLine("It's hot");
}
else
{
    Console.WriteLine("It's confortable");
}

for (int i = 0; i < 3; i++)
{
    Console.WriteLine("Operated " + i + " times in the loop");
}

Header("3. Methods");

static void Greet(string name)
{
    Console.WriteLine("Hello, " + name + "!");
}

Greet("Rain");


Header("4. Syntactic Sugar");
// string interpolation

string name = "Rain";
Console.WriteLine($"Hello, {name}. This year is {year}");

// Expression-bodied members
static int Add(int a, int b) => a + b;
Console.WriteLine(Add(5, 3));