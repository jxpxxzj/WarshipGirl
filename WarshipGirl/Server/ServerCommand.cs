using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using jxGameFramework.Data;
using WarshipGirl.Data;
using WarshipGirl.Server.OriginalData;

namespace WarshipGirl.Server
{
    static class ServerCommand
    {
        private static SQLiteInterop DBInterface = new SQLiteInterop("database.db");
        public static BaseShip GetShipInfo(int ShipID)
        {
            //TODO: Ship
            var data = DBInterface.getDataSet(string.Format("select * from ships where ID = {0} ",ShipID));
            var result = data.Tables[0].Rows[0].ItemArray;
            var ship = new ships(result);
            var target = BaseShip.CreateFromDB(ship);
            return target;
        }
        public static BaseShip GetBuildableRandomShip()
        {
            //TODO: Ship
            var data = DBInterface.getDataSet("select * from ships where build_time<>\" - \" and can_build=1 order by random() limit 1");
            var result = data.Tables[0].Rows[0].ItemArray;
            var ship = new ships(result);
            var target = BaseShip.CreateFromDB(ship);
            return target;
        }
        public static Fleet GetFleetInfo(int PlayerID, int FleetID)
        {
            //TODO: Fleet
            throw new NotImplementedException();
        }
    }
}
