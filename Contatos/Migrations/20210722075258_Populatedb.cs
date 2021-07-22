using Microsoft.EntityFrameworkCore.Migrations;

namespace Contatos.Migrations
{
    public partial class Populatedb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Users(Name,Email) Values('Kadu', 'kaduzera.ks@gmail.com')");
            mb.Sql("Insert into Contacts(Name,Number,Description,UserId) Values('Heitor','977777', 'Gamer',(Select UserId from Users where Name='Kadu'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("");
        }
    }
}
