using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;


namespace Modul
{
    class Program
    {
        private static void Usage()
        {
            string help = "\nСправка по использованию \n" +
            "Это программа, которая будет работать с базами данных.\n" +
            "Ключи вводятся:\n" +
            "a.exe -db <database name> [-?] [-v]\n" +
            "-db <database name> - имя базы данных\n" +
            "-? - вызов справки\n" +
            "-v - дополнительная информация";

            Console.Write(help);
            Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            string database = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-db")
                    database = args[++i];
                if (args[i] == "-?")
                    Usage();
            }

            string src = "Data Source=" + database;
            SQLiteConnection conn = new SQLiteConnection(src);
            conn.Open();
            List<string> list = new List<string>();
            using (SQLiteCommand command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", conn))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((string)reader[0]);
                }
            }
            MainForm form = new MainForm(list, conn);
            Application.Run(form);
        }
    }
}
