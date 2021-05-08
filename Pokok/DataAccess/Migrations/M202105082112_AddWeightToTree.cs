using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokok.DataAccess.Migrations
{
    [Migration(20210508_2112)]
    public class M202105082112_AddWeightToTree : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("Tree")
                .AddColumn("Weight")
                .AsDouble()
                .Nullable();
        }
    }
}
