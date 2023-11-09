using System;
using System.Collections.Generic;
using System.IO;

namespace TLab_9
{
        enum BankType
        {
            Текущий,
            Сберегательный,
        }
        internal class BankAccount
        {
            private static uint unique_id = 1;
            private uint id;
            private double balance;
            private BankType type;
            private Queue<BankTransaction> transactions = new Queue<BankTransaction>();
            private bool disposed = false;

            public void UniqueID()
            {
                unique_id++;
            }

            public BankAccount(BankType type)
            {
                UniqueID();
                id = unique_id;
                this.type = type;
            }

            public BankAccount(double balance)
            {
                UniqueID();
                id = unique_id;
                this.balance = balance;
            }

            public BankAccount(BankType type, double balance)
            {
                UniqueID();
                id = unique_id;
                this.balance = balance;
                this.type = type;
            }

            public void deposit_money(double summa)
            {
                if (summa > 0)
                {
                    BankTransaction transaction = new BankTransaction(summa);
                    transactions.Enqueue(transaction);
                    balance += summa;
                    Console.WriteLine($"\nСчёт пополнен на {summa} р.\nТекущий баланс: {balance}");
                }
                else
                {
                    Console.WriteLine("\nЗначение должно быть положительным");
                }
            }

            public void withdraw_money(double summa)
            {
                if (summa <= balance)
                {
                    BankTransaction transaction = new BankTransaction(summa);
                    transactions.Enqueue(transaction);
                    balance -= summa;
                    Console.WriteLine($"\nСо счёта снято {summa} р.\nТекущий баланс: {balance}");
                }
                else
                {
                    Console.WriteLine("\nНа вашем счёте недостаточно средств");
                }
            }

            public void transfer_money(BankAccount account1, double money)
            {
                if (balance < money)
                {
                    Console.WriteLine("На счете недостаточно средств\n");
                }
                else
                {
                    BankTransaction transaction = new BankTransaction(money);
                    transactions.Enqueue(transaction);
                    balance -= money;
                    account1.balance += money;
                    Console.WriteLine($"Вы перевели {money} рублей на счет {account1.id}, ваш баланс {balance}");
                }
            }

            public void Print()
            {
                Console.WriteLine($"\nНомер вашего счёта: {id}\nБаланс: {balance}\nТип счета: {type}");
            }

            public void Dispose()
            {
                if (!disposed)
                {
                    foreach (BankTransaction transaction in transactions)
                    {

                        File.WriteAllText("transactions.txt", transaction.Print());
                    }
                    transactions.Clear();
                    disposed = true;

                    GC.SuppressFinalize(this);
                }
            }
        }

        internal class BankTransaction
        {
            DateTime time { get; set; }

            readonly double summa;

            public BankTransaction(double summa)
            {
                time = DateTime.Now;
                this.summa = summa;
            }

            public string Print()
            {
                return ($"{time} произошла операция на сумму {summa} рублей\n");
            }
        }

        internal class Song
        {
            private string name { get; set; }
            private string author { get; set; }
            public Song prev { get; set; }


            public Song()
            {

            }
            public Song(string name, string author)
            {
                this.name = name;
                this.author = author;
                prev = null;
            }
            public Song(string name, string author, Song prev)
            {
                this.name = name;
                this.author = author;
                this.prev = prev;
            }
            public void Name()
            {
                Console.WriteLine("Введите название песни: ");
                name = Console.ReadLine();
                Console.WriteLine("Песня добавлена");
            }
            public void Author()
            {
                Console.WriteLine("Введите автора песни: ");
                author = Console.ReadLine();
                Console.WriteLine("Автор добавлен");
            }

            public void Previous(List<Song> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Name();
                    list[i].Author();
                    if (i != 0)
                    {
                        list[i].prev = list[i - 1];

                    }
                    else
                    {
                        Console.WriteLine("Вы находитесь на первой песне в списке");

                    }
                }
            }

            public string Title()
            {
                string info = name + "" + author;
                return info;
            }

            public override bool Equals(object d)
            {
                Song song = d as Song;
                if (song != null && (song.name == name) && (song.author == author))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    internal class Program
    {
        static void Main(string[] args)
        {
            Song mySong = new Song();
        }
    }
}
