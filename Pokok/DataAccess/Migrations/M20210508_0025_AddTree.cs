using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokok.DataAccess.Migrations
{
    [Migration(20210508_0025)]
    public class M20210508_0025_AddTree : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("Tree")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Latitude").AsDouble().NotNullable()
                .WithColumn("Longitude").AsDouble().NotNullable()
                .WithColumn("Species").AsString().Nullable()
                ;
        }
    }
}
