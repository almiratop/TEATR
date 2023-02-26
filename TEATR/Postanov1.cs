using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEATR
{
    public class Postanov
    {
        public Postanov()
        {
            this.Spectak = new HashSet<Spektak>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public virtual ICollection<Spektak> Spectak { get; set; }
    }
    public class Spektak
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public System.DateTime Date { get; set; }
        public string Actors { get; set; }
        public byte[] Photo { get; set; }
        public string Actual { get; set; }
        public int PostanovId { get; set; }

        public virtual Postanov Postanov { get; set; }
    }
    public class Kassir
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
    public class Bilet
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Oplata { get; set; }
        public string Buyer { get; set; }
        public int id_Buyer { get; set; }
        public int id_Spektak { get; set; }
        public string Status { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Skidka { get; set; }
        public string Card { get; set; }
    }
    public class EntityModelContainer : DbContext
    {
        public EntityModelContainer()
            : base("name=EntityModelContainer")
        {
        }

        public DbSet<Kassir> Kassirs { get; set; }
        public DbSet<Bilet> Bilets { get; set; }
        public DbSet<Spektak> Spektaks { get; set; }
        public DbSet<Postanov> Postanovs { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
