namespace CodeFirts_MigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniDBschema : DbMigration
    {

       // La procédure Up() contient les lignes de code permettant de faire une mise à jour de la base de données (ajout d'une colonne, 
        //ajout d'une table, modification d'une colonne, etc.). 
        //on peut meme y insérer même des instructions SQL.
        public override void Up()
        {
            //@me
            AddColumn("Students", "Rating", c => c.Int(nullable: false));

           Sql("UPDATE Students SET Age = 95 WHERE ID =1");
        }


        //La procédure Down() contient les lignes de code permettant de revenir en arrière pour une mise à jour dont le code 
        //est défini dans la procédure Up() (supprimer la colonne créée, supprimer la table créée, etc.).
        public override void Down()
        {
            //@me
            //DropColumn("Students", "Rating");

        }
    }
}
