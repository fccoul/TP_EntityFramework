namespace CodeFirts_MigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniDBschema : DbMigration
    {

       // La proc�dure Up() contient les lignes de code permettant de faire une mise � jour de la base de donn�es (ajout d'une colonne, 
        //ajout d'une table, modification d'une colonne, etc.). 
        //on peut meme y ins�rer m�me des instructions SQL.
        public override void Up()
        {
            //@me
            AddColumn("Students", "Rating", c => c.Int(nullable: false));

           Sql("UPDATE Students SET Age = 95 WHERE ID =1");
        }


        //La proc�dure Down() contient les lignes de code permettant de revenir en arri�re pour une mise � jour dont le code 
        //est d�fini dans la proc�dure Up() (supprimer la colonne cr��e, supprimer la table cr��e, etc.).
        public override void Down()
        {
            //@me
            //DropColumn("Students", "Rating");

        }
    }
}
