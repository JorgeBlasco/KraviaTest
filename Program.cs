using KraviaTest.DataBase;
using KraviaTest.Events;
using KraviaTest.Handlers.Implementations;
using KraviaTest.Handlers.Interfaces;
using KraviaTest.Services.Implementations;
using KraviaTest.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using EventHandler = KraviaTest.Handlers.Implementations.EventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddTransient<IPostenFailureHandler, PostenFailureHandler>();
builder.Services.AddTransient<IEventHandler, EventHandler>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddHostedService<EventListener>();
//builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//Polinomy.processFile("logic-test-input.txt");
app.Run();


public static class Polinomy
{
    public static void ProcessFile(string filePath)
    {
        List<string> operations = new List<string>(File.ReadAllLines(filePath));
        foreach (string poli in operations)
        {
            Console.WriteLine(poli + " = " + GetResult(poli));
        }
    }
    public static int GetResult(string poli)
    {
        poli = poli.Replace(" ","");

        //1# Search for parenthesis
        while (poli.Contains("(")) {
            int openPPos = poli.LastIndexOf('(');
            int closePPos = poli.IndexOf(')',openPPos);
            int partialResult = GetResult(poli.Substring(openPPos + 1,closePPos-openPPos-1));
            poli = poli.Substring(0,openPPos) + partialResult + poli.Substring(closePPos + 1);
        }

        //2# Search for sums or products
        while (poli.Contains('+') || poli.Contains('*')) {
            int op1 = poli.IndexOf('+');
            int op2 = poli.IndexOf('*');
            int opPos;

            //Getting the first operation to do
            if (op1 > 0 && op2 > 0)
                opPos = Math.Min(op1, op2);
            else if (op1 < 0)
                opPos = op2;
            else
                opPos = op1;


            int num1, num2;
            int num2Pos = opPos + 1;

            //Getting first operand
            num1 = int.Parse(poli.Substring(0, opPos));

            //Getting second operand
            while (poli.Length > num2Pos + 1 && Char.IsDigit(poli[num2Pos + 1]))
                num2Pos++;
            num2 = int.Parse(poli.Substring(opPos + 1, num2Pos-opPos));

            int partialResult = 0;
            if (poli.ElementAt(opPos) == '+') partialResult = num1 + num2;
            if (poli.ElementAt(opPos) == '*') partialResult = num1 * num2;

            poli = partialResult + poli.Substring(num2Pos + 1);
        }
        return int.Parse(poli);
    }

}
