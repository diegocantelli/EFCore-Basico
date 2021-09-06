using System;
using System.Linq;
using Curso.Data;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
             using var db = new ApplicationContext();
            // db.Database.Migrate();
            var exists = db.Database.GetPendingMigrations().Any(); 
            Console.WriteLine("Hello World!");
        }
    }
}
