    using FluentMigrator;

    namespace ChatRoomApi.Migrations
    {
        [Migration(202410)]
        public class CreateMessagesTable : Migration
        {
        public override void Up()
        {
            Create.Table("messages")
            .WithColumn("code").AsString(255).NotNullable() 
            .WithColumn("msg_id").AsInt64().Identity().PrimaryKey()
            .WithColumn("sender").AsString(255).NotNullable()
            .WithColumn("message").AsString().NotNullable()
            .WithColumn("msg_time").AsTime().NotNullable();

                        
            Create.ForeignKey("fk_code")
            .FromTable("messages")
            .ForeignColumn("code")
            .ToTable("chat_room")
            .PrimaryColumn("code");

        }

        public override void Down()
        {
            Delete.Table("messages");
        }
    }
    }