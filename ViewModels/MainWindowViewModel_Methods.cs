using PBDProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel
    {
        private void InitializeData()
        {
            //initialize ClientTable
            CreateSqlCommand("select * from [Clienti]");
            _sqlCommand.ExecuteNonQuery();
            SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                ClientModel clientModel = new ClientModel((int)sqlDataReader["IdClient"], (string)sqlDataReader["Nume"],
                    (string)sqlDataReader["Prenume"], ((DateTime)sqlDataReader["DataNasterii"]).Date, (string)sqlDataReader["NumarCard"]);
                ClientiList.Add(clientModel);
            }

        }

        private void CreateSqlCommand (string cmdText)
        {
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = CommandType.Text;
            _sqlCommand.CommandText = cmdText;
        }
    }
}
