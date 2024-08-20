using FluentMigrator;
 
 namespace ChatRoomApi.Migrations
 {
    [Migration(202310)]
    public class CreateChatRoomTable : Migration
    {
        public override void Up()
        {
            Create.Table("chat_room")
            .WithColumn("code").AsString(255).NotNullable().Unique().PrimaryKey()
            .WithColumn("owner").AsString(255).NotNullable();

        }
        public override void Down()
        {
            Delete.Table("chat_room");
        }
    }
 }