namespace FreshTask.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Total = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quntity = c.String(),
                        UnitPrice = c.String(),
                        InvoiceId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            DropTable("dbo.Items");
            DropTable("dbo.Invoices");
        }
    }
}
