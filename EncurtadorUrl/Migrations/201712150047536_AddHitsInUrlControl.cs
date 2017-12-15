namespace EncurtadorUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHitsInUrlControl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrlControl", "Hits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrlControl", "Hits");
        }
    }
}
