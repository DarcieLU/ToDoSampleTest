namespace ToDoSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoModels", "ToDo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoModels", "ToDo");
        }
    }
}
